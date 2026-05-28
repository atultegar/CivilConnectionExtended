using CivilConnection.ApiExtractor.Configuration;
using CivilConnection.ApiExtractor.Models;
using System.Text;

namespace CivilConnection.ApiExtractor.Generators
{
    public class MermaidGenerator
    {
        public void Generate(ApiDocument document, ExtractorSettings settings)
        {
            string outputPath = Path.Combine(
                settings.OutputDirectory,
                "diagrams",
                "architecture.mmd");

            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);

            var sb = new StringBuilder();

            sb.AppendLine("graph TD");

            foreach (var ns in document.Namespaces)
            {
                foreach (var cls in ns.Classes)
                {
                    sb.AppendLine(
                        $"{ns.Name.Replace('.', '_')} --> {cls.Name}");
                }
            }

            File.WriteAllText(outputPath, sb.ToString());
        }
    }
}
