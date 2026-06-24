using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using CivilConnection.Interop.Utilities;
using CivilConnection.Contracts.Validation;

namespace CivilConnection.Interop.Services
{    
    public class CorridorService
    {
        private readonly BaselineService _baselineService;
        private readonly CivilPythonService _civilPythonService;
        private readonly LandXmlService _landXmlService;

        public CorridorService()
        {
            _baselineService = new BaselineService();
            _civilPythonService = new CivilPythonService();
            _landXmlService = new LandXmlService();
        }

        public IList<CorridorWrapper> GetCorridors(DocumentWrapper document)
        {
            return document.Corridors.ToList();
        }

        public CorridorWrapper GetCorridorByName(DocumentWrapper document, string name)
        {
            return GetCorridors(document).Where(x => x.Name == name).First();
        }

        public IList<string> GetCodes(CorridorWrapper corridor)
        {
            var codes = new HashSet<string>();

            try
            {
                foreach (var baseline in corridor.Baselines)
                {
                    foreach (string code in baseline.CorridorCodes)
                    {
                        codes.Add(code);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return codes.OrderBy(x => x).ToList();
        }

        public string ExportShapesXml(CorridorWrapper corridor)
        {
            string xmlPath = Utilities.Paths.GetCorridorShapesXmlPath();

            string command = $"-ExportSubassemblyShapesToXML\n{corridor.Handle}\n{-1}\n{-1}\n";

            return ExportXml(corridor, xmlPath, command);
        }

        public string ExportLinksXml(CorridorWrapper corridor)
        {
            string xmlPath = Utilities.Paths.GetCorridorLinksXmlPath();

            string command = $"-ExportSubassemblyLinksToXML\n{corridor.Handle}\n{-1}\n{-1}\n";

            return ExportXml(corridor, xmlPath, command);
        }

        public IList<IList<IList<FeaturelineData>>> GetFeaturelines(CorridorWrapper corridor)
        {
            var result = new List<IList<IList<FeaturelineData>>>();

            var xmlPath = GetFeaturelinesXmlPath(corridor);

            var codes = GetCodes(corridor);

            foreach (var baseline in corridor.Baselines)
            {

                var baselineFeatureLines = new List<IList<FeaturelineData>>();

                foreach (string code in codes)
                {
                    foreach (var region in _baselineService.GetFeaturelines(baseline, xmlPath, code))
                    {
                        baselineFeatureLines.Add(region);
                    }
                }

                result.Add(baselineFeatureLines);
            }            

            return result;
        }

        public IList<IList<IList<FeaturelineData>>> GetFeaturelinesByCode(CorridorWrapper corridor, string code)
        {
            var result = new List<IList<IList<FeaturelineData>>>();

            var xmlPath = GetFeaturelinesXmlPath(corridor);

            foreach (var baseline in corridor.Baselines)
            {
                result.Add(_baselineService.GetFeaturelines(baseline, xmlPath, code));
            }

            return result;
        }

        public IList<IList<FeaturelineData>> GetFeaturelinesByCodeStation(CorridorWrapper corridor, string code, double station)
        {
            var result = new List<IList<FeaturelineData>>();

            var codes = GetCodes(corridor);

            foreach (var baseline in corridor.Baselines)
            {
                result.Add(_baselineService.GetFeaturelinesAtStation(baseline, code, station));
            }

            return result;
        }



        public bool IsFeaturelinesXMLExported()
        {
            var xmlPath = Utilities.Paths.GetCorridorFeaturelinesXmlPath();

            if (!File.Exists(xmlPath))
                return false;

            return true;
        }

        public bool IsCorridorFeaturelinesXMLExported(string corridorName)
        {
            var xmlPath = Utilities.Paths.GetCorridorFeaturelineXmlPath(corridorName);

            if (!File.Exists(xmlPath))
                return false;

            return true;
        }

        public string GetFeaturelinesXmlPath(CorridorWrapper corridor)
        {
            string nullXmlPath = Utilities.Paths.GetCorridorFeaturelinesXmlPath();

            string corridorXmlPath = Utilities.Paths.GetCorridorFeaturelineXmlPath(corridor.Name);

            if (!File.Exists(corridorXmlPath) && !File.Exists(nullXmlPath))
            {
                ExportFeaturelinesXml(corridor);
            }

            if (File.Exists(nullXmlPath))
            {
                return nullXmlPath;
            }

            if (File.Exists(corridorXmlPath))
            {
                return corridorXmlPath;
            }

            throw new FileNotFoundException("Failed to locate CorridorFeatureLines.xml");
        }

        public IList<IList<IList<AppliedSubassemblyShapeData>>> GetShapes(CorridorWrapper corridor)
        {
            var xmlPath = ExportShapesXml(corridor);

            if (File.Exists(xmlPath))
            {

                return _landXmlService.ReadShapes(xmlPath, corridor.Name);
            }
            else
            {
                return new List<IList<IList<AppliedSubassemblyShapeData>>>();
            }
        }

        public IList<AppliedSubassemblyShapeData> GetShapes(CorridorWrapper corridor, int baselineIndex, int regionIndex)
        {
            var shapes = GetShapes(corridor);

            if (baselineIndex < 0 || baselineIndex >= shapes.Count)
                return new List<AppliedSubassemblyShapeData>();

            var baseline = shapes[baselineIndex];

            if (regionIndex < 0 || regionIndex >= baseline.Count)
                return new List<AppliedSubassemblyShapeData>();

            return baseline[regionIndex];
        }

        public IList<IList<IList<AppliedSubassemblyLinkData>>> GetLinks(CorridorWrapper corridor)
        {
            var xmlPath = ExportLinksXml(corridor);

            if (File.Exists(xmlPath))
            {

                return _landXmlService.ReadLinks(xmlPath, corridor.Name);
            }
            else
            {
                return new List<IList<IList<AppliedSubassemblyLinkData>>>();
            }
        }

        public IList<AppliedSubassemblyLinkData> GetLinks(CorridorWrapper corridor, int baselineIndex, int regionIndex)
        {
            var links = GetLinks(corridor);

            if (baselineIndex < 0 || baselineIndex >= links.Count)
                return new List<AppliedSubassemblyLinkData>();

            var baseline = links[baselineIndex];

            if (regionIndex < 0 || regionIndex >= baseline.Count)
                return new List<AppliedSubassemblyLinkData>();

            return baseline[regionIndex];
        }

        public IList<CorridorSurfaceData> GetCorridorSurfaces(CorridorWrapper corridor)
        {
            var output = new List<CorridorSurfaceData>();

            if (corridor?.CorridorSurfaces == null)
            {
                return output;
            }

            foreach (var surface in corridor.CorridorSurfaces)
            {
                var surfaceData = new CorridorSurfaceData
                {
                    Name = surface.Name
                };

                if (surface.Masks.Count() > 0)
                {
                    foreach (var mask in surface.Masks)
                    {
                        var points = mask.GetPolygonPoints().ToList();

                        surfaceData.Masks.Add(points);
                    }
                }
                
                if (surface.Boundaries.Count() > 0)
                {
                    foreach (var boundary in surface.Boundaries)
                    {
                        var points = boundary.GetPolygonPoints().ToList();

                        surfaceData.Boundaries.Add(points);
                    }
                }
                

                output.Add(surfaceData);
            }

            return output;
        }

        private void ExportFeaturelinesXml(CorridorWrapper corridor)
        {
            string xmlPath = Utilities.Paths.GetCorridorFeaturelineXmlPath(corridor.Name);

            string nullXmlPath = Utilities.Paths.GetCorridorFeaturelinesXmlPath();

            CivilPythonValidator.EnsureInstalled(corridor.Document.Version);

            string command = $"-ExportCorridorFeatureLinesToXml\n{corridor.Handle}\n";

            ExportXml(corridor, xmlPath, command);
        }

        private string ExportXml(CorridorWrapper corridor, string xmlPath, string command)
        {
            var document = corridor.Document;

            CivilPythonValidator.EnsureInstalled(corridor.Document.Version);

            document.SendCommand(command);

            Paths.WaitForXml(xmlPath);

            return xmlPath;
        }

        

        public FeaturelineData ClosestFeaturelineByPoint(CorridorWrapper corridor, PointData p, int baselineIndex, string code, string side)
        {
            var b = corridor.Baselines.First(x => x.Index == baselineIndex);

            if (b == null)
                return new FeaturelineData();

            var soeData = b.Alignment.GetStationOffset(p.X, p.Y);

            var station = soeData.Station;
            var offset = soeData.Offset;

            var stations = b.Stations;

            double min = stations.Min();
            double max = stations.Max();

            if ((station < min && Math.Abs(station - min) > 0.00001) || (station > max && Math.Abs(station - max) > 0.00001))
            {                
                throw new Exception("ERROR: the point is outside of the Baseline station range.");
            }

            var f = _baselineService.GetFeaturelinesAtStation(b, code, station).First(x => x.Side.ToString() == side);

            return f;
        }
    }
}
