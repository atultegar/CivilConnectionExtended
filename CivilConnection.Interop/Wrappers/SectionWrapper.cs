using CivilConnection.Interop.Wrappers.Base;

namespace CivilConnection.Interop.Wrappers
{
    public class SectionWrapper : ComWrapper
    {
        public SectionWrapper(object obj) : base(obj)
        {
        }

        public double Station => (double)ComObject.Station;
        public double LengthLeft => (double)ComObject.LengthLeft;
        public double LengthRight => (double)ComObject.LengthRight;
        public double ElevationMin => (double)ComObject.ElevationMin;
        public double ElevationMax => (double)ComObject.ElevationMax;
    }
}
