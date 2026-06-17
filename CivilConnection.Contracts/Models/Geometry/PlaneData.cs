namespace CivilConnection.Contracts.Models.Geometry
{
    public class PlaneData
    {
        public PointData Origin { get; set; }
        public VectorData XAxis { get; set; }
        public VectorData YAxis { get; set; }
    }
}
