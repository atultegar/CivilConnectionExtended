using System.Collections.Generic;
using System.Linq;

namespace CivilConnection.Interop.Runtime
{
    public class CivilRuntime
    {
        public List<CivilHost> Hosts { get; } = new();

        public CivilHost? ActiveHost => Hosts.FirstOrDefault();
    }
}
