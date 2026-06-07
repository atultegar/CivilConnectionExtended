using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Context;
using CivilConnection.Interop.Converters;
using CivilConnection.Interop.Utilities;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection.Interop.Services
{
    
    public class AlignmentService
    {
        #region ALIGNMENTS

        public List<AlignmentData> GetAll(string? version = null)
        {
            var context = CivilContext.Create(version);

            var output = new List<AlignmentData>();

            dynamic document = context.Host.Application.ActiveDocument;

            dynamic alignments = document.AlignmentsSiteless;

            foreach (dynamic alignment in alignments)
            {
                var wrapper = new AlignmentWrapper(alignment);

                output.Add(AlignmentConverter.Convert(wrapper, this));
            }

            return output;
        }

        public IList<AlignmentWrapper> GetAlignments(DocumentWrapper documentWrapper)
        {
            Validator.ValidateDocument(documentWrapper);

            var output = new List<AlignmentWrapper>();
            
            foreach (var alignment in documentWrapper.ComObject.AlignmentsSiteless)
            {                
                output.Add(new AlignmentWrapper(alignment));
            }

            return output;
        }

        public AlignmentWrapper GetAlignmentByName(DocumentWrapper documentWrapper, string name)
        {
            Validator.ValidateDocument(documentWrapper);
                       

            foreach(var alignment in documentWrapper.ComObject.AlignmentsSiteless)
            {
                if (string.Equals((string)alignment.DisplayName, name, StringComparison.OrdinalIgnoreCase))
                {
                    return new AlignmentWrapper(alignment);
                }
            }

            return new AlignmentWrapper(null);
        }

        #endregion

        #region STATIONS

        public IList<double> GetGeometryStations(AlignmentWrapper alignment)
        {
            return GetStations(alignment, AlignmentStationType.GeometryPoint);
        }

        public IList<double> GetPIStations(dynamic alignment)
        {
            return GetStations(alignment, AlignmentStationType.PIPoint);
        }

        public IList<double> GetSuperTransStations(dynamic alignment)
        {
            return GetStations(alignment, AlignmentStationType.SuperTransitionPoint);
        }

        public IList<double> GetEquationStations(dynamic alignment)
        {
            return GetStations(alignment, AlignmentStationType.Equation);
        }

        public IList<double> GetStationsAhead(dynamic alignment)
        {
            Validator.ValidateAlignment(alignment);

            var result = new List<double>();

            foreach (dynamic equation in alignment.StationEquations)
            {
                result.Add((double)equation.StationAhead);
            }

            return result;
        }

        public PointData PointByStationOffset(dynamic alignment, double station, double offset = 0, double elevation = 0)
        {
            double easting = 0;
            double northing = 0;

            alignment.PointLocation(station, offset, out easting, out northing);

            return new PointData { X = easting, Y = northing, Z = elevation };
        }

        

        private IList<double> GetStations(AlignmentWrapper alignment, AlignmentStationType stationType)
        {
            if (alignment == null)
                throw new ArgumentNullException(nameof(alignment));

            var result = new List<double>();

            var stations = alignment.ComObject.GetStations(
                (int)stationType,
                alignment.ComObject.StartingStation,
                alignment.ComObject.EndingStation);

            foreach (var station in stations)
            {
                result.Add((double)station.Station);
            }

            return result;
        }

        #endregion
    }
}
