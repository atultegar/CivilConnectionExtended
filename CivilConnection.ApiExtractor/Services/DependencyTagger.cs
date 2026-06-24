using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.ApiExtractor.Services
{
    public static class DependencyTagger
    {
        public static List<string> TagClass(ClassDeclarationSyntax cls)
        {
            var text = cls.ToFullString();

            var tags = new List<string>();

            if (text.Contains("Autodesk.AECC"))
                tags.Add("Civil3D");

            if (text.Contains("RevitAPI"))
                tags.Add("Revit");

            if (text.Contains("Dynamo"))
                tags.Add("Dynamo");

            if (text.Contains("System.Runtime.InteropServices"))
                tags.Add("COM");

            return tags;
        }
    }
}
