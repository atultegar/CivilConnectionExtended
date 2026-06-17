using CivilConnection.Contracts.Models.Civil;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection.Converters
{
    public static class SampleLineGroupConverter
    {
        public static Dictionary<string, object> ToDynamo(SampleLineGroupData data)
        {
            return new Dictionary<string, object>
            {
                { "station", data.Sections.Select(x => x.Station) },
                { "lengthLeft", data.Sections.Select(x => x.LengthLeft) },
                { "lengthRight", data.Sections.Select(x => x.LengthRight) },
                { "elevationMin", data.Sections.Select(x => x.ElevationMin) },
                { "elevationMax", data.Sections.Select(x => x.ElevationMax) }
            };
        }
    }
}
