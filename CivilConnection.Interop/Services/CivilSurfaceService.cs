using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;

namespace CivilConnection.Interop.Services
{
    public class CivilSurfaceService
    {
        private readonly DocumentService _documentService;
        private readonly CivilPythonService _civilPythonService;
        public CivilSurfaceService()
        {
            _documentService = new DocumentService();
            _civilPythonService = new CivilPythonService();
        }
        public IList<CivilSurfaceWrapper> GetSurfaces(DocumentWrapper document)
        {
            var output = new List<CivilSurfaceWrapper>();

            foreach (var surface in document.Surfaces)
            {
                output.Add(surface);
            }

            return output;
        }

        public IEnumerable<PointData> GetPoints(CivilSurfaceWrapper civilSurface)
        {
            var output = new List<PointData>();

            var points = civilSurface.Points;

            for (int i = 0; i < points.Length; i += 3)
            {
                output.Add(new PointData
                {
                    X = points[i],
                    Y = points[i + 1],
                    Z = points[i + 2]
                });
            }

            return output;
        }

        public double FindElevationAtXY(CivilSurfaceWrapper civilSurface, double x, double y)
        {
            return civilSurface.FindElevationAtXY(x, y);
        }

        public double FindElevationAtPoint(CivilSurfaceWrapper civilSurface, PointData point)
        {
            return FindElevationAtXY(civilSurface, point.X, point.Y);
        }

        public IList<PointData> SampleElevations(CivilSurfaceWrapper civilSurface, LineData line)
        {
            var output = new List<PointData>();

            var elevationPoints = civilSurface.SampleElevations(line.StartPoint.X, line.StartPoint.Y, line.EndPoint.X, line.EndPoint.Y);

            for (int i = 0; i < elevationPoints.Length; i += 3)
            {
                output.Add(new PointData
                {
                    X = elevationPoints[i],
                    Y = elevationPoints[i + 1],
                    Z = elevationPoints[i + 2]
                });
            }

            return output;
        }

        public PointData GetIntersectionPoint(CivilSurfaceWrapper civilSurface, PointData point, VectorData direction)
        {
            var intersectionPoint = civilSurface.IntersectPointWithSurface(point.ToArray(), direction.ToArray());

            return new PointData
            {
                X = intersectionPoint[0],
                Y = intersectionPoint[1],
                Z = intersectionPoint[2]
            };
        }

        public string ExportSurfaceToXml(CivilSurfaceWrapper civilSurface)
        {
            string xmlPath = Path.Combine(Environment.GetEnvironmentVariable("TMP", EnvironmentVariableTarget.User), "Surface.xml");

            try
            {
                //Ensure CivilPython installed
                civilSurface.Document.SendCommand($"-ExportSurfaceToXML\n{civilSurface.Handle}\n");

                return xmlPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR: {ex.Message}");
            }            
        }

        public TinSurfaceWrapper CreateTinSurfaceFromPoints(DocumentWrapper document, IList<PointData> points, string name, string layer)
        {
            if (document == null)
                return null;

            if (points.Count < 3)
                return null;

            var surfaceHandle = _civilPythonService.AddTinSurface(document, name);

            var surface = new TinSurfaceWrapper(document.HandleToObject(surfaceHandle));

            surface.Description = "Created by CivilConnection";

            var pointGroup = _documentService.AddPointGroup(document, points, name);

            surface.AddPointGroup(pointGroup);

            surface.Rebuild();

            return surface;
        }
    }
}
