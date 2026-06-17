using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class ModelSpaceWrapper : ComWrapper
    {
        public ModelSpaceWrapper(object obj) : base(obj)
        {
        }

        public int Count => (int)ComObject.Count;

        public dynamic Item(int i)
        {
            return ComObject.Item(i);
        }
    }
}
