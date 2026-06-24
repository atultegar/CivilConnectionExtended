using CivilConnection.Contracts.Enums;
using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class SolidWrapper : ComWrapper
    {
        public SolidWrapper(object obj) : base(obj)
        {
            ObjectName = "Acad3DSolid";
        }

        public string Layer => ComObject.Layer;

        public void Union(SolidWrapper other)
        {
            if (other == null)
                return;

            ComObject.Boolean(SolidBooleanType.Union, other.ComObject);
        }

        public SolidWrapper Slice(PointData p1, PointData p2, PointData p3, bool keepBothSides)
        {
            var result = ComObject.SliceSolid(p1.ToArray(), p2.ToArray(), p3.ToArray(), keepBothSides);

            return result != null
                ? new SolidWrapper(result)
                : null;
        }

        public void Subtract(SolidWrapper other)
        {
            if (other == null)
                return;

            ComObject.Boolean(SolidBooleanType.Union, other.ComObject);
        }

        public bool CheckInterference(SolidWrapper other)
        {
            ComObject.CheckInterference(other.ComObject, false, out bool result);

            return result;
        }

        public override string ToString()
        {
            return $"{ComObject.EntityName}";
        }
    }
}
