using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Models
{
    public enum DrawingUnitType
    {
        Meters,
        Decimeters,
        Centimeters,
        Millimeters,
        Feet,
        Inches
    }

    public class UnitSettings
    {
        public DrawingUnitType DrawingUnit { get; set; }
        public int Precision { get; set; }
        public double Accuracy { get; set; }
    }
}
