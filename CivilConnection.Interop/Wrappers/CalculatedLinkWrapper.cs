using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class CalculatedLinkWrapper : ComWrapper
    {
        public CalculatedLinkWrapper(object calculatedLink) : base(calculatedLink)
        {
        }

        public CorridorWrapper Corridor => new CorridorWrapper(ComObject.Corridor);

        public IEnumerable<CalculatedPointWrapper> CalculatedPoints
        {
            get
            {
                foreach (var point in ComObject.CalculatedPoints)
                {
                    yield return new CalculatedPointWrapper(point);
                }
            }
        }
    }
}
