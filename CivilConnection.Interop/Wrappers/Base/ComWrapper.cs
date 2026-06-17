using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers.Base
{
    public abstract class ComWrapper
    {
        public dynamic ComObject { get; }

        protected ComWrapper(object obj)
        {
            ComObject = obj ?? throw new ArgumentNullException(nameof(obj));
        }

        public string ObjectName { get; protected set; }

        public string Handle
        {
            get
            {
                try
                {
                    return (string)ComObject.Handle;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public long? ObjectId
        {
            get
            {
                try
                {
                    return Convert.ToInt64(ComObject.ObjectId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public override string ToString()
        {
            return $"{GetType().Name}(Handle={Handle})";
        }
    }
}
