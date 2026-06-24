namespace CivilConnection.ApiExtractor.Services
{
    public class FileScanner
    {
        public List<string> GetCSharpFiles(string rootFolder)
        {
            return Directory.GetFiles(
                rootFolder,
                "*.cs",
                SearchOption.AllDirectories
            ).ToList();
        }
    }
}
