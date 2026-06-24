using CivilConnection.ApiExtractor.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.ApiExtractor.Services
{
    public class SemanticBuilder
    {
        private readonly Dictionary<string, NamespaceInfo> _namespaceCache = new();

        public ApiDocument Build(List<SyntaxNode> roots)
        {
            var document = new ApiDocument();

            foreach (var root in roots)
            {
                BuildFromRoot(root, document);
            }

            return document;
        }

        private void BuildFromRoot(SyntaxNode root, ApiDocument document)
        {
            var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

            foreach (var cls in classes)
            {
                var nsName = GetNamespace(cls);

                var ns = GetOrCreateNamespace(document, nsName);

                var classInfo = BuildClass(cls, nsName);

                ns.Classes.Add(classInfo);                
            }
        }

        private ClassInfo BuildClass(ClassDeclarationSyntax cls, string ns)
        {
            var classInfo = new ClassInfo
            {
                Name = cls.Identifier.Text,
                Namespace = ns,
                Summary = GetSummary(cls),
                BaseTypes = cls.BaseList?.Types
                    .Select(t => t.Type.ToString())
                    .ToList() ?? new(),
                Methods = new List<MethodInfo>(),
            };

            // Dependency tagging
            classInfo.Tags = DependencyTagger.TagClass(cls);

            // methods
            foreach (var method in cls.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                classInfo.Methods.Add(BuildMethod(method));
            }

            return classInfo;
        }

        private MethodInfo BuildMethod(MethodDeclarationSyntax method)
        {
            return new MethodInfo
            {
                Name = method.Identifier.Text,
                ReturnType = method.ReturnType.ToString(),
                IsPublic = method.Modifiers.Any(m => m.Text == "public"),
                Summary = GetSummary(method),

                Attributes = method.AttributeLists
                    .SelectMany(a => a.Attributes)
                    .Select(a => a.Name.ToString())
                    .ToList(),

                Parameters = method.ParameterList.Parameters
                    .Select(p => new ParameterInfo
                    {
                        Name = p.Identifier.Text,
                        Type = p.Type?.ToString() ?? "unknown"
                    })
                    .ToList()
            };
        }

        private NamespaceInfo GetOrCreateNamespace(ApiDocument document, string name)
        {
            if (_namespaceCache.TryGetValue(name, out var existing))
                return existing;

            var ns = new NamespaceInfo
            {
                Name = name,
                Classes = new List<ClassInfo>()
            };

            _namespaceCache[name] = ns;
            document.Namespaces.Add(ns);

            return ns;
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

        private string GetSummary(MethodDeclarationSyntax method)
        {
            var trivia = method.GetLeadingTrivia().ToFullString();
            return trivia.Contains("summary") ? trivia : "";
        }
    }
}
