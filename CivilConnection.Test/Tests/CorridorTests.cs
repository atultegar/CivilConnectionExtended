using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Repositories;
using CivilConnection.Interop.Services;
using CivilConnection.Interop.Wrappers;
using CivilConnection.Test.Utilities;
using System;
using System.Drawing;
using System.Linq;

namespace CivilConnection.Test.Tests
{
    public static class CorridorTests
    {
        public static void Run(DocumentWrapper doc)
        {
            TestConsole.Header("CORRIDOR TESTS");

            var service = new CorridorService();

            var corridors = service.GetCorridors(doc);

            Console.WriteLine($"Corridors found: {corridors.Count}");

            var selectedCorridor = service.GetCorridorByName(doc, "Corridor - (1)");

            Console.WriteLine(selectedCorridor);

            var baselines = service.GetShapes(selectedCorridor);

            foreach(var baseline in baselines)
            {
                foreach (var regions in baseline)
                {
                    foreach (var region in regions)
                    {
                        foreach (var point in region.BoundaryPoints)
                        {
                            Console.WriteLine(point);
                        }
                    }
                }
            }

        }
    }
}
