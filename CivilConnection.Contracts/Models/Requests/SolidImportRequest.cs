using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Models.Requests
{
    public class SolidImportRequest
    {
        public IList<string> ExistingHandles { get; set; } = new List<string>();
        public IList<string> SatFiles { get; set; } = new List<string>();
        public string Layer { get; set; }
        public bool ReplaceExisting { get; set; }
    }
}
