using CivilConnection.Interop.Wrappers.Base;

namespace CivilConnection.Interop.Wrappers
{
    public class StationEquationWrapper : ComWrapper
    {
        public StationEquationWrapper(object obj) : base(obj)
        {
        }

        public double RawStationBack => (double)ComObject.RawStationBack;

        public double StationAhead => (double)ComObject.StationAhead;
        public double StationBack => (double)ComObject.StationBack;
    }
}
