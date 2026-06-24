using CivilConnection.Interop.Wrappers.Base;
using System.Collections.Generic;

namespace CivilConnection.Interop.Wrappers
{
    public class AppliedAssemblyWrapper : ComWrapper
    {
        public AppliedAssemblyWrapper(object appliedAssembly) : base(appliedAssembly)
        {
        }

        public IEnumerable<AppliedSubassemblyWrapper> AppliedSubassemblies
        {
            get
            {
                foreach (dynamic appliedSubassembly in AppliedSubassemblies)
                {
                    yield return new AppliedSubassemblyWrapper(appliedSubassembly);
                }
            }
        }
    }
}
