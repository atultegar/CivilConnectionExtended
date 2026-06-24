// Copyright (c) 2019 Autodesk, Inc.
// Copyright (c) 2026 Atul Tegar
//
// Original Authors: paolo.serra@autodesk.com, atul.tegar@gmail.com
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
using CivilConnection.Interop.Wrappers;
using CivilConnection.Interop.Services;
using CivilConnection.Converters;
using CivilConnection.Services;

namespace CivilConnection
{
    /// <summary>
    /// Civil 3D Surface object
    /// </summary>
    public class CivilSurface
    {
        #region INTERNAL

        internal readonly CivilSurfaceWrapper _surface;

        #endregion

        #region SERVICES

        private readonly CivilSurfaceService _surfaceService;
        private readonly LandXmlService _landXmlService;
        

        #endregion

        /// <summary>
        /// Gets the internal element
        /// </summary>
        internal CivilSurfaceWrapper InternalElement => _surface;

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the name of surface
        /// </summary>
        /// <value>
        /// Name of surface
        /// </value>
        public string Name => _surface.Name;
        /// <summary>
        /// Gets the surface type
        /// </summary>
        public string SurfaceType => _surface.Type;
        #endregion

        #region INTERNAL CONSTRUCTOR       

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="surface">the internal AeccSurface</param>
        internal CivilSurface(CivilSurfaceWrapper surface)
        {
            _surface = surface ?? throw new ArgumentNullException(nameof(surface));
            _surfaceService = new CivilSurfaceService();
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Get elevation of surface at point
        /// </summary>
        /// <param name="point">The point to process</param>
        /// <returns>
        /// The List of Elevations
        /// </returns>
        public double GetElevationAtPoint(Point point)
        {
            Utils.LogMethodStart(this);

            try
            {
                double elevation = System.Math.Round(_surfaceService.FindElevationAtXY(_surface, point.X, point.Y), 5);

                Utils.LogMethodEnd(this);

                return elevation;
            }

            catch (Exception ex)
            {
                Utils.Log($"ERROR: CivilSurface.GetElevationAtPoint: The surface elevation at selected point is not found, {ex.Message}");

                return double.NaN;
            }
        }        

        /// <summary>
        /// Gets all surface points along line
        /// </summary>
        /// <param name="line">The line to process</param>
        /// <returns>
        /// The List of Points
        /// </returns>
        public List<Point> GetPointsAlongLine(Line line)
        {
            Utils.LogMethodStart(this);

            var lineData = GeometryConverter.ToLineData(line);

            var points = _surfaceService.SampleElevations(_surface, lineData);

            Utils.LogMethodEnd(this);

            return points.Select(x => GeometryConverter.ToProtoPoint(x)).ToList();
        }

        /// <summary>
        /// Get all surface points
        /// </summary>
        /// <returns>
        /// The List of Points
        /// </returns>
        public List<Point> GetSurfacePoints()
        {
            Utils.LogMethodStart(this);

            var points = _surfaceService.GetPoints(_surface);

            Utils.LogMethodEnd(this);

            return points.Select(x => GeometryConverter.ToProtoPoint(x)).ToList();
        }

        /// <summary>
        /// Gets all surface points between lower and upper limit points.
        /// </summary>
        /// <param name="lowerLeftPoint">The minmum point.</param>
        /// <param name="upperRightPoint">The maximum pont.</param>
        /// <returns>
        /// The List of Points
        /// </returns>
        private List<Point> GetPointsBetweenLowerUpperLimits(Point lowerLeftPoint, Point upperRightPoint)  // author: Atul Tegar
        {
            Utils.LogMethodStart(this);

            List<Point> points = new List<Point>();

            double minX = lowerLeftPoint.X;
            double minY = lowerLeftPoint.Y;

            double maxX = upperRightPoint.X;
            double maxY = upperRightPoint.Y;

            List<Point> surfacePoints = GetSurfacePoints();

            foreach (Point point in surfacePoints)
            {
                if (point.X >= minX && point.Y >= minY && point.X <= maxX && point.Y <= maxY)
                {
                    points.Add(point);
                }
            }

            Utils.LogMethodEnd(this);

            return points;
        }

        /// <summary>
        /// Gets all surface points in the BoundingBox.
        /// </summary>
        /// <param name="boundingBox">The BoundingBox used for the containment test.</param>
        /// <returns>
        /// The List of Points
        /// </returns>
        public List<Point> GetPointsInBoundingBox(BoundingBox boundingBox)
        {
            Utils.LogMethodStart(this);

            List<Point> points = GetSurfacePoints().Where(p => boundingBox.Contains(p)).ToList();

            Utils.LogMethodEnd(this);

            return points;
        }

        /// <summary>
        /// Get surface points inside a closed boundary
        /// </summary>
        /// <param name="boundary">A closed curve</param>
        /// <param name="tolerance">A value between 0 and 1 to define the precision of the tessellation along non-straight curves.</param>
        /// <returns>
        /// The List of Points
        /// </returns>
        public List<Point> GetPointsInBoundary(Curve boundary, double tolerance = 0.1)
        {
            Utils.LogMethodStart(this);

            var pointsInside = new List<Point>();

            if (!boundary.IsClosed)
            {
                string message = "The Curve provided is not closed.";

                Utils.Log($"ERROR: CivilSurface.GetPointsInBoundary {message}");

                throw new Exception(message);
            }

            Plane xy = Plane.XY();

            Curve boundaryXY = null;

            Surface patchSurface = null;

            Polygon polygon = null;

            try
            {
                boundaryXY = boundary.PullOntoPlane(xy);

                try
                {
                    patchSurface = Surface.ByPatch(boundaryXY);
                }
                catch
                {

                }

                if (patchSurface != null)
                {
                    pointsInside = GetSurfacePoints().Where(p => Point.ByCoordinates(p.X, p.Y).DoesIntersect(patchSurface)).ToList();

                    Utils.LogMethodEnd(this);
                }

                polygon = CreatePolygonFromBoundary(boundary, tolerance);

                if (polygon == null)
                {
                    Utils.LogMethodEnd(this);
                    return pointsInside;
                }

                pointsInside = GetSurfacePoints().Where(p => polygon.ContainmentTest(Point.ByCoordinates(p.X, p.Y))).ToList();

                Utils.LogMethodEnd(this);

                return pointsInside;
            }
            finally 
            {
                polygon?.Dispose();
                patchSurface?.Dispose();
                boundaryXY?.Dispose();
                xy?.Dispose();
            }
        }        

        /// <summary>
        /// Gets all the triangle surfaces in a CivilSurface
        /// </summary>
        /// <returns></returns>
        public IList<Surface> GetTrianglesSurfaces()
        {
            Utils.LogMethodStart(this);

            var xmlPath = _surfaceService.ExportSurfaceToXml(_surface);

            var triangles = _landXmlService.ReadTriangles(xmlPath, Name);

            var outputSurfaces = triangles.Select(x => GeometryConverter.ToSurface(x)).ToList();

            Utils.LogMethodEnd(this);

            return outputSurfaces;
        }

        /// <summary>
        /// Gets all the triangle surfaces in a CivilSurface via LandXML
        /// </summary>
        /// <param name="landXMLpath">The path to the LandXML that contains the surface export</param>
        /// <param name="onlyVisible">Processes only the visible faces</param>
        /// <returns></returns>
        public IList<Surface> GetTrianglesSurfaces(string landXMLpath, bool onlyVisible = true)
        {
            Utils.LogMethodStart(this);

            var triangles = _landXmlService.ReadTriangles(landXMLpath, Name);

            var outputSurfaces = triangles.Select(x => GeometryConverter.ToSurface(x)).ToList();

            Utils.LogMethodEnd(this);

            return outputSurfaces;
        }

        /// <summary>
        /// Gets all the triangle faces in a CivilSurface via LandXML
        /// </summary>
        /// <param name="landXMLpath">The path to the LandXML that contains the surface export</param>
        /// <param name="onlyVisible">Processes only the visible faces</param>
        /// <returns></returns>
        [MultiReturn(new string[]{"Points", "Faces"})]
        public Dictionary<string, object> GetFacesSurfaces(string landXMLpath, bool onlyVisible = true)
        {
            Utils.LogMethodStart(this);

            try
            {
                var mesh = _landXmlService.ReadFaces(landXMLpath, Name, onlyVisible);

                var points = mesh.Points
                    .OrderBy(x => x.Key)
                    .Select(x => GeometryConverter.ToProtoPoint(x.Value))
                    .ToList();

                var faces = mesh.Faces
                    .Select(x => x.ToList())
                    .ToList();

                return new Dictionary<string, object>
                {
                    { "Points", points },
                    { "Faces", faces }
                };
            }
            finally
            {
                Utils.LogMethodEnd(this);
            }
        }

        /// <summary>
        /// Joins the surfaces recursively into a Polysurface
        /// </summary>
        /// <param name="surfaces">The surfaces to join</param>
        /// <param name="limit">The amount of surfaces to join with recursion</param>
        /// <returns></returns>
        public static IList<Surface> JoinSurfaces(IList<Surface> surfaces, int limit = 100)
        {
            Utils.Log($"CivilSurface.Joinsurface started...");

            var output = DynamoGeometryService.JoinSurfaces(surfaces, limit);

            Utils.Log($"CivilSurface.Joinsurface completed...");

            return output;
        }

        /// <summary>
        /// Get intersection point between the line with start point and direction on the surface 
        /// </summary>
        /// <param name="point">The point to process</param>
        /// <param name="vector">The direction vector</param>
        /// <returns>
        /// The intersection point
        /// </returns>
        public Point GetIntersectionPoint(Point point, Vector vector) // author: Atul Tegar
        {
            Utils.LogMethodStart(this);

            var origin = GeometryConverter.ToPointData(point);

            var direction = GeometryConverter.ToVectorData(vector);

            try
            {
                var pointData = _surfaceService.GetIntersectionPoint(_surface, origin, direction);

                Utils.LogMethodEnd(this);

                return GeometryConverter.ToProtoPoint(pointData);
            }
            catch (Exception ex)
            {
                var message = $"ERROR: No intersection with vector and surface found, {ex.Message}";

                Utils.Log(message);

                throw new Exception(message);
            }            
        }


        /// <summary>
        /// Public textual representation in the Dynamo node preview.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"CivilSurface(Name = {Name})";
        }

        #endregion

        #region PRIVATE METHODS

        private Polygon CreatePolygonFromBoundary(Curve boundary, double tolerance)
        {
            if (tolerance <= 0 || tolerance > 1)
                tolerance = 0.1;

            if (boundary is Polygon polygon)
                return polygon;

            var points = new List<Point>();

            if (boundary is PolyCurve polyCurve)
            {
                for (int i = 0; i < polyCurve.NumberOfCurves; i++)
                {
                    var curve = polyCurve.CurveAtIndex(i);

                    if (curve is Line)
                    {
                        points.Add(curve.StartPoint);
                    }
                    else
                    {
                        for (double t = 0; t <= 1;  t += tolerance)
                        {
                            points.Add(curve.PointAtParameter(t));
                        }
                    }
                }
            }
            else if (boundary is Circle)
            {
                for (double t = 0; t <= 1; t += tolerance)
                {
                    points.Add(boundary.PointAtParameter(t));
                }
            }
            else if (boundary is Rectangle rectangle)
            {
                points.AddRange(
                    rectangle.Curves()
                        .Select(x => x.StartPoint));
            }

            if (!points.Any())
                return null;

            return Polygon.ByPoints(
                points.Select(p => 
                    Point.ByCoordinates(
                        p.X,
                        p.Y)));            
        }

        #endregion
    }
}