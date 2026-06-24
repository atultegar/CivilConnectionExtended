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
    public class AlignmentEntityWrapper : ComWrapper
    {
        public AlignmentEntityWrapper(object obj) : base(obj)
        {
        }

        public AlignmentEntityType EntityType
        {
            get
            {
                int value = ComObject.Type;

                return Enum.IsDefined(typeof(AlignmentEntityType), value)
                    ? (AlignmentEntityType)value
                    : AlignmentEntityType.Unknown;
            }
        }

        public double StartingStation => (double)ComObject.StartingStation;

        public double EndingStation => (double)ComObject.EndingStation;

        public PointData StartPoint
        {
            get
            {
                return new PointData
                {
                    X = ComObject.StartEasting,
                    Y = ComObject.StartNorthing,
                    Z = 0
                };
            }
        }

        public PointData EndPoint
        {
            get
            {
                return new PointData
                {
                    X = ComObject.EndEasting,
                    Y = ComObject.EndNorthing,
                    Z = 0
                };
            }
        }

        public PointData Center
        {
            get
            {
                return new PointData
                {
                    X = (double)ComObject.CenterEasting,
                    Y = (double)ComObject.CenterNorthing,
                    Z = 0
                };
            }
        }

        public bool Clockwise => (bool)ComObject.Clockwise;

        public double Length => (double)ComObject.Length;

        public double A => (double)ComObject.A;

        public double RadiusIn => (double)ComObject.RadiusIn;

        public double RadiusOut => (double)ComObject.RadiusOut;
    }
}
