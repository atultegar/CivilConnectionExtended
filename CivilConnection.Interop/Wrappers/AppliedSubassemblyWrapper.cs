using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class AppliedSubassemblyWrapper : ComWrapper
    {
        public AppliedSubassemblyWrapper(object asa) : base(asa)
        {
        }

        public SubassemblyWrapper SubassemblyDbEntity => new SubassemblyWrapper(ComObject.SubassemblyDbEntity);

        public CorridorWrapper Corridor => new CorridorWrapper(ComObject.Corridor);

    }
}
