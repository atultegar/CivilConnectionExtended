namespace CivilConnection.Contracts.Models.Geometry
{
    /// <summary>
    /// Represents a point or vector in three-dimensional space with X, Y, and Z coordinates.
    /// </summary>
    public class PointData
    {
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public double Z { get; set; } = 0;

        /// <summary>
        /// Returns the components of the vector as an array of doubles.
        /// </summary>
        /// <returns>An array of three doubles containing the X, Y, and Z components of the vector, in that order.</returns>
        public double[] ToArray()
        {
            return new[] { X, Y, Z };
        }

        public override string ToString()
        {
            return $"PointData(X = {X}, Y = {Y}, Z = {Z})";
        }
    }
}
