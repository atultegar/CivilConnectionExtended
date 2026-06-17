using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Models.Civil
{
    public class AlignmentGeometryData
    {
        public List<AlignmentEntityData> Entities { get; set; } = new List<AlignmentEntityData>();
    }
}
