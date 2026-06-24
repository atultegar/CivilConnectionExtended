using System;
using System.IO;

namespace CivilConnection.Contracts.Validation
{
    public static class CivilPythonValidator
    {
        private const string DownloadUrl = "https://www.bimformative.com/tools/civilpython or https://github.com/atultegar/CivilPython/releases";

        public static void EnsureInstalled(string version, bool includeDownloadLink = true)
        {
            string dllPath = GetCivilPythonDllPath(version);

            if (File.Exists(dllPath))
                return;

            string message = $"CivilPython {version} is not installed.";

            if (includeDownloadLink)
            {
                message += $"\n\nDownload it from:\n{DownloadUrl}";
            }

            message += $"\n\nExpectedLocation: {GetCivilPythonDllPath(version)}";

            throw new InvalidOperationException(message);            
        }

        public static string GetDownloadUrl()
        {
            return DownloadUrl;
        }
        
        public static string GetCivilPythonDllPath(string version)
        {
            int versionInt = int.Parse(version);

            string root = versionInt >= 2026
                ? Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Autodesk",
                    "ApplicationPlugins")
                : Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    "Autodesk",
                    "ApplicationPlugins");

            return Path.Combine(root,
                $"CivilPython{version}.bundle",
                "Contents",
                "win",
                $"CivilPython{version}.dll");
        }
        
    }
}
