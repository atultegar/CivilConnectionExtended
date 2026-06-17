using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Models.Civil
{
    public abstract class AppliedSubassemblyGeometryData
    {
        public string Name { get; set; }
        public double Station { get; set; }
        public List<string> Codes { get; set; }
    }
}
