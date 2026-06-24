using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.ApiExtractor.Models
{
    public class MethodInfo
    {
        public string Name { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string ReturnType { get; set; } = string.Empty;

        public bool IsPublic { get; set; }

        public bool IsStatic { get; set; }

        public bool IsVisibleInDynamo { get; set; }

        public bool UsesInterop { get; set; }

        public List<ParameterInfo> Parameters { get; set; } = new();

        public List<string> Attributes { get; set; } = new();
    }
}
