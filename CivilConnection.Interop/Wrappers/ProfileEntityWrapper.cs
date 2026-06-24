using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class ProfileEntityWrapper : ComWrapper
    {
        public ProfileEntityWrapper(object entity) : base(entity)
        {
        }

        public int Type => (int)ComObject.Type;

        public double StartStation => (double)ComObject.StartStation;

        public double EndStation => (double)ComObject.EndStation;

        public double StartElevation => (double)ComObject.StartElevation;

        public double EndElevation => (double)ComObject.EndElevation;

        public double HighLowPointStation
        {
            get
            {
                try
                {
                    return (double)ComObject.HighLowPointStation;
                }
                catch
                {
                    return double.NaN;
                }
            }
        }

        public double HighLowPointElevation
        {
            get
            {
                try
                {
                    return (double)ComObject.HighLowPointElevation;
                }
                catch
                {
                    return double.NaN;
                }
            }
        }

        public bool HasHighLowPoint
        {
            get
            {
                try
                {
                    var x = ComObject.HighLowPointStation;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
