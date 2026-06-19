
using Autodesk.DesignScript.Geometry;
using CivilConnection.Contracts.Models.Civil;
using System.Linq;

namespace CivilConnection.Converters
{
    internal static class AppliedSubassemblyConverter
    {
        internal static AppliedSubassemblyShape ToDynamo(AppliedSubassemblyShapeData data)
        {
            var points = data.BoundaryPoints
                .Select(GeometryConverter.ToProtoPoint)
                .ToList();

            Surface surface = Surface.ByPatch(Polygon.ByPoints(points));

            return new AppliedSubassemblyShape(data.Name, surface, data.Codes, data.Station);
        }

        internal static AppliedSubassemblyLink ToDynamo(AppliedSubassemblyLinkData data)
        {
            var points = data.Points
                .Select(GeometryConverter.ToProtoPoint)
                .ToList();

            Curve curve = PolyCurve.ByPoints(points);

            return new AppliedSubassemblyLink(data.Name, curve, data.Codes, data.Station);
        }
    }
}
