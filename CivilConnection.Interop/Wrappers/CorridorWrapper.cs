using CivilConnection.Interop.Wrappers.Base;
using System.Collections.Generic;

namespace CivilConnection.Interop.Wrappers
{
    public class CorridorWrapper : ComWrapper
    {
        public CorridorWrapper(object corridor) : base(corridor)
        {            
        }

        public string Name => (string)ComObject.Name;

        public string DisplayName => (string)ComObject.DisplayName;

        public IEnumerable<BaselineWrapper> Baselines
        {
            get
            {
                foreach (dynamic baseline in ComObject.Baselines)
                {
                    yield return new BaselineWrapper(baseline);
                }
            }
        }

        public void Rebuild()
        {
            ComObject.Rebuild();
        }

        public override string ToString()
        {
            return $"Corridor(Name = {Name})";
        }
    }
}
