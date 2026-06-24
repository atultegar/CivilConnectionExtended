using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Context
{
    internal static class ProgIdProvider
    {
        public static string[] GetCivilProgIds()
        {
#if C2023
            return new[]
            {
                "AeccXUiRoadway.AeccRoadwayApplication.13.5"
            };

#elif C2024
            return new[]
            {
                "AeccXUiRoadway.AeccRoadwayApplication.13.6"
            };

#elif C2025
            return new[]
            {
                "AeccXUiRoadway.AeccRoadwayApplication.13.7"
            };

#elif C2026
            return new[]
            {
                "AeccXUiRoadway.AeccRoadwayApplication.13.8"
            };


#elif C2027
            return new[]
            {
                "AeccXUiRoadway.AeccRoadwayApplication.13.9"
            };

#else
            return Array.Empty<string>();
#endif
        }
    }
}
