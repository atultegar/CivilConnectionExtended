using CivilConnection.Contracts.Models.Geometry;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CivilConnection.Contracts.Models.Civil
{
    public class CorridorSurfaceData
    {
        public string Name { get; set; }
        public IList<IList<PointData>> Boundaries { get; set; } = new List<IList<PointData>>();
        public IList<IList<PointData>> Masks { get; set; } = new List<IList<PointData>>();
    }
}
