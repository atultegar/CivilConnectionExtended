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
using CivilConnection.Contracts.Models.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection.Converters
{
    internal static class GeometryConverter
    {
        public static IGeometryData Convert(Curve curve)
        {
            switch (curve)
            {
                case Arc arc:
                    return ToArcData(arc);

                case Circle circle:
                    return ToCircleData(circle);

                case Line line:
                    return ToLineData(line);

                case PolyCurve polyCurve:
                    return ToPolylineData(polyCurve);

                default:
                    throw new NotSupportedException($"Unsupported geometry type {curve.GetType().Name}");
            }
        }

        public static PointData ToPointData(Point point)
        {
            return new PointData
            {
                X = point.X,
                Y = point.Y,
                Z = point.Z
            };
        }

        public static Point ToProtoPoint(PointData point)
        {
            return Point.ByCoordinates(point.X, point.Y, point.Z);
        }

        public static VectorData ToVectorData(Vector vector)
        {
            return new VectorData
            {
                X = vector.X,
                Y = vector.Y,
                Z = vector.Z
            };
        }

        public static LineData ToLineData(Line line)
        {
            return new LineData
            {
                StartPoint = ToPointData(line.StartPoint),
                EndPoint = ToPointData(line.EndPoint)                
            };
        }

        public static PolylineData ToPolylineData(IList<Point> points)
        {
            var vertices = new List<double>();

            foreach (Point p in points)
            {
                vertices.Add(p.X);
                vertices.Add(p.Y);
                vertices.Add(p.Z);
            }

            return new PolylineData
            {
                Vertices = vertices.ToArray(),
                IsClosed = false
            };
        }

        public static PolylineData ToPolylineData(PolyCurve polyCurve)
        {
            var vertices = new List<double>();

            for (int i = 0; i < polyCurve.NumberOfCurves; i++)
            {
                var segment = polyCurve.CurveAtIndex(i);

                if (i == 0)
                {
                    vertices.Add(segment.StartPoint.X);
                    vertices.Add(segment.StartPoint.Y);
                    vertices.Add(segment.StartPoint.Z);
                }

                vertices.Add(segment.EndPoint.X);
                vertices.Add(segment.EndPoint.Y);
                vertices.Add(segment.EndPoint.Z);                
            }

            return new PolylineData
            {
                Vertices = vertices.ToArray(),
                IsClosed = polyCurve.IsClosed
            };
        }

        public static PolylineData ToPolylineData(IList<Curve> curves)
        {
            var polyCurve = PolyCurve.ByJoinedCurves(curves, 0.001, false, 0);

            return ToPolylineData(polyCurve);
        }

        public static ArcData ToArcData(Arc arc)
        {
            Point center = arc.CenterPoint;

            Plane curvePlane = Plane.ByOriginNormal(center, arc.Normal);

            // (1) Create horizontal Dynamo Arc from Arc input
            CoordinateSystem curveCSInverse = curvePlane.ToCoordinateSystem().Inverse();

            Arc ha = arc.Transform(curveCSInverse) as Arc;  // this arc is the transformed copy of the input in the origin

            // Do not trust the StartAngle and SweepAngle properties..
            Vector cs = Vector.ByTwoPoints(ha.CenterPoint, ha.StartPoint);
            Vector ce = Vector.ByTwoPoints(ha.CenterPoint, ha.EndPoint);
            double start = Vector.XAxis().AngleAboutAxis(cs, Vector.ZAxis());
            double end = Vector.XAxis().AngleAboutAxis(ce, Vector.ZAxis());

            curveCSInverse = curveCSInverse.Inverse();

            double[,] matrix =
            {
                {curveCSInverse.XAxis.X / curveCSInverse.XScaleFactor, curveCSInverse.YAxis.X / curveCSInverse.XScaleFactor, curveCSInverse.ZAxis.X / curveCSInverse.XScaleFactor, curveCSInverse.Origin.X},
                {curveCSInverse.XAxis.Y / curveCSInverse.YScaleFactor, curveCSInverse.YAxis.Y / curveCSInverse.YScaleFactor, curveCSInverse.ZAxis.Y / curveCSInverse.YScaleFactor, curveCSInverse.Origin.Y},
                {curveCSInverse.XAxis.Z / curveCSInverse.ZScaleFactor, curveCSInverse.YAxis.Z / curveCSInverse.ZScaleFactor, curveCSInverse.ZAxis.Z / curveCSInverse.ZScaleFactor, curveCSInverse.Origin.Z},
                {0, 0, 0, 1}
            };

            double radius = arc.Radius;

            ArcData arcData = new ArcData
            {
                Center = ToPointData(ha.CenterPoint),
                Radius = arc.Radius,
                StartAngleRadians = Utils.DegToRad(start),
                EndAngleRadians = Utils.DegToRad(end),
                Transform = matrix
            };

            Utils.DisposeObjects(
                center,
                curvePlane,
                curveCSInverse,
                ha,
                cs,
                ce);

            return arcData;
        }

        public static CircleData ToCircleData(Circle circle)
        {
            var data = new CircleData
            {
                Center = ToPointData(circle.CenterPoint),
                Radius = circle.Radius
            };

            if (Math.Abs(Math.Abs(circle.Normal.Dot(Vector.ZAxis())) -1) > 0.001)
            {
                data.Transform = TransformConverter.CreateCircleTransform(circle);
            }

            return data;
        }

        public static Surface ToSurface(TriangleData triangleData)
        {
            var points = new List<Point>();

            points.Add(ToProtoPoint(triangleData.A));
            points.Add(ToProtoPoint(triangleData.B));
            points.Add(ToProtoPoint(triangleData.C));

            return Surface.ByPerimeterPoints(points);
        }

        private static PolylineData CreateFromPoints(IEnumerable<Point> points, bool closed)
        {
            var vertices = points.SelectMany(p => new[]
            {
                p.X,
                p.Y,
                p.Z
            })
            .ToArray();

            return new PolylineData
            {
                Vertices = vertices,
                IsClosed = closed
            };
        }
    }
}
