namespace CivilConnection.Contracts.Models.Civil
{
    public class FeaturelinePointData
    {
        public double Station { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public bool IsBreak { get; set; }
        public int RegionIndex { get; set; }
    }
}
