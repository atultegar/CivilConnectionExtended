using Autodesk.DesignScript.Geometry;
using CivilConnection.Contracts.Models.Civil;
using System;
using System.Linq;

namespace CivilConnection.Converters
{
    public static class AlignmentConverter
    {
        public static Curve ToDynamo(AlignmentEntityData data)
        {
            switch (data)
            {
                case AlignmentLineData line:
                    return Line.ByStartPointEndPoint(
                        GeometryConverter.ToProtoPoint(line.StartPoint),
                        GeometryConverter.ToProtoPoint(line.EndPoint));

                case AlignmentArcData arc:
                    return arc.Clockwise
                        ? Arc.ByCenterPointStartPointEndPoint(
                            GeometryConverter.ToProtoPoint(arc.Center),
                            GeometryConverter.ToProtoPoint(arc.EndPoint),
                            GeometryConverter.ToProtoPoint(arc.StartPoint))
                        : Arc.ByCenterPointStartPointEndPoint(
                            GeometryConverter.ToProtoPoint(arc.Center),
                            GeometryConverter.ToProtoPoint(arc.StartPoint),
                            GeometryConverter.ToProtoPoint(arc.EndPoint));

                case AlignmentSpiralData spiral:
                    return NurbsCurve.ByPoints(
                        spiral.Points
                            .Select(GeometryConverter.ToProtoPoint)
                            .ToList());

                default:
                    throw new NotSupportedException(data.GetType().Name);
            }
        }
    }
}
