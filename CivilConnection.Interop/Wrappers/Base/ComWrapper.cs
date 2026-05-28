using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers.Base
{
    public abstract class ComWrapper
    {
        protected dynamic InternalObject;

        protected ComWrapper(object obj)
        {
            InternalObject = obj;
        }
    }
}
