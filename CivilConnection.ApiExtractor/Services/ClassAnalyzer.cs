using CivilConnection.ApiExtractor.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.ApiExtractor.Services
{
    public class ClassAnalyzer
    {
        public void Extract(SyntaxNode root, ApiDocument document)
        {
            var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

            foreach (var cls in classes)
            {
                var namespaceName = GetNamespace(cls);

                // 1. Ensure namespace exists
                var namespaceInfo = document.Namespaces
                    .FirstOrDefault(n => n.Name == namespaceName);

                if (namespaceInfo == null)
                {
                    namespaceInfo = new NamespaceInfo
                    {
                        Name = namespaceName,
                        Classes = new List<ClassInfo>()
                    };

                    document.Namespaces.Add(namespaceInfo);
                }

                // 2. Create class info
                var classInfo = new ClassInfo
                {
                    Name = cls.Identifier.Text,
                    Namespace = GetNamespace(cls),
                    Summary = GetSummary(cls),
                    IsPublic = cls.Modifiers.Any(m => m.Text == "public"),
                    Methods = new List<MethodInfo>()
                };

                // 3. Add class to namespace
                namespaceInfo.Classes.Add(classInfo);
            }
        }

        private string GetNamespace(ClassDeclarationSyntax cls)
        {
            return cls.Ancestors()
                .OfType<NamespaceDeclarationSyntax>()
                .FirstOrDefault()
                ?.Name.ToString() ?? "Global";
        }

        private string GetSummary(ClassDeclarationSyntax cls)
        {
            var trivia = cls.GetLeadingTrivia().ToFullString();
            return trivia.Contains("summary") ? trivia : "";
        }
    }
}
