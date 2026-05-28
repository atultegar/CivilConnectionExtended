using CivilConnection.Interop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Repositories
{
    public static class CivilVersionRepository
    {
        private static readonly List<CivilVersionInfo> _versions = new()
        {
            new CivilVersionInfo
            {
                Version = "2023",
                AutoCADVersion = "24.2",
                CivilVersion = "13.5",
                ProgId = "AeccXUiRoadway.AeccRoadwayApplication.13.5"
            },

            new CivilVersionInfo
            {
                Version = "2024",
                AutoCADVersion = "24.3",
                CivilVersion = "13.6",
                ProgId = "AeccXUiRoadway.AeccRoadwayApplication.13.6"
            },

            new CivilVersionInfo
            {
                Version = "2025",
                AutoCADVersion = "25.0",
                CivilVersion = "13.7",
                ProgId = "AeccXUiRoadway.AeccRoadwayApplication.13.7"
            },

            new CivilVersionInfo
            {
                Version = "2026",
                AutoCADVersion = "25.1",
                CivilVersion = "13.8",
                ProgId = "AeccXUiRoadway.AeccRoadwayApplication.13.8"
            },

            new CivilVersionInfo
            {
                Version = "2027",
                AutoCADVersion = "26",
                CivilVersion = "13.9",
                ProgId = "AeccXUiRoadway.AeccRoadwayApplication.13.9"
            }
        };

        public static IReadOnlyList<CivilVersionInfo> GetAll()
        {
            return _versions;
        }

        public static CivilVersionInfo? GetByYear(string version)
        {
            return _versions.FirstOrDefault(x => x.Version == version);
        }

        public static CivilVersionInfo? GetByAutoCADVersion(string acadVersion)
        {
            return _versions.FirstOrDefault(x => x.AutoCADVersion == acadVersion);
        }

        public static CivilVersionInfo? GetByCivilVersion(string civilVersion)
        {
            return _versions.FirstOrDefault(x => x.CivilVersion == civilVersion);
        }

        public static CivilVersionInfo? GetByProgId(string progId)
        {
            return _versions.FirstOrDefault(x => x.ProgId == progId);
        }
    }
}
