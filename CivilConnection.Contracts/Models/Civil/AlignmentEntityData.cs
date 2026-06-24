using CivilConnection.Contracts.Enums;
using CivilConnection.Contracts.Models.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Models.Civil
{
    public abstract class AlignmentEntityData
    {
        public AlignmentEntityType EntityType { get; set; }

        public double StartStation { get; set; }
        public double EndStation { get; set; }
    }

    public class AlignmentLineData : AlignmentEntityData
    {
        public PointData StartPoint { get; set; }
        public PointData EndPoint { get; set; }
    }

    public class AlignmentArcData : AlignmentEntityData
    {
        public PointData Center { get; set; }
        public PointData StartPoint { get; set; }
        public PointData EndPoint { get; set; }

        public bool Clockwise { get; set; }
    }

    public class AlignmentSpiralData : AlignmentEntityData
    {
        public List<PointData> Points { get; set; } = new List<PointData>();

        public double A { get; set; }
        public double Length { get; set; }
        public double RadiusIn { get; set; }
        public double RadiusOut { get; set; }
    }
}
