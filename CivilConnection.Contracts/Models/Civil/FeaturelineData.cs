using System.Collections.Generic;

namespace CivilConnection.Contracts.Models.Civil
{
    public enum SideType
    {
        Left,
        Right
    }
    public class FeaturelineData
    {
        public int RegionIndex { get; set; }
        public string Code { get; set; }
        public double Side { get; set; }
        public List<FeaturelinePointData> Points { get; set; } = new List<FeaturelinePointData>();
    }
}
