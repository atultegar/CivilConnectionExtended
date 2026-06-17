using Autodesk.AECC.Interop.UiRoadway;
using CivilConnection.Interop.Wrappers;
using Microsoft.SqlServer.Server;
using System;
using System.IO;
using System.Xml.Linq;

namespace CivilConnection.Interop.Services
{
    public class CivilPythonService
    {
        public string GetPluginRootFolder(string version)
        {
            int versionInt = int.Parse(version);

            return versionInt >= 2026
                ? Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Autodesk",
                    "ApplicationPlugins")
                : Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    "Autodesk",
                    "ApplicationPlugins");
        }

        public string GetCivilPythonDllPath(string version)
        {
            return Path.Combine(
                GetPluginRootFolder(version),
                $"CivilPython{version}.bundle",
                "Contents",
                "win",
                $"CivilPython{version}.dll");
        }

        public bool IsInstalled(string version)
        {
            return File.Exists(GetCivilPythonDllPath(version));
        }

        public void ReplaceSolid(DocumentWrapper document, string solidHandle, string newHandle)
        {
            if (!IsInstalled(document.Version))
            {
                throw new InvalidOperationException(
                    $"CivilPython {document.Version} is not installed.");
            }

            document.SendCommand($"-ReplaceSolid \"{solidHandle}\"\n\"{newHandle}\"\n\n");
        }

        public void ExportLandFeatureLinesToXml(DocumentWrapper document)
        {
            if (!IsInstalled(document.Version))
            {
                throw new InvalidOperationException(
                    $"CivilPython {document.Version} is not installed.");
            }

            document.SendCommand("-ExportLandFeatureLinesToXml\n");
        }

        public string AddTinSurface(DocumentWrapper document, string name)
        {
            if (!IsInstalled(document.Version))
            {
                throw new InvalidOperationException(
                    $"CivilPython {document.Version} is not installed.");
            }

            document.SendCommand($"-CreateTinSurface \"{name}\"\n");

            return name;
        }

        public string CreatePropertySetDefinition(DocumentWrapper document, string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Error: File not found");

            if (!IsInstalled(document.Version))
            {
                throw new InvalidOperationException(
                    $"CivilPython {document.Version} is not installed.");
            }

            document.SendCommand($"-CREATEPROPERTYSETDEFINITION\n{path}\n");

            return Path.GetFileNameWithoutExtension(path);
        }

        public string CreatePropertySets(DocumentWrapper document, string psetDefinitionName, string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Error: File not found");

            if (!IsInstalled(document.Version))
            {
                throw new InvalidOperationException(
                    $"CivilPython {document.Version} is not installed.");
            }

            document.SendCommand($"-ASSIGNPROPERTYSET\n{psetDefinitionName}\n{path}\n");

            return "PropertySet added successfully";
        }
    }
}
