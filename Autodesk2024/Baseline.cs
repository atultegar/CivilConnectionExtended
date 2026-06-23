// Copyright (c) 2016 Autodesk, Inc.
// Copyright (c) 2026 Atul Tegar
//
// Original Author: paolo.serra@autodesk.com
// Maintained and extended by: atul.tegar@gmail.com
// 
// Licensed under the Apache License, Version 2.0 (the "License").
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using CivilConnection.Converters;
using CivilConnection.Interop.Services;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection
{

    /// <summary>
    /// Baseline obejct type.
    /// </summary>
    public class Baseline
    {
        #region INTERNAL

        internal readonly BaselineWrapper _baseline;

        #endregion

        #region SERVICES

        private readonly LandXmlService _landXmlService;
        private readonly BaselineService _baselineService;
        private readonly CorridorService _corridorService;

        #endregion

        #region PRIVATE PROPERTIES

        private double _start;
        private double _end;
        private double[] _stations;
        private int _index;
        private IList<BaselineRegion> _baselineRegions;
        Corridor _corridor;
        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Gets the alignment.
        /// </summary>
        /// <value>
        /// The alignment.
        /// </value>
        public Alignment Alignment => new Alignment(_baseline.Alignment);

        /// <summary>
        /// Gets the corridor.
        /// </summary>
        public Corridor Corridor => new Corridor(_baseline.Corridor);

        /// <summary>
        /// Gets the start station.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public double Start => _baseline.StartStation;
        /// <summary>
        /// Gets the end station.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public double End => _baseline.EndStation;
        /// <summary>
        /// Gets the stations.
        /// </summary>
        /// <value>
        /// The stations.
        /// </value>
        public double[] Stations => _baseline.Stations.ToArray();
        /// <summary>
        /// Gets the geometry representation of the Baseline.
        /// </summary>
        /// <value>
        /// The poly curves.
        /// </value>
        public IList<PolyCurve> PolyCurves { get { return this.BaselinePolyCurves(); } }
        /// <summary>
        /// Gets the name of the Corridor.
        /// </summary>
        /// <value>
        /// The name of the Corridor.
        /// </value>
        public string CorridorName => _baseline.Corridor.DisplayName;
        /// <summary>
        /// Gets the internal element.
        /// </summary>
        /// <value>
        /// The internal element.
        /// </value>
        internal BaselineWrapper InternalElement => _baseline;
        /// <summary>
        /// Gets the index of the Baseline in the Corridor
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index { get { return this._index; } }
        /// <summary>
        /// Gets the list of BaselineRegions
        /// </summary>
        internal IList<BaselineRegion> BaselineRegions { get { return this._baselineRegions; } }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Internal constructor
        /// </summary>
        internal Baseline(BaselineWrapper baseline, int index, Corridor corridor)
        {
            _baseline = baseline;
            _start = baseline.StartStation;
            _end = baseline.EndStation;
            _corridor = corridor;

            _stations = baseline.Stations.ToArray();
            _index = index;
            _baselineRegions = baseline.BaselineRegions.Select(x => new BaselineRegion(this, x, index)).ToList();
            _landXmlService = new LandXmlService();
            _baselineService = new BaselineService();
            _corridorService = new CorridorService();
        }

        #endregion
               

        #region PUBLIC METHODS

        /// <summary>
        /// Returns the list of BaselineRegions in the Baseline.
        /// </summary>
        /// <returns>A list of BaselineRegions.</returns>
        public IList<BaselineRegion> GetBaselineRegions()
        {
            return this._baselineRegions;
        }

        /// <summary>
        /// Returns the index of the BaselineRegion in the Baseline that contains the station value.
        /// </summary>
        /// <param name="station">A double that represents a station along the corridor.</param>
        /// <returns>An integer for the BaselineRegion that contains the station value.</returns>
        /// <remarks>If the station input is less than the first station it returns 0. If the station input is greater than the last station it returns the number of BaselineRegions - 1.</remarks>
        public int GetBaselineRegionIndexByStation(double station)
        {
            Utils.LogMethodStart(this);

            int output = 0;
            int res = -1;

            foreach (var region in this._baseline.BaselineRegions)
            {
                if (region.StartStation <= station && region.EndStation >= station)
                {
                    res = output;
                    break;
                }
               
                output += 1;
            }

            if (res == -1)
            {
                Utils.Log($"ERROR: The station is not compatible with the Baseline");
            }

            Utils.LogMethodEnd(this);

            return res;
        }

        /// <summary>
        /// Returns a point relative to the Baseline with station, offset and elevation.
        /// </summary>
        /// <param name="station">The distance measured along the Alignment.</param>
        /// <param name="offset">The horizontal displacement from the Baseline measured at a given station.</param>
        /// <param name="elevation">The vertical displacement from the Baseline measured at a given station.</param>
        /// <returns>A Dynamo Point.</returns>
        public Point PointByStationOffsetElevation(double station = 0, double offset = 0, double elevation = 0)
        {
            Utils.LogMethodStart(this);

            var xyz = _baselineService.PointByStationOffsetElevation(_baseline, station, offset, elevation);

            var p = GeometryConverter.ToProtoPoint(xyz);

            Utils.LogMethodEnd(this);

            return p;
        }

        /// <summary>
        /// Returns the Baseline CoordinateSystem at a station value.
        /// </summary>
        /// <param name="station">The input station.</param>
        /// <returns></returns>
        /// <remarks>if the station falls outside of the corridor it returns the Identity Coordinate System.</remarks>
        public CoordinateSystem CoordinateSystemByStation(double station = 0)
        {
            Utils.LogMethodStart(this);

            CoordinateSystem cs = null;

            if (station >= Start && station <= End)
            {
                var xyz = _baselineService.GetDirectionAtStation(_baseline, station);

                Vector y = Vector.ByCoordinates(xyz.X, xyz.Y, xyz.Z);

                Vector x = y.Cross(Vector.ZAxis());

                Point origin = PointByStationOffsetElevation(station, 0, 0);

                cs = CoordinateSystem.ByOriginVectors(origin, x, y, Vector.ZAxis());

                y.Dispose();
                x.Dispose();
                origin.Dispose();
            }
            else
            {
                var message = "The Station value is not compatible with the Baseline.";

                Utils.Log($"ERROR: {message}");

                // throw new Exception(message);
                return null;
            }

            if (cs == null)
            {
                Utils.Log($"ERROR: CoordinateSystem is null.");
            }

            Utils.LogMethodEnd(this);

            return cs;
        }

        /// <summary>
        /// Returns the closest Baseline CoordinateSystem and uses the point as new origin.
        /// </summary>
        /// <param name="point">The input Point.</param>
        /// <returns>The CoordinateSystem.</returns>
        /// <remarks>if the station falls outside of the corridor it returns the Identity Coordinate System.</remarks>
        public CoordinateSystem GetCoordinateSystemByPoint(Point point)
        {
            Utils.LogMethodEnd(this);
           
            var soe = Alignment.InternalElement.GetStationOffset(point.X, point.Y);

            var cs = CoordinateSystemByStation(soe.Station);

            cs = CoordinateSystem.ByOriginVectors(point, cs.XAxis, cs.YAxis, cs.ZAxis);

            Utils.LogMethodEnd(this);

            return cs;
        }

        /// <summary>
        /// Returns the station, offset, elevation of the point with respect to the Baseline.
        /// </summary>
        /// <param name="point">The input Point.</param>
        /// <returns>A double[].</returns>
        [MultiReturn(new string[] { "Station", "Offset", "Elevation" })]
        public Dictionary<string, object> GetStationOffsetElevationByPoint(Point point)
        {
            Utils.LogMethodStart(this);
                        
            var soeData = Alignment.InternalElement.GetStationOffset(point.X, point.Y);

            double station = soeData.Station;
            double offset = soeData.Offset;

            double elevation = point.Z - PointByStationOffsetElevation(station, offset, 0).Z;

            Utils.LogMethodEnd(this);

            return new Dictionary<string, object> { { "Station", station }, { "Offset", offset }, { "Elevation", elevation } };
        }

        /// <summary>
        /// Gets the array station offset elevation by point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>A double[].</returns>
        [IsVisibleInDynamoLibrary(false)]
        public double[] GetArrayStationOffsetElevationByPoint(Point point)
        {
            Utils.Log($"Baseline.GetArrayStationOffsetElevationByPoint {point} started...");

            var soeData = Alignment.InternalElement.GetStationOffset(point.X, point.Y);

            double station = soeData.Station;
            double offset = soeData.Offset;
            double elevation = 0;

            try
            {
                elevation = point.Z - _baseline.Profile.ElevationAt(station);
            }
            catch (Exception ex)
            {
                Utils.Log($"EXCEPTION: {ex.Message} {ex.StackTrace}");
            }

            Utils.LogMethodEnd(this);

            return new double[] { station, offset, elevation };
        }

        /// <summary>
        /// Returns Offset Alignments from the Baseline.
        /// </summary>
        /// <returns>The offset Alignments. Null if there are any.</returns>
        public IList<Alignment> GetOffsetBaselinesAlignments()
        {
            Utils.LogMethodStart(this);

            var wrappers = _baselineService.GetOffsetAlignments(_baseline);

            var result = wrappers
                .Select(x => new Alignment(x))
                .ToList();

            Utils.LogMethodEnd(this);

            return result;
        }

        /// <summary>
        /// Gets the featurelines by code and station
        /// </summary>
        /// <param name="code">the Featurelines code.</param>
        /// <param name="station">the station used to select the featurelines.</param>
        /// <returns></returns>
        public IList<Featureline> GetFeaturelinesByCodeStation(string code, double station)  // 1.1.0
        {
            Utils.Log($"Baseline.GetFeaturelinesByCodeStation({code}, {station}) Started...");

            var data = _baselineService.GetFeaturelinesAtStation(_baseline, code, station);

            var result = new List<Featureline>();

            foreach (var featurelineData in data)
            {
                var points = featurelineData.Points
                    .Select(x => Point.ByCoordinates(x.X, x.Y, x.Z)).ToList();

                points = Point.PruneDuplicates(points).ToList();

                if (points.Count < 2)
                    continue;

                PolyCurve polyCurve = PolyCurve.ByPoints(points);

                double offset = GetArrayStationOffsetElevationByPoint(polyCurve.StartPoint)[1];

                var side = offset < 0
                    ? Featureline.SideType.Left
                    : Featureline.SideType.Right;

                result.Add(
                    new Featureline(
                        this,
                        polyCurve,
                        code,
                        side,
                        featurelineData.RegionIndex));
            }

            Utils.LogMethodEnd(this);

            return result;            
        }
                
        /// <summary>
        /// Gets the featurelines by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IList<IList<Featureline>> GetFeaturelinesByCode(string code)
        {
            Utils.LogMethodStart(this);

            try
            {
                var xmlPath = _corridorService.GetFeaturelinesXmlPath(_baseline.Corridor);
                var data = _baselineService.GetFeaturelines(_baseline, xmlPath, code);

                return FeaturelineConverter.ToDynamo(data, this);
            }
            finally
            {
                Utils.LogMethodEnd(this);
            }
        }

        /// <summary>
        /// Public textual representation of the Dynamo node preview
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Baseline(Start = {Math.Round(Start, 2).ToString()}, End = {Math.Round(End, 2).ToString()}";
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Private method to retrieve Baseline PolyCurves.
        /// </summary>
        /// <returns>A list of PolyCurves for each BaselineRegion.</returns>
        /// <remarks>In case of large dataset, the Geometry Working Range wiill return a warning. Set the Geometry Working Range to Medium.</remarks>
        private IList<PolyCurve> BaselinePolyCurves()
        {
            Utils.Log(string.Format("Baseline.BaselinePolyCurves started...", ""));

            IList<PolyCurve> polyCurves = new List<PolyCurve>();

            foreach (BaselineRegion blr in this.GetBaselineRegions())
            {
                IList<Point> baseLinePoints = new List<Point>();

                foreach (double station in blr.Stations)
                {
                    baseLinePoints.Add(this.PointByStationOffsetElevation(station, 0, 0));
                }

                polyCurves.Add(PolyCurve.ByPoints(baseLinePoints));

                foreach (var p in baseLinePoints)
                {
                    if (p != null)
                    {
                        p.Dispose();
                    }
                }
            }

            Utils.Log(string.Format("Baseline.BaselinePolyCurves completed.", ""));

            return polyCurves;
        }

        /// <summary>
        /// Returns the Offset Alignment name.
        /// </summary>
        /// <returns>The names of the offset Alignments, otherwise "None".</returns>
        private string GetOffsetAlignment()
        {
            if (null != _baseline.OffsetAlignment)
            {
                return _baseline.OffsetAlignment.DisplayName;
            }
            else
            {
                return "<None>";
            }
        }

        #endregion
    }
}
