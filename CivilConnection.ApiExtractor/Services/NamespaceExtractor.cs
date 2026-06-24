using CivilConnection.ApiExtractor.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CivilConnection.ApiExtractor.Services
{
    public class NamespaceExtractor
    {
        public void Extract(
            Microsoft.CodeAnalysis.SyntaxNode root,
            ApiDocument apiDocument)
        {
            var namespaces = root.DescendantNodes()
                .OfType<NamespaceDeclarationSyntax>();

            foreach (var ns in namespaces)
            {
                var namespaceInfo = new NamespaceInfo
                {
                    Name = ns.Name.ToString()
                };

                apiDocument.Namespaces.Add(namespaceInfo);
            }
        }
    }
}
