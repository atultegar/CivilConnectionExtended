using System.Collections.Generic;

namespace CivilConnection.Interop.Runtime
{
    public static class ProgIdRegistry
    {
        public static readonly Dictionary<string, string> CivilProgIds = new()
        {
            { "24.2", "AeccXUiRoadway.AeccRoadwayApplication.13.5" },
            { "24.3", "AeccXUiRoadway.AeccRoadwayApplication.13.6" },
            { "25.0", "AeccXUiRoadway.AeccRoadwayApplication.13.7" },
            { "25.1", "AeccXUiRoadway.AeccRoadwayApplication.13.8" },
            { "26.0", "AeccXUiRoadway.AeccRoadwayApplication.13.9" }
        };

        public static string? GetCivilProgId(string acadVersion)
        {
            CivilProgIds.TryGetValue(acadVersion, out var progId);

            return progId;
        }
        
    }
}
