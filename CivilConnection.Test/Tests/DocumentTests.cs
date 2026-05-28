using CivilConnection.Contracts.Interfaces;
using CivilConnection.Interop.Services;
using CivilConnection.Test.Utilities;
using System;

namespace CivilConnection.Test.Tests
{
    public static class DocumentTests
    {
        public static void Run()
        {
            TestConsole.Header("DOCUMENT TESTS");

            var service = new DocumentService();

            //var documents = service.GetAllDocuments();

            //Console.WriteLine($"Documents found: {documents.Count}");

            //foreach (var document in documents)
            //{
            //    Console.WriteLine($"{document.Name} | {document.Version}");
            //}

            string version = "2027";

            var activeDoc = service.ActiveDocument(version);

            Console.WriteLine($"ActiveDocument in Version: {version}");
            Console.WriteLine($"{activeDoc.Name} | {activeDoc.Version}");
        }
    }
}
