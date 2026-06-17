using CivilConnection.Contracts.Models.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Models.Civil
{
    public class AppliedSubassemblyLinkData : AppliedSubassemblyGeometryData
    {        
        public List<PointData> Points { get; set; } = new List<PointData>();
    }
}
