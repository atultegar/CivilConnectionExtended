namespace CivilConnection.Contracts.Models.Geometry
{
    public class PolylineData : IGeometryData
    {
        public double[] Vertices { get; set; }
        public bool IsClosed { get; set; }
    }
}
