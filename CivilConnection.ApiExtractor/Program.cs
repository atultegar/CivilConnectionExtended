using CivilConnection.ApiExtractor.Configuration;
using CivilConnection.ApiExtractor.Generators;
using CivilConnection.ApiExtractor.Services;
using Microsoft.CodeAnalysis;
using System.CodeDom;

namespace CivilConnection.ApiExtractor
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var settings = new ExtractorSettings
            {
                RootFolder = @"C:\Projects\CivilConnection\2024\CivilConnection",
                OutputDirectory = @"C:\Projects\CivilConnection\2024\CivilConnection\CivilConnection.ApiExtractor\Output"
            };

            Console.WriteLine("Scanning C# files...");

            var fileScanner = new FileScanner();

            var files = fileScanner.GetCSharpFiles(settings.RootFolder);

            Console.WriteLine($"Files found: {files.Count}");

            var analyzer = new SyntaxAnalyzer();

            var document = analyzer.Analyze(files);

            Console.WriteLine("Generaing outputs...");

            new JsonGenerator().Generate(document, settings);
            new MarkdownGenerator().Generate(document, settings);
            
            Console.WriteLine("API extraction completed.");
        }
    }
}
