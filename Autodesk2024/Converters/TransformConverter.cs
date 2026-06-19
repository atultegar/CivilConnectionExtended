using Autodesk.DesignScript.Geometry;

namespace CivilConnection.Converters
{
    internal static class TransformConverter
    {
        public static double[,] CreateCircleTransform(Circle circle)
        {
            var center = circle.CenterPoint;

            var plane = Plane.ByOriginNormal(center, circle.Normal);

            var cs = plane.ToCoordinateSystem();

            return ToMatrix(cs);
        }

        private static double[,] ToMatrix(CoordinateSystem cs)
        {
            return new double[,]
            {
                {
                    cs.XAxis.X,
                    cs.YAxis.X,
                    cs.ZAxis.X,
                    cs.Origin.X
                },
                {
                    cs.XAxis.Y,
                    cs.YAxis.Y,
                    cs.ZAxis.Y,
                    cs.Origin.Y
                },
                {
                    cs.XAxis.Z,
                    cs.YAxis.Z,
                    cs.ZAxis.Z,
                    cs.Origin.Z
                },
                {
                    0,0,0,1
                }
            };
        }
    }
}
