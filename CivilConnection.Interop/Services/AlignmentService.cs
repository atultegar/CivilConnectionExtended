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
    public class AlignmentService
    {
        public List<AlignmentData> GetAll(string? version = null)
        {
            var context = CivilContext.Create(version);

            var output = new List<AlignmentData>();

            dynamic document = context.Host.Application.ActiveDocument;

            dynamic alignments = document.AlignmentsSiteless;

            foreach (dynamic alignment in alignments)
            {
                var wrapper = new AlignmentWrapper(alignment);

                output.Add(AlignmentConverter.Convert(wrapper));
            }

            return output;
        }

        public IList<dynamic> GetAlignments(dynamic document)
        {
            var output = new List<dynamic>();
            
            foreach (var alignment in document.AlignmentsSiteless)
            {                
                output.Add(alignment);
            }

            return output;
        }
    }
}
