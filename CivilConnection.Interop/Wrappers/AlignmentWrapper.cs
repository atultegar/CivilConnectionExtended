using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Wrappers.Base;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class AlignmentWrapper : ComWrapper
    {
        public AlignmentWrapper(object alignment) : base(alignment)
        {            
        }

        public string Name => (string)ComObject.Name;

        public string DisplayName => (string)ComObject.DisplayName;

        public DocumentWrapper Document => new DocumentWrapper(ComObject.Document);

        public double Length => Convert.ToDouble(ComObject.Length);

        public double StartingStation => Convert.ToDouble(ComObject.StartingStation);

        public double EndingStation => Convert.ToDouble(ComObject.EndingStation);

        public IEnumerable<StationEquationWrapper> StationEquations
        {
            get
            {
                foreach (dynamic stationEquation in ComObject.StationEquations)
                {
                    yield return new StationEquationWrapper(stationEquation);
                }
            }
        }

        public IEnumerable<AlignmentEntityWrapper> Entities
        {
            get
            {
                foreach (dynamic entity in ComObject.Entities)
                {
                    yield return new AlignmentEntityWrapper(entity);
                }
            }
        }

        public IEnumerable<ProfileWrapper> Profiles
        {
            get
            {
                foreach (dynamic profile in ComObject.Profiles)
                {
                    yield return new ProfileWrapper(profile);
                }
            }
        }

        public IEnumerable<ProfileViewWrapper> ProfileViews
        {
            get
            {
                foreach (dynamic profileView in ComObject.ProfileViews)
                {
                    yield return new ProfileViewWrapper(profileView);
                }
            }
        }

        public override string ToString()
        {
            return $"Alignment (Name = {Name})";
        }

        public SOEData GetStationOffset(double x, double y)
        {
            ComObject.StationOffset(
                x,
                y,
                out double station,
                out double offset);

            return new SOEData
            {
                Station = station,
                Offset = offset,
                Elevation = 0
            };
        }

        public PointData PointLocation(double station, double offset)
        {
            ComObject.PointLocation(
                station, 
                offset, 
                out double easting, 
                out double northing);

            return new PointData
            {
                X = easting,
                Y = northing,
                Z = 0
            };
        }

        public string GetStationStringWithEquations(double dRawStation)
        {
            return (string)ComObject.GetStationStringWithEquations(dRawStation);
        }

        public IEnumerable<SampleLineGroupWrapper> SampleLineGroups
        {
            get
            {
                if (ComObject.SampleLineGroups == null)
                    yield break;

                foreach (var group in ComObject.SampleLineGroups)
                {
                    yield return new SampleLineGroupWrapper(group);
                }
            }
        }
    }
}
