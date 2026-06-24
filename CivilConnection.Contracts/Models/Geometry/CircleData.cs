using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Models.Geometry
{
    public class CircleData : IGeometryData
    {
        public PointData Center { get; set; }

        public double Radius { get; set; }

        public double[,] Transform { get; set; }
    }
}
