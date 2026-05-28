using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Test.Utilities
{
    public static class TestConsole
    {
        public static void Header(string text)
        {
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine(text);
            Console.WriteLine("===================================");
        }

        public static void Success(string text)
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"✓ {text}");
            Console.WriteLine();
        }

        public static void Failure(string text)
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"✕ {text}");
            Console.WriteLine();
        }
    }
}
