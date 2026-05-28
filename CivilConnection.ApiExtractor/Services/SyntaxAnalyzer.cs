using CivilConnection.ApiExtractor.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.ApiExtractor.Services
{
    public class SyntaxAnalyzer
    {
        private readonly ClassAnalyzer _classAnalyzer = new();

        public ApiDocument Analyze(List<string> files)
        {
            var syntaxTrees = new List<SyntaxNode>();

            foreach (var file in files)
            {
                var code = File.ReadAllText(file);

                var tree = CSharpSyntaxTree.ParseText(code);

                var root = tree.GetRoot();

                syntaxTrees.Add(root);
            }

            Console.WriteLine("Building semantic model...");

            var builder = new SemanticBuilder();

            var document = builder.Build(syntaxTrees);

            return document;
        }
    }
}
