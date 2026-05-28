using CivilConnection.ApiExtractor.Configuration;
using CivilConnection.ApiExtractor.Models;
using System.Text.Json;

namespace CivilConnection.ApiExtractor.Generators
{
    public class JsonGenerator
    {
        public void Generate(ApiDocument document, ExtractorSettings settings) 
        {
            string outputPath = Path.Combine(
                settings.OutputDirectory,
                "json",
                "api.json");

            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);

            var json = JsonSerializer.Serialize(
                document,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                });

            File.WriteAllText(outputPath, json);
        }
    }
}
