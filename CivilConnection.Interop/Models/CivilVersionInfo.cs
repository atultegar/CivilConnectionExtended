using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Models
{
    public class CivilVersionInfo
    {
        /// <summary>
        /// Civil 3D Version
        /// Example: 2026
        /// </summary>
        public string Version { get; set; } = "";

        /// <summary>
        /// AutoCAD COM Version
        /// Example: 25.1
        /// </summary>
        public string AutoCADVersion { get; set; } = "";

        /// <summary>
        /// Civil 3D COM Version
        /// Example: 13.8
        /// </summary>
        public string CivilVersion { get; set; } = "";

        /// <summary>
        /// Civil 3D COM ProgId
        /// </summary>
        public string ProgId { get; set; } = "";

        /// <summary>
        /// AutoCAD Application ProgId
        /// </summary>
        public string AutoCADProgId => $"AutoCAD.Application.{AutoCADVersion}";

        public override string ToString()
        {
            return
                $"Civil 3D {Version}" +
                $"(AutoCAD {AutoCADVersion}";
        }
    }
}
