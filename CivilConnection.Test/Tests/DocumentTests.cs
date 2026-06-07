using CivilConnection.Contracts.Interfaces;
using CivilConnection.Interop.Context;
using CivilConnection.Interop.Services;
using CivilConnection.Test.Utilities;
using System;

namespace CivilConnection.Test.Tests
{
    public static class DocumentTests
    {
        public static void Run(CivilContext context)
        {
            TestConsole.Header("DOCUMENT TESTS");

            var service = new DocumentService();

            var documents = service.GetDocuments(context);

            Console.WriteLine($"Documents found: {documents.Count}");

            foreach (var document in documents)
            {
                Console.WriteLine($"Document {documents.IndexOf(document)}: {document.Name}");
            }

            var activeDoc = service.GetActiveDocument(context);

            Console.WriteLine($"ActiveDocument: {activeDoc.Name}");
        }
    }
}
