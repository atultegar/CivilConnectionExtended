using System.Collections.Generic;

namespace CivilConnection.Contracts.Models.Civil
{
    public enum AlignmentStationType
    {
        GeometryPoint = 3,
        PIPoint = 6,
        SuperTransitionPoint = 5,
        Equation = 4
    }
    public class AlignmentData
    {
        public string Name { get; set; } = "";
        public double Length { get; set; }
        public double StartingStation { get; set; }
        public double EndingStation { get; set; }
        public List<double> GeometryStations { get; set; }
        public List<double> PIStations { get; set; }
        public List<double> SuperTransitionStations { get; set; }
        public List<double> EquationStations { get; set; }
        public List<double> StationAhead { get; set; }
    }
}
