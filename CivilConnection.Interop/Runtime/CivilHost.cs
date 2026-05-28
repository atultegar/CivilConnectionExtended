using CivilConnection.Interop.Models;
using System.Collections.Generic;

namespace CivilConnection.Interop.Runtime
{
    public class CivilHost
    {
        public CivilVersionInfo VersionInfo { get; set; }
        public dynamic Application { get; set; }
        public dynamic AcadApplication { get; set; }
        public bool IsRunning { get; set; }
        public List<dynamic> Documents { get; set; } = new();
    }
}
