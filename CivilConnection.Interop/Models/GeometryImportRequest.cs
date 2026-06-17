using CivilConnection.Contracts.Models.Geometry;
using System.Collections.Generic;

namespace CivilConnection.Interop.Models
{
    public class GeometryImportRequest
    {
        public IList<IGeometryData> Geometry { get; set; } = new List<IGeometryData>();
        public IList<string> SatFiles { get; set; } = new List<string>();

        public bool DeleteSatFilesAfterImport { get; set; }
    }
}
