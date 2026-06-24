using CivilConnection.Interop.Wrappers.Base;
using System.Collections.Generic;

namespace CivilConnection.Interop.Wrappers
{
    public class CalculatedShapeWrapper : ComWrapper
    {
        public CalculatedShapeWrapper(object calculatedShape) : base(calculatedShape)
        {
        }

        public CorridorWrapper Corridor => new CorridorWrapper(ComObject.Corridor);

        public IEnumerable<CalculatedLinkWrapper> CalculatedLinks
        {
            get
            {
                foreach (var link in ComObject.CalculatedLinks)
                {
                    yield return new CalculatedLinkWrapper(link);
                }
            }
        }


    }
}
