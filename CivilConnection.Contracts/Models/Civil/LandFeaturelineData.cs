using CivilConnection.Contracts.Models.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Models.Civil
{
    public class LandFeaturelineData
    {
        public string Handle { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public IList<PointData> Points { get; set; } = new List<PointData>();
    }
}
