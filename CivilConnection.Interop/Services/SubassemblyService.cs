using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection.Interop.Services
{
    public class SubassemblyService
    {
        public IList<ParameterWrapper> GetParameters(SubassemblyWrapper subassembly)
        {
            if (subassembly == null)
                throw new ArgumentNullException(nameof(subassembly));

            return subassembly.Parameters.ToList();
        }
        public ParameterWrapper GetParameter(SubassemblyWrapper subassembly, string name)
        {
            return GetParameters(subassembly)
                .FirstOrDefault(x => 
                    x.DisplayName.Equals(
                        name,
                        StringComparison.OrdinalIgnoreCase));
        }

        public void SetParameter(SubassemblyWrapper subassembly, string name, object value)
        {
            var parameter = GetParameter(subassembly, name);

            if (parameter == null)
                throw new Exception($"Parameter '{name}' not found.");

            parameter.Value = value;
        }

        public void Rebuild(SubassemblyWrapper subassembly)
        {
            if (subassembly?.Corridor == null)
                return;

            subassembly.Corridor.Rebuild();
        }

        public void SetParameter(SubassemblyWrapper subassembly, string name, object value, bool rebuild)
        {
            SetParameter(subassembly, name, value);

            if (rebuild)
            {
                Rebuild(subassembly);
            }
        }
    }
}
