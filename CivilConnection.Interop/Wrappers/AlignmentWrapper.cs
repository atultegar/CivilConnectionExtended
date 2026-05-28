using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class AlignmentWrapper : ComWrapper
    {
        public AlignmentWrapper(object alignment) : base(alignment)
        {            
        }

        public string Name => InternalObject.Name;

        public double Length => InternalObject.Length;
    }
}
