using CivilConnection.ApiExtractor.Models;
using Microsoft.CodeAnalysis;

namespace CivilConnection.ApiExtractor.Services
{
    public class ProjectScanner
    {
        public async Task<ApiDocument> ScanAsync(List<Project> projects)
        {
            var document = new ApiDocument();

            var namespaceExtractor = new NamespaceExtractor();

            foreach (var project in projects)
            {
                foreach (var doc in project.Documents)
                {
                    var root = await doc.GetSyntaxRootAsync();

                    if (root == null)
                        continue;

                    namespaceExtractor.Extract(root, document);
                }
            }

            return document;
        }
    }
}
