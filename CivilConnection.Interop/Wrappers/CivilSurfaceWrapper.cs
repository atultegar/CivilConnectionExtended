using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class CivilSurfaceWrapper : ComWrapper
    {
        public CivilSurfaceWrapper(object civilSurface) : base(civilSurface)
        {
        }

        public string Name => (string)ComObject.Name;

        public string Type => ComObject.Type.ToString();

        public DocumentWrapper Document => new DocumentWrapper(ComObject.Document);

        public double[] Points => (double[])ComObject.Points;

        public double FindElevationAtXY(double x, double y) => (double)ComObject.FindElevationAtXY(x, y);

        public double[] SampleElevations(double startX, double startY, double endX, double endY) => (double[])ComObject.SampleElevations(startX, startY, endX, endY);

        public double[] IntersectPointWithSurface(double[] point, double[] direction) => (double[])ComObject.IntersectPointWithSurface(point, direction);
    }
}
