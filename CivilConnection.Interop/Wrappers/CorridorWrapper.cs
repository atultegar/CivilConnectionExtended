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

        public DocumentWrapper Document => new DocumentWrapper(ComObject.Document);

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

        public IEnumerable<CorridorSurfaceWrapper> CorridorSurfaces
        {
            get
            {
                foreach (dynamic surface in ComObject.CorridorSurfaces)
                {
                    yield return new CorridorSurfaceWrapper(surface);
                }
            }
        }

        public void Rebuild()
        {
            ComObject.Rebuild();
        }

        public bool IsFeaturelinesXMLExported { get; set; } = false;

        public override string ToString()
        {
            return $"Corridor(Name = {Name})";
        }
    }
}
