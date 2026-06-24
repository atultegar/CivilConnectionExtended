namespace CivilConnection.ApiExtractor.Models
{
    public class ApiDocument
    {
        public List<NamespaceInfo> Namespaces { get; set; } = new();
        public List<DiagnosticInfo> Diagnostics { get; set; } = new();
    }
}
