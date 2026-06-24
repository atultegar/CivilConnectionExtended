using CivilConnection.Interop.Context;
using CivilConnection.Interop.Wrappers;
using CivilConnection.Test.Tests;
using CivilConnection.Test.Utilities;
using System;

namespace CivilConnection.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CivilConnection Test Runner";

            try
            {
                TestConsole.Header("CivilConnection Test Runner");

                var version = "2026";

                var context = CivilContext.Create(version);

                DocumentTests.Run(context);

                //dynamic document = context.Host.Application.ActiveDocument;

                var document = new DocumentWrapper(context.Host.Application.ActiveDocument);

                //AlignmentTests.Run(document);

                //GeometryTests.Run(document);

                //Console.WriteLine(document.Version);

                CorridorTests.Run(document);

                SurfaceTests.Run(document);

                TestConsole.Success("All tests completed");
            }
            catch (Exception ex)
            {
                TestConsole.Failure(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
