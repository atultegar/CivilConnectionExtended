namespace CivilConnection.ApiExtractor.Models
{
    public class ClassInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Namespace { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;

        public List<string> BaseTypes { get; set; } = new();
        public List<string> Interfaces { get; set; } = new();

        public bool IsPublic { get; set; }
        public bool IsVisibleInDynamo { get; set; }
        public bool UsesInterop { get; set; }
        public List<AttributeInfo> Attributes { get; set; } = [];
        public List<MethodInfo> Methods { get; set; } = new();

        public List<string> Tags { get; set; } = new();
    }
}
