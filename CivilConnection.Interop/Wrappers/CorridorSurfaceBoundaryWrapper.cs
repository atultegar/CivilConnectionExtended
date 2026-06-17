using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class CorridorSurfaceBoundaryWrapper : ComWrapper
    {
        public CorridorSurfaceBoundaryWrapper(object obj) : base(obj)
        {
        }

        public string Name => (string)ComObject.Name;

        public CorridorWrapper Corridor => new CorridorWrapper(ComObject.Corridor);

        public IEnumerable<PointData> GetPolygonPoints()
        {
            foreach (dynamic point in ComObject.GetPolygonPoints())
            {
                yield return new PointData
                {
                    X = point[0],
                    Y = point[1],
                    Z = point[2]
                };
            }
        }
    }
}
