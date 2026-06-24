using CivilConnection.Interop.Wrappers.Base;
using System.Collections.Generic;

namespace CivilConnection.Interop.Wrappers
{
    public class SampleLineWrapper : ComWrapper
    {
        public SampleLineWrapper(object obj) : base(obj)
        {
        }

        public IEnumerable<SectionWrapper> Sections
        {
            get
            {
                if (ComObject.Sections == null)
                    yield break;

                foreach (var section in ComObject.Sections)
                {
                    yield return new SectionWrapper(section);
                }
            }
        }
    }
}
