using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class BaselineWrapper : ComWrapper
    {
        public BaselineWrapper(object baseline) : base(baseline)
        {
        }

        public int Index => ComObject.Index;

        public double StartStation => ComObject.StartStation;

        public double EndStation => ComObject.EndStation;

        public IEnumerable<double> Stations
        {
            get
            {
                var stations = ComObject.GetSortedStations();

                foreach (var station in stations)
                {
                    if (!stations.Contains(Math.Round(station, 3)))
                    {
                        yield return station;
                    }
                }
            }
        }

        public AlignmentWrapper Alignment => new AlignmentWrapper(ComObject.Alignment);

        public ProfileWrapper Profile => new ProfileWrapper(ComObject.Profile);        

        public CorridorWrapper Corridor => new CorridorWrapper(ComObject.Corridor);

        public IEnumerable<BaselineRegionWrapper> BaselineRegions
        {
            get
            {
                foreach (var blr in ComObject.BaselineRegions)
                {
                    yield return new BaselineRegionWrapper(blr);
                }
            }
        }

        public AlignmentWrapper OffsetAlignment => new AlignmentWrapper(ComObject.MainBaselineFeatureLines.OffsetAlignment);

        public IEnumerable<string> CorridorCodes
        {
            get
            {
                foreach(var code in ComObject.MainBaselineFeatureLines.CodeNames)
                {
                    yield return (string)code;
                }
            }
        }

        public IEnumerable<AlignmentWrapper> GetOffsetAlignments()
        {
            if (ComObject.OffsetBaselineFeatureLinesCol == null)
                yield break;

            foreach (dynamic baselineFeatureline in ComObject.OffsetBaselineFeatureLinesCol)
            {
                if (!(bool)baselineFeatureline.IsMainBaseline)
                {
                    yield return new AlignmentWrapper(baselineFeatureline.OffsetAlignment);
                }
            }
        }

        public PointData StationOffsetElevationToXYZ(double station, double offset, double elevation)
        {
            dynamic xyz = ComObject.StationOffsetElevationToXYZ(new[] { station, offset, elevation });

            return new PointData
            {
                X = xyz[0],
                Y = xyz[1],
                Z = xyz[2]
            };
        }

        public VectorData GetDirectionAtStation(double station)
        {
            dynamic dir = ComObject.GetDirectionAtStation(station);

            return new VectorData
            {
                X = dir[0],
                Y = dir[1],
                Z = dir[2]
            };
        }

        public IEnumerable<dynamic> GetFeaturelinesByCode(string code)
        {
            dynamic featurelines = null;
            try
            {
                featurelines = ComObject.MainBaselineFeatureLines.FeatureLinesCol.Item(code);
            }
            catch
            {
                yield break;
            }

            foreach (dynamic featureline in featurelines)
            {
                yield return featureline;
            }
        }
    }
}
