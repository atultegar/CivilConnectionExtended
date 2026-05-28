using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Converters
{
    public static class AlignmentConverter
    {
        public static AlignmentData Convert(AlignmentWrapper wrapper)
        {
            return new AlignmentData
            {
                Name = wrapper.Name,
                Length = wrapper.Length
            };
        }
    }
}
