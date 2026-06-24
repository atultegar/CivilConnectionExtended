using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Utilities
{
    public static class Validator
    {
        public static void ValidateDocument(dynamic document)
        {
            if (document == null)
            {
                throw new Exception("Civil document is null");
            }
        }

        public static void ValidateAlignment(dynamic alignment)
        {
            if (alignment == null)
            {
                throw new Exception("Alignment is null");
            }
        }
    }
}
