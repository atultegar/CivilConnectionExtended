using CivilConnection.Interop.Wrappers.Base;
using System.Collections.Generic;

namespace CivilConnection.Interop.Wrappers
{
    public class SampleLineGroupWrapper : ComWrapper
    {
        public SampleLineGroupWrapper(object obj) : base(obj)
        {
        }

        public string Name => (string)ComObject.Name;

        public IEnumerable<SampleLineWrapper> SampleLines
        {
            get
            {
                if (ComObject.SampleLines == null)
                    yield break;

                foreach (var sampleLine in ComObject.SampleLines)
                {
                    yield return new SampleLineWrapper(sampleLine);
                }
            }
        }
    }
}
