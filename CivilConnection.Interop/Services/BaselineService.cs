using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection.Interop.Services
{
    public class BaselineService
    {
        public PointData PointByStationOffsetElevation(
            BaselineWrapper baseline, 
            double station, 
            double offset, 
            double elevation)
        {
            return baseline.StationOffsetElevationToXYZ(station, offset, elevation);
        }

        public int GetRegionIndex(BaselineWrapper baseline, double station)
        {
            int index = 0;

            foreach (var region in baseline.BaselineRegions)
            {
                if (region.StartStation <= station &&
                    region.EndStation >= station)
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        public VectorData GetDirectionAtStation(BaselineWrapper baseline, double station)
        {
            return baseline.GetDirectionAtStation(station);
        }

        public List<AlignmentWrapper> GetOffsetAlignments(BaselineWrapper baseline)
        {
            return baseline.GetOffsetAlignments().ToList();
        }

        public List<FeaturelineData> GetFeaturelinesAtStation(BaselineWrapper baseline, string code, double station)
        {
            var result = new List<FeaturelineData>();

            int regionIndex = GetRegionIndex(baseline, station);

            if (regionIndex < 0)
                return result;

            var region = baseline.BaselineRegions.ElementAt(regionIndex);

            foreach (dynamic featureline in baseline.GetFeaturelinesByCode(code))
            {
                var data = new FeaturelineData
                {
                    Code = code,
                    RegionIndex = regionIndex
                };

                foreach (dynamic point in featureline.FeatureLinePoints)
                {
                    double pointStation = Math.Round((double)point.Station, 5);

                    bool inside = 
                        pointStation >= region.StartStation &&
                        pointStation <= region.EndStation;

                    if (!inside)
                        continue;

                    dynamic xyz = point.XYZ;

                    data.Points.Add(
                        new FeaturelinePointData
                        {
                            X = xyz[0],
                            Y = xyz[1],
                            Z = xyz[2],
                            Station = pointStation
                        });
                }

                if (data.Points.Count > 1)
                {
                    result.Add(data);
                }
            }
            return result;
        }       
    }
}
