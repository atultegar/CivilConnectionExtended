using CivilConnection.Contracts.Models.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Models.Requests
{
    public class IfcExportRequest
    {
        public string OutputFolder { get; set; }
        public PointData Origin { get; set; }
        public double RotationAngle { get; set; }
        public bool ExportSolids { get; set; } = true;
        public bool ExportSurfaces { get; set; } = true;
        public bool ExportBodies { get; set; } = true;
    }
}
