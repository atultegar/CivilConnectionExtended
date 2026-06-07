using System.Collections.Generic;

namespace CivilConnection.Contracts.Models.Geometry
{
    public class SurfaceMeshData
    {
        public Dictionary<int, PointData> Points { get; set; } = new Dictionary<int, PointData>();

        public List<int[]> Faces { get; set; } = new List<int[]>();
    }
}
