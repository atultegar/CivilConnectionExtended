using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Services;
using CivilConnection.Interop.Wrappers;
using CivilConnection.Test.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection.Test.Tests
{
    internal class SurfaceTests
    {
        public static void Run(DocumentWrapper doc)
        {
            TestConsole.Header("SURFACE TESTS");

            var service = new CivilSurfaceService();

            var surfaces = doc.Surfaces.ToList();

            foreach (var surface in surfaces)
            {
                Console.WriteLine($"Surface: {surface.Name}");

                Console.WriteLine($"TypeName: {surface.ObjectName}");
            }

            var points = new List<PointData>
            {
                new PointData
                {
                    X = 0,
                    Y = 0,
                    Z = 0
                },
                new PointData
                {
                    X = 10,
                    Y = 0,
                    Z = 0
                },
                new PointData
                {
                    X = 10,
                    Y = 10,
                    Z = 0
                },
                new PointData
                {
                    X = 0,
                    Y = 10,
                    Z = 0
                }
            };

            //var surfaceWrapper = service.CreateTinSurfaceFromPoints(doc, points, "newSurface", "0");

            //Console.WriteLine(surfaceWrapper.Name);
        }
    }
}
