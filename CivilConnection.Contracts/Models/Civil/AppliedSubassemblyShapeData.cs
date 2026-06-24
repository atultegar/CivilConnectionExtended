using CivilConnection.Contracts.Models.Geometry;
using System.Collections.Generic;

namespace CivilConnection.Contracts.Models.Civil
{
    public class AppliedSubassemblyShapeData : AppliedSubassemblyGeometryData
    {       
        public List<PointData> BoundaryPoints { get; set; } = new List<PointData>();
    }
}
