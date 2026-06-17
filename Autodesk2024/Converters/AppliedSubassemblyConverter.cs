
using Autodesk.DesignScript.Geometry;
using CivilConnection.Contracts.Models.Civil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Converters
{
    public static class AppliedSubassemblyConverter
    {
        public static AppliedSubassemblyShape ToDynamo(AppliedSubassemblyShapeData data)
        {
            var points = data.BoundaryPoints
                .Select(GeometryConverter.ToProtoPoint)
                .ToList();

            Surface surface = Surface.ByPatch(Polygon.ByPoints(points));

            return new AppliedSubassemblyShape(data.Name, surface, data.Codes, data.Station);
        }

        public static AppliedSubassemblyLink ToDynamo(AppliedSubassemblyLinkData data)
        {
            var points = data.Points
                .Select(GeometryConverter.ToProtoPoint)
                .ToList();

            Curve curve = PolyCurve.ByPoints(points);

            return new AppliedSubassemblyLink(data.Name, curve, data.Codes, data.Station);
        }
    }
}
