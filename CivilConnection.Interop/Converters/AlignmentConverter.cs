using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Interop.Services;
using CivilConnection.Interop.Wrappers;

namespace CivilConnection.Interop.Converters
{
    public static class AlignmentConverter
    {
        public static AlignmentData Convert(AlignmentWrapper wrapper, AlignmentService service)
        {
            var alignment = wrapper.ComObject;

            return new AlignmentData
            {
                Name = wrapper.Name,
                Length = wrapper.Length,
                StartingStation = wrapper.StartingStation,
                EndingStation = wrapper.EndingStation,

                GeometryStations = service.GetGeometryStations(alignment).ToList(),

                PIStations = service.GetPIStations(alignment).ToList(),

                SuperTransitionStations = service.GetSuperTransStations(alignment).ToList(),

                EquationStations = service.GetEquationStations(alignment).ToList(),

                StationAhead = service.GetStationsAhead(alignment).ToList()
            };
        }
    }
}
