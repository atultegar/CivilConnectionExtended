using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Interop.Wrappers.Base;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class AlignmentWrapper : ComWrapper
    {
        public AlignmentWrapper(object alignment) : base(alignment)
        {            
        }

        public string Name => (string)ComObject.Name;

        public string DisplayName => (string)ComObject.DisplayName;

        public DocumentWrapper Document => new DocumentWrapper(ComObject.Document);

        public double Length => Convert.ToDouble(ComObject.Length);

        public double StartingStation => Convert.ToDouble(ComObject.StartingStation);

        public double EndingStation => Convert.ToDouble(ComObject.EndingStation);

        public override string ToString()
        {
            return $"Alignment (Name = {Name})";
        }

        public SOEData GetStationOffset(double x, double y)
        {
            ComObject.StationOffset(
                x,
                y,
                out double station,
                out double offset);

            return new SOEData
            {
                Station = station,
                Offset = offset,
                Elevation = 0
            };
        }
    }
}
