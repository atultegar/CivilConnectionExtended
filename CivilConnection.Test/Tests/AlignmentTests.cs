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
        public static void Run()
        {
            TestConsole.Header("ALIGNMENT TESTS");

            var service = new AlignmentService();

            var alignments = service.GetAll();

            Console.WriteLine($"Alignments found: {alignments.Count}");

            foreach (var alignment in alignments)
            {
                Console.WriteLine($"{alignment.Name} | {alignment.Length}");
            }
        }
    }
}
