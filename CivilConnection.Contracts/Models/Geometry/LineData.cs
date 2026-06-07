using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Models.Geometry
{
    public class LineData : IGeometryData
    {
        public PointData StartPoint { get; set; }
        public PointData EndPoint { get; set; }
    }
}
