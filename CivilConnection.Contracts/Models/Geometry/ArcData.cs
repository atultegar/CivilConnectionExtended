namespace CivilConnection.Contracts.Models.Geometry
{
    /// <summary>
    /// Represents the geometric data for a circular arc, including its center point, radius, and angular span.
    /// </summary>
    public class ArcData : IGeometryData
    {
        public PointData Center { get; set; }
        public double Radius { get; set; }
        public double StartAngleRadians { get; set; }
        public double EndAngleRadians { get; set; }
        public double[,] Transform { get; set; }
    }
}
