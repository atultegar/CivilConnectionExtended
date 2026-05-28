using CivilConnection.ApiExtractor.Configuration;
using CivilConnection.ApiExtractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CivilConnection.ApiExtractor.Generators
{
    public class MarkdownGenerator
    {
        public void Generate(ApiDocument document, ExtractorSettings settings)
        {
            string outputDir = Path.Combine(
                settings.OutputDirectory,
                "markdown");

            Directory.CreateDirectory(outputDir);

            foreach (var ns in document.Namespaces)
            {
                var sb = new StringBuilder();

                sb.AppendLine($"# {ns.Name}");

                foreach (var cls in ns.Classes)
                {
                    sb.AppendLine($"## {cls.Name}");

                    sb.AppendLine(cls.Summary);

                    foreach (var method in cls.Methods)
                    {
                        sb.AppendLine($"### {method.Name}");

                        sb.AppendLine(method.Summary);

                        sb.AppendLine($"Return Type: {method.ReturnType}");

                        sb.AppendLine($"IsPublic: {method.IsPublic}");

                        sb.AppendLine($"Parameters: {method.Parameters.Select(a => a.Name)}");
                    }
                }

                File.WriteAllText(
                    Path.Combine(outputDir, $"{ns.Name}.md"),
                    sb.ToString());
            }
        }
    }
}
