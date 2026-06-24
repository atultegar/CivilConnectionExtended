using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Models.Civil
{
    public class SampleLineGroupData
    {
        public string Name { get; set; }
        public IList<SampleLineSectionData> Sections { get; set; } = new List<SampleLineSectionData>();
    }
}
