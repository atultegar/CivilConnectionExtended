using CivilConnection.Contracts.Models.Geometry;
using System;

namespace CivilConnection.Contracts.Helpers
{
    public static class VectorMath
    {
        public static double Dot(VectorData a, VectorData b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static VectorData Cross(VectorData a, VectorData b)
        {
            return new VectorData
            {
                X = a.Y * b.Z - a.Z * b.Y,
                Y = a.Z * b.X - a.X * b.Z,
                Z = a.X * b.Y - a.Y * b.X
            };
        }

        public static double Length(VectorData v)
        {
            return Math.Sqrt(
                v.X * v.X +
                v.Y * v.Y +
                v.Z * v.Z);
        }

        public static VectorData Normalize(VectorData v)
        {
            double len = Length(v);

            if (len < 1e-9)
                return new VectorData();

            return new VectorData
            {
                X = v.X / len,
                Y = v.Y / len,
                Z = v.Z / len
            };
        }

        public static double Angle(VectorData a, VectorData b)
        {
            a = Normalize(a);
            b = Normalize(b);

            double dot = Dot(a, b);

            dot = Math.Max(-1.0, Math.Min(1.0, dot));

            return Math.Acos(dot);
        }
    }
}
