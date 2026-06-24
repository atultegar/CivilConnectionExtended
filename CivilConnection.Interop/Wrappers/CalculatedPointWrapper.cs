using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Interop.Wrappers.Base;

namespace CivilConnection.Interop.Wrappers
{
    public class CalculatedPointWrapper : ComWrapper
    {
        public CalculatedPointWrapper(object obj) : base(obj)
        {
        }

        public CorridorWrapper Corridor => new CorridorWrapper(ComObject.Corridor);

        public SOEData GetStationOffsetElevationToBaseline()
        {
            var soe = ComObject.GetStationOffsetElevationToBaseline();

            return new SOEData
            {
                Station = soe[0],
                Offset = soe[1],
                Elevation = soe[2]
            };
        }

    }
}
