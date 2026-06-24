using CivilConnection.Interop.Wrappers.Base;
using System.Collections.Generic;

namespace CivilConnection.Interop.Wrappers
{
    public class AppliedSubassemblyWrapper : ComWrapper
    {
        public AppliedSubassemblyWrapper(object asa) : base(asa)
        {
        }

        public SubassemblyWrapper SubassemblyDbEntity => new SubassemblyWrapper(ComObject.SubassemblyDbEntity);

        public CorridorWrapper Corridor => new CorridorWrapper(ComObject.Corridor);

        public IEnumerable<CalculatedShapeWrapper> CalculatedShapes 
        {
            get
            {
                foreach (dynamic calculatedShape in ComObject.CalculatedShapes)
                {
                    yield return new CalculatedShapeWrapper(calculatedShape);
                }
            }
        }

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
