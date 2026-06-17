using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class CorridorSurfaceWrapper : ComWrapper
    {
        public CorridorSurfaceWrapper(object cooridorSurface) : base(cooridorSurface)
        {
        }

        public string Name => (string)ComObject.Name;

        public IEnumerable<CorridorSurfaceBoundaryWrapper> Boundaries
        {
            get
            {
                foreach (dynamic boundary in ComObject.Boundaries)
                {
                    yield return new CorridorSurfaceBoundaryWrapper(boundary);
                }
            }
        }

        public IEnumerable<CorridorSurfaceMaskWrapper> Masks
        {
            get
            {
                foreach (dynamic mask in ComObject.Masks)
                {
                    yield return new CorridorSurfaceMaskWrapper(mask);
                }
            }
        }
    }
}
