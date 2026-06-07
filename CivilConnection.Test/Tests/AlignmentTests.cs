using CivilConnection.Interop.Context;
using CivilConnection.Interop.Services;
using CivilConnection.Test.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Test.Tests
{
    public static class AlignmentTests
    {
        public static void Run(dynamic doc)
        {
            TestConsole.Header("ALIGNMENT TESTS");

            var service = new AlignmentService();

            var alignments = service.GetAlignments(doc);

            Console.WriteLine($"Alignments found: {alignments.Count}");

            var selectedAlignment = service.GetAlignmentByName(doc, "Spår 22");

            Console.WriteLine(selectedAlignment);

            var geometryStations  = service.GetEquationStations(selectedAlignment);

            foreach ( var geometry in geometryStations)
            {
                Console.WriteLine($"Station: {geometry}");
            }
        }
    }
}
