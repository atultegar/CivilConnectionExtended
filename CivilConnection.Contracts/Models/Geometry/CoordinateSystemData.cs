namespace CivilConnection.Contracts.Models.Geometry
{
    public class CoordinateSystemData
    {
        public PointData Origin { get; set; }
        public VectorData XAxis { get; set; }
        public VectorData YAxis { get; set; }
        public VectorData ZAxis { get; set; }
        public double XScaleFactor { get; set; } = 1.0;
        public double YScaleFactor { get; set; } = 1.0;
        public double ZScaleFactor { get; set; } = 1.0;

        public double[,] ToTransformMatrix()
        {
            return new double[,]
            {
                { XAxis.X, YAxis.X, ZAxis.X, Origin.X },
                { XAxis.Y, YAxis.Y, ZAxis.Y, Origin.Y },
                { XAxis.Z, YAxis.Z, ZAxis.Z, Origin.Z },
                { 0, 0, 0, 1 }
            };
        }
    }

    
}
