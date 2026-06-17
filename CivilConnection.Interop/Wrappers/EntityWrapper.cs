using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class EntityWrapper : ComWrapper
    {
        public EntityWrapper(object obj) : base(obj)
        {
        }

        public string EntityName => (string)ComObject.EntityName;

        public void Move(PointData from, PointData to)
        {
            ComObject.Move(from.ToArray(), to.ToArray());
        }

        public void Rotate(PointData basePoint, double angleRadians)
        {
            ComObject.Rotate(basePoint.ToArray(), angleRadians);
        }
    }
}
