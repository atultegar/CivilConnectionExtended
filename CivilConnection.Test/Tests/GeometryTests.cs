using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Context;
using CivilConnection.Interop.Services;
using CivilConnection.Test.Utilities;
using System;

namespace CivilConnection.Test.Tests
{
    public static class GeometryTests
    {
        public static void Run(dynamic document)
        {
            TestConsole.Header("GEOMETRY TESTS");

            try
            {
                

                var geometryService = new GeometryService();

                // Simple horizontal arc
                var arcData = new ArcData
                {
                    Center = new PointData
                    {
                        X = 0,
                        Y = 0,
                        Z = 0
                    },

                    Radius = 100,
                    StartAngleRadians = 0,
                    EndAngleRadians = Math.PI / 2,
                    Transform = null
                };

                string handle = geometryService.AddArc(document, arcData, "0");

                TestConsole.Success("Arc created successfully.");
                Console.WriteLine($"Handle: {handle}");
            }
            catch (Exception ex)
            {
                TestConsole.Failure(ex.Message);
            }
        }
    }
}
