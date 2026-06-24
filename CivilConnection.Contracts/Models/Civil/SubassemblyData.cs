using CivilConnection.Contracts.Models.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Models.Civil
{
    public class SubassemblyData
    {
        public string Code { get; set; }
        public double Station { get; set; }
        public List<PointData> BoundaryPoints { get; set; } = new List<PointData>();
    }
}
