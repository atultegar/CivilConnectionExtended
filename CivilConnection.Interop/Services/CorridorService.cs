using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Interop.Context;
using CivilConnection.Interop.Converters;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Services
{
    public class CorridorService
    {
        public IList<dynamic> GetCorridors(dynamic document)
        {
            var output = new List<dynamic>();

            foreach (var corridor in document.Corridors)
            {
                output.Add(corridor);
            }

            return output;
        }
    }
}
