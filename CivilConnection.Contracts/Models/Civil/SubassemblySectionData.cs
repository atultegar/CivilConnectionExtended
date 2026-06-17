using CivilConnection.Contracts.Models.Geometry;
using System.Collections.Generic;

namespace CivilConnection.Contracts.Models.Civil
{
    public class SubassemblySectionData
    {
        public double Station { get; set; }
        public List<PointData> Points { get; set; } = new List<PointData>();
    }
}
