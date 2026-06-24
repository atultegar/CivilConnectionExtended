using CivilConnection.Contracts.Enums;
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

        public AlignmentWrapper? GetAlignmentByName(DocumentWrapper documentWrapper, string name)
        {
            if (documentWrapper == null)
                throw new ArgumentNullException(nameof(documentWrapper));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    "Alignment name cannot be empty.",
                    nameof(name));

            var alignment = documentWrapper.AlignmentsSiteless
                .FirstOrDefault(x =>
                    string.Equals(
                        x.DisplayName as string,
                        name,
                        StringComparison.OrdinalIgnoreCase));

            return alignment ?? null;
        }

        #endregion

        #region STATIONS

        public IList<double> GetGeometryStations(AlignmentWrapper alignment)
        {
            return GetStations(alignment, AlignmentStationType.GeometryPoint);
        }

        public IList<double> GetPIStations(AlignmentWrapper alignment)
        {
            return GetStations(alignment, AlignmentStationType.PIPoint);
        }

        public IList<double> GetSuperTransStations(AlignmentWrapper alignment)
        {
            return GetStations(alignment, AlignmentStationType.SuperTransitionPoint);
        }

        public IList<double> GetEquationStations(AlignmentWrapper alignment)
        {
            return GetStations(alignment, AlignmentStationType.Equation);
        }

        public IList<double> GetStationsAhead(AlignmentWrapper alignment)
        {

            return alignment.StationEquations.Select(x => x.StationAhead).ToList();
        }

        public PointData PointByStationOffset(AlignmentWrapper alignment, double station, double offset = 0)
        {
            if (alignment == null)
                throw new ArgumentNullException(nameof(alignment));

            return alignment.PointLocation(station, offset);
        }

        private IList<double> GetStations(AlignmentWrapper alignment, AlignmentStationType stationType)
        {
            if (alignment == null)
                throw new ArgumentNullException(nameof(alignment));

            var result = new List<double>();

            var stations = alignment.ComObject.GetStations(
                (int)stationType,
                alignment.StartingStation,
                alignment.EndingStation);

            foreach (var station in stations)
            {
                result.Add((double)station.Station);
            }

            return result;
        }

        public SOEData GetStationOffset(AlignmentWrapper alignment, double x, double y)
        {
            if (alignment == null)
                throw new ArgumentNullException(nameof(alignment));

            return alignment.GetStationOffset(x, y);
        }

        #endregion

        #region PROFILES

        public IList<ProfileWrapper> GetProfiles(AlignmentWrapper alignment)
        {
            if (alignment == null)
                throw new ArgumentNullException(nameof(alignment));

            return alignment.Profiles.ToList();
        }

        public IList<ProfileViewWrapper> GetProfileViews(AlignmentWrapper alignment)
        {
            if (alignment == null)
                throw new ArgumentNullException(nameof(alignment));

            return alignment.ProfileViews.ToList();
        }

        #endregion

        #region GEOMETRY

        public IList<AlignmentEntityWrapper> GetEntities(AlignmentWrapper alignment)
        {
            return alignment.Entities.OrderBy(x => x.StartingStation).ToList();
        }

        public IList<AlignmentEntityData> GetCurves(AlignmentWrapper alignment, double tessellation = 1.0)
        {
            var output = new List<AlignmentEntityData>();

            if (alignment.Entities == null)
                return output;

            var entities = ExpandEntities(alignment.Entities);

            foreach (var entity in entities)
            {
                switch (entity.EntityType)
                {
                    case AlignmentEntityType.Tangent:
                        output.Add(CreateLine(entity));
                        break;

                    case AlignmentEntityType.Arc:
                        output.Add(CreateArc(entity));
                        break;

                    default:
                        output.Add(CreateSampledCurve(alignment, entity, tessellation));
                        break;
                }
            }

            return output.OrderBy(x => x.StartStation).ToList();
        }

        public double AbsoluteStation(AlignmentWrapper alignment, double station)
        {
            int i = 0;

            var stationsAhead = GetStationsAhead(alignment);
            var equationStations = GetEquationStations(alignment);

            while (station > stationsAhead[i])
            {
                if (i < stationsAhead.Count - 1)
                {
                    i++;
                }
                else
                {
                    break;
                }
            }
            double distance = station - stationsAhead[i - 1];
            double stationValue = equationStations[i - 1] + distance;

            return stationValue;
        }

        public string StationFromAbsoluteStation(AlignmentWrapper alignment, double absStation)
        {
            return alignment.GetStationStringWithEquations(absStation);
        }

        public IList<SampleLineGroupData> GetSampleLineParameters(AlignmentWrapper alignment)
        {
            var result = new List<SampleLineGroupData>();

            foreach (var group in alignment.SampleLineGroups)
            {
                var sections = new List<SampleLineSectionData>();

                foreach (var sampleLine in group.SampleLines)
                {
                    foreach (var section in sampleLine.Sections)
                    {
                        sections.Add(new SampleLineSectionData
                        {
                            Station = section.Station,
                            LengthLeft = section.LengthLeft,
                            LengthRight = section.LengthRight,
                            ElevationMin = section.ElevationMin,
                            ElevationMax = section.ElevationMax,
                        });
                    }
                }

                var grouped = sections.GroupBy(x => x.Station).OrderBy(x => x.Key);

                var merged = new SampleLineGroupData
                {
                    Name = group.Name
                };

                foreach (var station in grouped)
                {
                    merged.Sections.Add(
                        new SampleLineSectionData
                        {
                            Station = station.Key,
                            LengthLeft = station.Min(x => -x.LengthLeft),
                            LengthRight = station.Max(x => x.LengthRight),
                            ElevationMin = station.Min(x => x.ElevationMin),
                            ElevationMax = station.Max(x => x.ElevationMax)
                        });
                }

                result.Add(merged);
            }

            return result;
        }

        #endregion

        #region PRIVATE

        private List<AlignmentEntityWrapper> ExpandEntities(IEnumerable<AlignmentEntityWrapper> entities)
        {
            var result = new List<AlignmentEntityWrapper>();

            foreach (var entity in entities)
            {
                if (entity.EntityType == AlignmentEntityType.Unknown)
                {
                    
                }

                if (entity.EntityType == AlignmentEntityType.Tangent || 
                    entity.EntityType == AlignmentEntityType.Arc ||
                    entity.EntityType == AlignmentEntityType.Spiral)
                {
                    result.Add(entity);
                }

                else
                {
                    for (int i = 0; i < entity.ComObject.SubEntityCount; i++)
                    {
                        var subEntity = new AlignmentEntityWrapper(entity.ComObject.SubEntity(i));
                        result.Add(subEntity);
                    }
                }
            }

            return result;
        }

        private AlignmentEntityData CreateLine(AlignmentEntityWrapper entity)
        {
            return new AlignmentLineData
            {
                EntityType = AlignmentEntityType.Tangent,
                StartStation = entity.StartingStation,
                EndStation = entity.EndingStation,
                StartPoint = entity.StartPoint,
                EndPoint = entity.EndPoint
            };

        }

        private AlignmentEntityData CreateArc(AlignmentEntityWrapper entity)
        {
            return new AlignmentArcData
            {
                EntityType = AlignmentEntityType.Arc,
                StartStation = entity.StartingStation,
                EndStation = entity.EndingStation,
                StartPoint = entity.StartPoint,
                EndPoint = entity.EndPoint,
                Center = entity.Center,
                Clockwise = entity.Clockwise
            };
        }

        private AlignmentEntityData CreateSampledCurve(AlignmentWrapper alignment, AlignmentEntityWrapper entity, double tessellation)
        {
            var start = entity.StartingStation;
            var length = entity.Length;
            var a = entity.A;
            var radiusIn = entity.RadiusIn;
            var radiusOut = entity.RadiusOut;

            int divisions = Math.Max(10, (int)Math.Ceiling(length / tessellation));

            double delta = length / divisions;

            var points = new List<PointData>();

            for (int i = 0; i <= divisions; i++)
            {
                var pointData = alignment.PointLocation(start + i * delta, 0);
                points.Add(pointData);
            }

            return new AlignmentSpiralData
            {
                Length = length,
                A = a,
                RadiusIn = radiusIn,
                RadiusOut = radiusOut,
                Points = points
            };
        }

        #endregion
    }
}
