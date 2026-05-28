using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class DocumentWrapper : ComWrapper
    {
        public DocumentWrapper(object document) : base(document)
        {
        }

        public string Name => InternalObject.Name;

        public string Path => InternalObject.Path;

        public string Version => InternalObject.Application.Name;
    }
}
