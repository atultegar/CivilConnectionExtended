using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Services
{
    public class SurfaceService
    {
        public IList<dynamic> GetSurfaces(dynamic document)
        {
            var output = new List<dynamic>();

            foreach (var surface in document.Surfaces)
            {
                output.Add(surface);
            }

            return output;
        }
    }
}
