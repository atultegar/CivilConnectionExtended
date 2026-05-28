using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.ApiExtractor.Models
{
    public class DiagnosticInfo
    {
        public string Severity { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;
    }
}
