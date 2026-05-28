using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Models
{
    public class CivilSessionData
    {
        public string ActiveDocumentName { get; set; } = "";

        public List<CivilDocumentData> Documents { get; set; } = new();

        public string CivilVersion { get; set; } = "";

        public bool IsConnected { get; set; }
    }
}
