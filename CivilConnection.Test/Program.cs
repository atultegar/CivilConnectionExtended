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

                DocumentTests.Run();

                //AlignmentTests.Run();

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
