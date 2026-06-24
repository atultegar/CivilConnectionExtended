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

using System;
using System.Collections.Generic;
using System.Linq;

using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;

namespace CivilConnection
{
    /// <summary>
    /// Featureline obejct type.
    /// </summary>
    [DynamoServices.RegisterForTrace()]
    public class Featureline
    {

        /// <summary>
        /// Side enumerator.
        /// </summary>
        [IsVisibleInDynamoLibrary(false)]
        public enum SideType
        {
            ///<excluded/>
            None,
            ///<excluded/>
            Left,
            ///<excluded/>
            Right
        }

        #region PRIVATE PROPERTIES
        /// <summary>
        /// The baseline
        /// </summary>
        Baseline _baseline;
        /// <summary>
        /// The code
        /// </summary>
        string _code;
        /// <summary>
        /// The polycurve
        /// </summary>
        PolyCurve _polycurve;
        /// <summary>
        /// The side
        /// </summary>
        SideType _side;
        /// <summary>
        /// The Baseline Region Index of the Featureline
        /// </summary>
        int _regionIndex;
        /// <summary>
        /// The starting station
        /// </summary>
        double _start;
        /// <summary>
        /// The ending station
        /// </summary>
        double _end;
        /// <summary>
        /// The Feautreline points
        /// </summary>
        IList<Point> _points = new List<Point>();

        /// <summary>
        /// The station - Point map
        /// </summary>
        Dictionary<double, Point> _pointmap = new Dictionary<double, Point>();
        #endregion

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the baseline.
        /// </summary>
        /// <value>
        /// The baseline.
        /// </value>
        public Baseline Baseline { get { return this._baseline; } }
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get { return this._code; } }
        /// <summary>
        /// Gets the PolyCurve.
        /// </summary>
        /// <value>
        /// The curve.
        /// </value>
        public PolyCurve Curve { get { return this._polycurve; } }
        /// <summary>
        /// Gets the start station.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public double Start
        {
            get
            {
                return Math.Round(this._start, 3); 
            }
        }
        /// <summary>
        /// Gets the end station.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public double End
        {
            get
            {
                return Math.Round(this._end, 3); 
            }
        }
        /// <summary>
        /// Gets the side.
        /// </summary>
        /// <value>
        /// The side.
        /// </value>
        public SideType Side { get { return this._side; } }

        /// <summary>
        /// Gets the Baseline Region Index of the Featureline
        /// </summary>
        public int BaselineRegionIndex { get { return this._regionIndex; } }

        /// <summary>
        /// Gets the Featureline points.
        /// </summary>
        public IList<Point> Points
        {
            get
            {
                if (this._points.Count == 0)
                {
                    foreach (Curve c in this._polycurve.Curves())
                    {
                        this._points.Add(c.StartPoint);
                    }

                    this._points.Add(this._polycurve.EndPoint);
                }

                return this._points;
            }
        }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="Featureline"/> class.
        /// </summary>
        /// <param name="baseline">The baseline.</param>
        /// <param name="polycurve">The polycurve.</param>
        /// <param name="code">The code.</param>
        /// <param name="side">The side.</param>
        /// <param name="regionIndex">The region index</param>
        /// <param name="pointMap">The station - Point map for the Featureline</param>
        internal Featureline(Baseline baseline, PolyCurve polycurve, string code, SideType side, int regionIndex = 0, Dictionary<double, Point> pointMap = null)
        {
            _baseline = baseline;
            _polycurve = polycurve;
            _code = code;
            _side = side;
            _regionIndex = regionIndex;
            // 20190524 -- Start
            var startSOE = baseline.InternalElement.Alignment.GetStationOffset(polycurve.StartPoint.X, polycurve.StartPoint.Y);
            var endSOE = baseline.InternalElement.Alignment.GetStationOffset(polycurve.EndPoint.X, polycurve.EndPoint.Y);

            double startStation = startSOE.Station;
            double endStation = endSOE.Station;

            this._start = startStation;
            this._end = endStation;

            if (pointMap != null)
            {
                _pointmap = pointMap;
            }

            // 20190524 -- End
        }

        #endregion

        #region PRIVATE METHODS


        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Creates a Featureline from a corridor Baseline and a LandFeatureline.
        /// The LandFeatureline name must follow Corridor Name | Corridor Region | Corridor Feature Code | Feature Side | Next Counter | Style Name.
        /// </summary>
        /// <param name="baseline">The baseline.</param>
        /// <param name="landFeatureline">Teh land feature line.</param>
        /// <param name="regionIndex">The region Index</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        /// 
        [IsVisibleInDynamoLibrary(false)]
        public static Featureline ByBaselineLandFeatureline(Baseline baseline, LandFeatureline landFeatureline, int regionIndex = 0, string separator = "|")
        {
            Utils.Log($"Featureline.ByBaselineLandFeatureline started.");

            IList<Featureline> output = new List<Featureline>();

            string name = landFeatureline.Name;

            PolyCurve pc = landFeatureline.Curve;

            SideType side = SideType.Right;

            double station = Convert.ToDouble(baseline.GetStationOffsetElevationByPoint(pc.StartPoint)["Station"]);
            double offset = Convert.ToDouble(baseline.GetStationOffsetElevationByPoint(pc.StartPoint)["Offset"]);

            if (offset < 0)
            {
                side = SideType.Left;
            }

            string[] parameters = name.Split(new string[] { separator }, StringSplitOptions.None);

            string code = "UnknownCode";

            try
            {
                code = parameters[2];
            }
            catch
            { }

            Utils.Log($"Featureline.ByBaselineLandFeatureline completed.");

            return new Featureline(baseline, pc, code, side, regionIndex);
        }

        // TODO: Define Coordinate System when the featurelines returns on itslef like a U
        // Minimum distance from baseline
        /// <summary>
        /// CoordinateSystem by station.
        /// </summary>
        /// <param name="station">The station.</param>
        /// <param name="vertical">if set to <c>true</c> the ZAxis is [vertical].</param>
        /// <returns></returns>
        public CoordinateSystem CoordinateSystemByStation(double station, bool vertical = true)
        {
            Utils.LogMethodStart(this);

            CoordinateSystem cs = null;
            CoordinateSystem output = null;

            if (Math.Abs(station - this.Start) < 0.00001)
            {
                station = this.Start;
            }

            if (Math.Abs(station - this.End) < 0.00001)
            {
                station = this.End;
            }

            if (station < this.Start || station > this.End)
            {
                var message = "The Station value is not compatible with the Featureline.";

                Utils.Log($"ERROR: {message}");
            }

            cs = _baseline.CoordinateSystemByStation(station);

            Utils.Log($"CoordinateSystem: {cs}");

            if (cs != null )
            {
                Plane plane = cs.ZXPlane;

                PolyCurve pc = this._polycurve;

                Point p = null;
                double d = double.MaxValue;

                try
                {
                    var intersections = pc.Intersect(plane);

                    Utils.Log($"Intersections: {intersections.Length}");

                    // Get the closest point on the Feature Line
                    foreach (var result in intersections)
                    {
                        if (result is Point)
                        {
                            Point r = result as Point;

                            double dist = cs.Origin.DistanceTo(r);

                            if (dist < d)
                            {
                                p = Point.ByCoordinates(r.X, r.Y, r.Z);
                                d = dist;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utils.Log($"ERROR: {ex.Message}");

                    throw new Exception(ex.Message);
                }

                Utils.Log($"Distance: {d}");

                // 20190415  -- START
                if (p == null)
                {
                    if (Math.Abs(station - this.Start) < 0.0001)
                    {
                        p = pc.StartPoint;
                        Utils.Log($"Point forced on Featureline start.");
                    }
                    if (Math.Abs(station - this.End) < 0.0001)
                    {
                        p = pc.EndPoint;
                        Utils.Log($"Point forced on Featureline end.");
                    }
                }

                // 20190415  -- END

                Utils.Log($"Point: {p}");

                if (null != p)
                {
                    output = pc.CoordinateSystemAtParameter(pc.ParameterAtPoint(p));

                    output = CoordinateSystem.ByOriginVectors(output.Origin, output.YAxis.Cross(Vector.ZAxis()), output.YAxis, Vector.ZAxis());

                    if (vertical)
                    {
                        output = CoordinateSystem.ByOriginVectors(output.Origin,
                        Vector.ByCoordinates(output.XAxis.X, output.XAxis.Y, 0, true),
                        Vector.ByCoordinates(output.YAxis.X, output.YAxis.Y, 0, true),
                        Vector.ZAxis());
                    }
                }
                else
                {
                    Utils.Log($"ERROR: Point is null.");
                    // use the Baseline
                    output = CoordinateSystem.ByOriginVectors(cs.Origin, cs.XAxis, cs.YAxis, cs.ZAxis);

                    Utils.Log($"Baseline is used: {output}");
                }

                if (plane != null)
                {
                    plane.Dispose();
                }

                if (p != null)
                {
                    p.Dispose();
                }
            }
            else
            {
                var message = "The Station value is not compatible with the Featureline and its Baseline.";

                Utils.Log($"ERROR: {message}");

                throw new Exception(message);
            }

            if (cs != null)
            {
                cs.Dispose();
            }

            Utils.Log($"{output}");

            Utils.LogMethodEnd(this);

            return output;
        }

        /// <summary>
        /// Point at station.
        /// </summary>
        /// <param name="station">The station.</param>
        /// <returns></returns>
        public Point PointAtStation(double station)
        {
            return CoordinateSystemByStation(station).Origin;
        }

        /// <summary>
        /// Point the by station offset elevation.
        /// </summary>
        /// <param name="station">The station.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <param name="referToBaseline">if set to <c>true</c> [refer to baseline].</param>
        /// <returns></returns>
        public Point PointByStationOffsetElevation(double station, double offset, double elevation, bool referToBaseline)
        {
            Utils.LogMethodStart(this);

            Baseline baseline = _baseline;

            CoordinateSystem cs = CoordinateSystem.Identity();

            if (!referToBaseline)
            {
                cs = CoordinateSystemByStation(station);
            }
            else
            {
                cs = baseline.CoordinateSystemByStation(station);
            }

            Point p = Point.ByCoordinates(offset, 0, elevation).Transform(cs) as Point;

            cs.Dispose();

            Utils.LogMethodEnd(this);

            return p;
        }

        /// <summary>
        /// Gets the station, offset and elevation for a point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        [MultiReturn(new string[] { "Station", "Offset", "Elevation" })]
        public Dictionary<string, object> GetStationOffsetElevationByPoint(Point point)
        {
            Utils.LogMethodStart(this);

            Utils.Log($"Point: X: {point.X} Y: {point.Y} Z: {point.Z}");

            Point flatPt = Point.ByCoordinates(point.X, point.Y);

            Curve flatPC = this.Curve.PullOntoPlane(Plane.XY());

            var alignment = _baseline.Alignment.InternalElement;

            double station = 0;
            double offset = 0;
            double elevation = 0;

            var soeData = alignment.GetStationOffset(point.X, point.Y);

            double alStation = soeData.Station;
            double alOffset = soeData.Offset;

            Point ortho = null;

            CoordinateSystem cs = null;

            Point result = null;

            Utils.Log($"The point is at station {alStation}");

            // 20190414 -- START

            if (Math.Abs(alStation - Start) <= 0.00001)
            {
                alStation = Start;
                Utils.Log($"Station rounded to {alStation}");
            }

            if (Math.Abs(alStation - End) <= 0.00001)
            {
                alStation = End;
                Utils.Log($"Station rounded to {alStation}");
            }

            // 20190414 -- END

            if (Start <= Math.Round(alStation, 5) && Math.Round(alStation, 5) <= End
                || Math.Abs(Start - alStation) < 0.0001
                || Math.Abs(End - alStation) < 0.0001)
            {
                // the point is in the featureline range

                Utils.Log($"The point is inside the featureline station range.");

                // 20190205 -- START

                ortho = flatPC.ClosestPointTo(flatPt);

                Utils.Log($"Orthogonal Point at Z = 0: {ortho}");

                var orthoSoeData = alignment.GetStationOffset(ortho.X, ortho.Y);

                double orStation = orthoSoeData.Station;
                double orOffset = orthoSoeData.Offset;
                double error = 0;

                cs = CoordinateSystemByStation(orStation, true).Inverse();  // 20191117

                Utils.Log($"CoordinateSystem: {cs}");

                if (cs == null)
                {
                    var message = "The Point is not compatible with the Featureline and its Baseline.";

                    Utils.Log($"ERROR: {message}");
                    return null;
                }

                result = point.Transform(cs) as Point;

                Utils.Log($"Result: {result}");

                station = orStation;
                offset = result.X;
                elevation = result.Z;
                error = result.Y;

                // 20190205 -- END

                
                Utils.Log($"Final Margin: {error}");
            }
            else
            {
                Utils.Log($"The point is outside the featureline station range.");

                try
                {
                    station = alStation;
                    offset = alOffset;  // offset from alignment
                    elevation = point.Z - _baseline.PointByStationOffsetElevation(alStation, 0, 0).Z;  // Absolute elevation
                }
                catch (Exception ex)
                {
                    var message = "The Point is not compatible with the Alignment.";

                    Utils.Log($"ERROR: {message} {ex.Message}");
                    return null;
                }
            }

            Utils.Log($"Station: {station} Offset: {offset} Elevation: {elevation}");

            if (ortho != null)
            {
                ortho.Dispose(); 
            }
            if (cs != null)
            {
                cs.Dispose(); 
            }
            if (result != null)
            {
                result.Dispose(); 
            }
            if (flatPt != null)
            {
                flatPt.Dispose(); 
            }
            if (flatPC != null)
            {
                flatPC.Dispose(); 
            }

            Utils.LogMethodEnd(this);

            return new Dictionary<string, object>
            { 
                { "Station", Math.Round(station, 5) },
                { "Offset", Math.Round(offset, 5) },
                { "Elevation", Math.Round(elevation, 5) } 
            };
        }

        /// <summary>
        /// Gets a PolyCurve obtained by applying the offset and elevation displacement to each point of the Featureline PolyCurve.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <returns></returns>
        public PolyCurve GetPolyCurveByOffsetElevation(double offset, double elevation)
        {
            Utils.LogMethodStart(this);

            double startStation = Start; //  soeStart[0];
            double endStation = End; //  soeEnd[0];

            IList<double> stations = new List<double>() { startStation };

            foreach (double s in Baseline.Stations.Where(s => s >= startStation && s <= endStation))
            {
                stations.Add(s);
            }

            stations.Add(endStation);

            Point p = Point.ByCoordinates(offset, 0, elevation);

            CoordinateSystem cs = null;

            IList<Point> points = new List<Point>();

            foreach (double s in stations)
            {
                cs = this.CoordinateSystemByStation(s);
                points.Add(p.Transform(cs) as Point);
            }

            points = Point.PruneDuplicates(points);  // TODO this is slow

            PolyCurve res = null;

            if (points.Count > 1)
            {
                res = PolyCurve.ByPoints(points);
            }

            Utils.Log(string.Format("{0}", res));

            if (p != null)
            {
                p.Dispose();
            }

            if (cs != null)
            {
                cs.Dispose();
            }

            Utils.LogMethodEnd(this);

            return res;
        }

        /// <summary>
        /// Gets a PolyCurve obtained by applying the offset and elevation displacement to each point of the Featureline PolyCurve only for the station interval specified.
        /// </summary>
        /// <param name="startStation">The start station.</param>
        /// <param name="endStation">The end station.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <returns></returns>
        public PolyCurve GetPolyCurveByStationsOffsetElevation(double startStation, double endStation, double offset, double elevation)
        {
            Utils.LogMethodStart(this);

            if (Math.Abs(startStation - endStation) < 0.00001)
            {
                Utils.Log($"ERROR: start and end station are coincident");
                return null;
            }

            double sStation = Start < End ? Start : End;  // soeStart[0] < soeEnd[0] ? soeStart[0] : soeEnd[0];
            double eStation = Start < End ? End : Start;  // soeStart[0] < soeEnd[0] ? soeEnd[0] : soeStart[0];

            double min = startStation < endStation ? startStation : endStation;
            double max = endStation > startStation ? endStation : startStation;

            startStation = min;
            endStation = max;

            if (startStation < sStation)
            {
                startStation = sStation;
            }

            if (endStation > eStation)
            {
                endStation = eStation;
            }

            //Utils.Log(string.Format("Stations are ready", ""));

            IList<double> stations = new List<double>() { startStation };

            foreach (double s in Baseline.Stations.Where(s => s >= startStation && s <= endStation))
            {
                stations.Add(s);
            }

            stations.Add(endStation);

            //Utils.Log(string.Format("Station List created", ""));

            Point p = Point.ByCoordinates(offset, 0, elevation);

            IList<Point> points = new List<Point>();

            CoordinateSystem cs = null;

            foreach (double s in stations.OrderBy(x => x))  // 20201217
            {
                try
                {
                    points.Add(PointByStationOffsetElevation(s, offset, elevation, false));
                    //cs = this.CoordinateSystemByStation(s);
                    //points.Add(p.Transform(cs) as Point);
                }
                catch (Exception ex)
                {
                    Utils.Log($"EXCEPTION: {ex.Message}\n{ex.StackTrace}");
                }
            }

            points = Point.PruneDuplicates(points).ToList();

            //Utils.Log(string.Format("Station List created", ""));

            PolyCurve res = null;

            if (points.Count > 1)
            {
                res = PolyCurve.ByPoints(points); 
            }

            foreach (var item in points)
            {
                if (item != null)
                {
                    item.Dispose();
                }
            }

            points.Clear();

            if (cs != null)
            {
                cs.Dispose();
            }

            p.Dispose();

            Utils.LogMethodEnd(this);

            return res;
        }

        /// <summary>
        /// Points by chord distance on the Featureline.
        /// </summary>
        /// <param name="curve">The curve to subdivide.</param>
        /// <param name="chord">The chord.</param>
        /// <returns></returns>
        public static IList<Point> PointsByChord(Curve curve, double chord)
        {
            Utils.Log($"Featureline.PointsByChord started...");

            if (chord <= 0)
            {
                Utils.Log($"ERROR: Featureline.PointsByChord {"Distance must be positive."}");

                throw new Exception("Distance must be positive.");
            }

            Solid s = null;
            Point p = null;
            Curve c = null;
            Point e = null;

            IList<Point> points = new List<Point>();
            double startParameter = curve.ParameterAtPoint(curve.StartPoint);
            double endParameter = curve.ParameterAtPoint(curve.EndPoint);

            int n = Convert.ToInt32(Math.Ceiling(curve.Length / chord));

            Utils.Log($"Number of points: {n}");

            bool found = false;

            p = curve.StartPoint;

            points.Add(p);

            double par = startParameter;

            for (int i = 0; i < n; ++i)
            {
                Utils.Log($"Processing Point: {i}");

                s = Sphere.ByCenterPointRadius(p, chord);

                if (s != null)
                {
                    Utils.Log($"Sphere...");

                    var intersection = s.Intersect(curve);

                    Utils.Log($"Looking for intersections...");

                    if (intersection != null)
                    {
                        foreach (Geometry g in intersection)
                        {
                            if (g is Curve)
                            {
                                Utils.Log($"Curve found...");

                                try
                                {
                                    c = g as Curve;
                                    e = c.EndPoint;

                                    double parameter = curve.ParameterAtPoint(e);

                                    if (parameter >= endParameter)
                                    {
                                        p = curve.EndPoint;
                                        found = true;
                                        break;
                                    }

                                    if (parameter > par)
                                    {
                                        p = Point.ByCoordinates(e.X, e.Y, e.Z);
                                        par = parameter;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Utils.Log($"ERROR: {ex.Message}");
                                    throw ex;
                                }
                            }
                        }
                    }

                    foreach (var item in intersection)
                    {
                        if (item != null)
                        {
                            item.Dispose();
                        }
                    }
                }

                if (null != p)
                {
                    Utils.Log($"Parameter: {par}");

                    points.Add(p);
                }
                else
                {
                    Utils.Log($"ERROR: {"Point is null"}");
                    break;
                }

                if (found)
                {
                    break;
                }
            }

            s.Dispose();
            c.Dispose();
            e.Dispose();

            Utils.Log($"Featureline.PointsByChord completed.");

            return points;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Featureline({Curve}, Code = {Code}, Side = {_side}, Start = {Math.Round(Start, 3)}, End = {Math.Round(End, 3)})";
        }

        #endregion
    }
}
