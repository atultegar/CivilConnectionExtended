using CivilConnection.Interop.Wrappers.Base;
using System.Collections.Generic;

namespace CivilConnection.Interop.Wrappers
{
    public class SubassemblyWrapper : ComWrapper
    {
        public SubassemblyWrapper(object subassembly) : base(subassembly)
        {
        }

        public string Name => (string)ComObject.Name;

        public CorridorWrapper Corridor => new CorridorWrapper(ComObject.Corridor);

        public IEnumerable<ParameterWrapper> Parameters
        {
            get
            {
                foreach (dynamic p in ComObject.ParamsBool)
                    yield return new ParameterWrapper(p);

                foreach (dynamic p in ComObject.ParamsDouble)
                    yield return new ParameterWrapper(p);

                foreach (dynamic p in ComObject.ParamsLong)
                    yield return new ParameterWrapper(p);

                foreach (dynamic p in ComObject.ParamsString)
                    yield return new ParameterWrapper(p);
            }
        }
        
    }
}
