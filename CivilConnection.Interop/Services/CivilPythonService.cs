using CivilConnection.Contracts.Validation;
using CivilConnection.Interop.Wrappers;
using System.IO;

namespace CivilConnection.Interop.Services
{
    public class CivilPythonService
    {
        public void ReplaceSolid(DocumentWrapper document, string solidHandle, string newHandle)
        {
            CivilPythonValidator.EnsureInstalled(document.Version);

            document.SendCommand($"-ReplaceSolid \"{solidHandle}\"\n\"{newHandle}\"\n\n");
        }

        public void ExportLandFeatureLinesToXml(DocumentWrapper document)
        {
            CivilPythonValidator.EnsureInstalled(document.Version);

            document.SendCommand("-ExportLandFeatureLinesToXml\n");
        }

        public string AddTinSurface(DocumentWrapper document, string name)
        {
            CivilPythonValidator.EnsureInstalled(document.Version);

            document.SendCommand($"-CreateTinSurface \"{name}\"\n");

            return name;
        }

        public string CreatePropertySetDefinition(DocumentWrapper document, string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Error: File not found");

            CivilPythonValidator.EnsureInstalled(document.Version);

            document.SendCommand($"-CREATEPROPERTYSETDEFINITION\n{path}\n");

            return Path.GetFileNameWithoutExtension(path);
        }

        public string CreatePropertySets(DocumentWrapper document, string psetDefinitionName, string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Error: File not found");

            CivilPythonValidator.EnsureInstalled(document.Version);

            document.SendCommand($"-ASSIGNPROPERTYSET\n{psetDefinitionName}\n{path}\n");

            return "PropertySet added successfully";
        }
    }
}
