using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.ApiExtractor.Models
{
    public class AttributeInfo
    {
        public string Name { get; set; } = string.Empty;

        public List<string> Arguments { get; set; } = [];
    }
}
