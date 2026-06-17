using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class CivilPointWrapper : ComWrapper
    {
        public CivilPointWrapper(object obj) : base(obj)
        {
            ObjectName = "AeccPoint";
        }

        public PointData Location
        {
            get
            {
                return new PointData
                {
                    X = (double)ComObject.Easting,
                    Y = (double)ComObject.Northing,
                    Z = (double)ComObject.Elevation
                };
            }
        }

        public string Name => (string)ComObject.Name;

        public int Number => (int)ComObject.Number;
    }
}
