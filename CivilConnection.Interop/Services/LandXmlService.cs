using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Contracts.Models.Geometry;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace CivilConnection.Interop.Services
{
    public class LandXmlService
    {
        #region PUBLIC METHODS
        public List<TriangleData> ReadTriangles(string xmlPath, string surfaceName, bool onlyVisible = true)
        {
            var triangles = new List<TriangleData>();

            var xmlDoc = LoadXml(xmlPath);

            var surfaceElement = GetSurfaceElement(xmlDoc, surfaceName);

            if (surfaceElement == null)
            {
                throw new Exception($"Surface '{surfaceName}' not found in LandXML");
            }

            Dictionary<string, PointData> points = ReadSurfacePoints(surfaceElement);

            foreach (XmlElement faceElement in surfaceElement.GetElementsByTagName("F"))
            {
                if (onlyVisible &&
                    faceElement.HasAttribute("i") &&
                    faceElement.Attributes["i"].Value == "1")
                {
                    continue;
                }

                string[] indices = faceElement.InnerText.Split(
                    new[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (indices.Length < 3)
                    continue;

                PointData p1 = points[indices[0]];
                PointData p2 = points[indices[1]];
                PointData p3 = points[indices[2]];

                triangles.Add(new TriangleData { A = p1, B = p2, C = p3 });
            }
            return triangles;
        }
        

        public SurfaceMeshData ReadFaces(string xmlPath, string surfaceName, bool onlyVisible = true)
        {
            var xmlDoc = LoadXml(xmlPath);

            var surfaceElement = GetSurfaceElement(xmlDoc, surfaceName);

            if (surfaceElement == null)
            {
                throw new Exception($"Surface '{surfaceName}' not found in LandXML");
            }

            var mesh = new SurfaceMeshData();

            // POINTS
            var points = ReadSurfacePoints(surfaceElement);

            foreach (var kvp in points)
            {
                if (int.TryParse(kvp.Key, out int id))
                {
                    mesh.Points[id] = kvp.Value;
                }
            }

            // FACES
            foreach (XmlElement faceElement in surfaceElement.GetElementsByTagName("F"))
            {
                if (onlyVisible &&
                    faceElement.HasAttribute("i") &&
                    faceElement.Attributes["i"].Value == "1")
                {
                    continue;
                }

                try
                {
                    int[] indices = faceElement.InnerText
                        .Split(
                         new[] { ' ' },
                         StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();

                    if (indices.Length >= 3)
                    {
                        mesh.Faces.Add(indices);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error reading LandXML face: {ex.Message}", ex);
                }
            }

            return mesh;

        }

        public List<FeaturelineData> ReadFeaturelines(string xmlPath, string corridorName, int baselineIndex, string code)
        {
            var result = new List<FeaturelineData>();

            var xmlDoc = LoadXml(xmlPath);

            var baselineElement = GetBaselineElemet(xmlDoc, corridorName, baselineIndex);

            if (baselineElement == null)
            {
                return result;
            }

            foreach (XmlElement featurelineElement 
                in baselineElement.GetElementsByTagName("Featureline")
                .Cast<XmlElement>()
                .Where(x =>
                    string.Equals(x.Attributes["Code"]?.Value,
                    code,
                    StringComparison.OrdinalIgnoreCase)))
            {
                double side = 1;

                if (featurelineElement.HasAttribute("Side"))
                {
                    side = Convert.ToDouble(featurelineElement.Attributes["Side"].Value,
                        CultureInfo.InvariantCulture);
                }

                var featureline = new FeaturelineData
                {
                    Code = code,
                    Side = side
                };

                foreach (XmlElement pointElement in featurelineElement.GetElementsByTagName("Point")
                    .Cast<XmlElement>()
                    .OrderBy(x =>
                        Convert.ToDouble(x.Attributes["Station"].Value,
                        CultureInfo.InvariantCulture)))
                {
                    featureline.Points.Add(
                        new FeaturelinePointData
                        {
                            Station = ReadDouble(pointElement, "Station"),
                            X = ReadDouble(pointElement, "X"),
                            Y = ReadDouble(pointElement, "Y"),
                            Z = ReadDouble(pointElement, "Z"),
                            IsBreak = ReadDouble(pointElement, "IsBreak") > 0,
                            RegionIndex = ReadInt(pointElement, "RegionIndex"),
                        });
                }

                result.Add(featureline);
            }

            return result;
        }

        #endregion

        #region PRIVATE METHODS
        private Dictionary<string, PointData> ReadSurfacePoints(XmlElement surfaceElement)
        {
            var points = new Dictionary<string, PointData>();

            foreach (XmlElement pointElement
                in surfaceElement.GetElementsByTagName("P"))
            {
                try
                {
                    string id =
                        pointElement.Attributes["id"]?.Value;

                    if (string.IsNullOrWhiteSpace(id))
                        continue;

                    string[] values =
                        pointElement.InnerText.Split(
                            new[] { ' ' },
                            StringSplitOptions.RemoveEmptyEntries);

                    if (values.Length < 3)
                        continue;

                    double x = Convert.ToDouble(
                        values[1],
                        CultureInfo.InvariantCulture);

                    double y = Convert.ToDouble(
                        values[0],
                        CultureInfo.InvariantCulture);

                    double z = Convert.ToDouble(
                        values[2],
                        CultureInfo.InvariantCulture);

                    points[id] = new PointData
                    {
                        X = x,
                        Y = y,
                        Z = z
                    };
                }
                catch (Exception ex)
                {
                    throw new Exception($"ERROR reading LandXML point: {ex.Message}");
                }
            }

            return points;
        }

        private XmlDocument LoadXml(string xmlPath)
        {
            if (!File.Exists(xmlPath))
            {
                throw new FileNotFoundException($"LandXML file not found: {xmlPath}");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            return xmlDoc;
        }

        private XmlElement GetSurfaceElement(XmlDocument xmlDoc, string surfaceName)
        {
            return xmlDoc.GetElementsByTagName("Surface")
                    .Cast<XmlElement>()
                    .FirstOrDefault(x =>
                        string.Equals(
                            x.Attributes["name"]?.Value,
                            surfaceName,
                            StringComparison.OrdinalIgnoreCase));
        }

        private XmlElement GetBaselineElemet(XmlDocument xmlDoc, string corridorName, int baselineIndex)
        {
            return xmlDoc.GetElementsByTagName("Baseline")
                .Cast<XmlElement>()
                .FirstOrDefault(x =>
                    Convert.ToInt32(
                        x.Attributes["Index"].Value,
                        CultureInfo.InvariantCulture) == baselineIndex
                    &&
                    x.ParentNode?.ParentNode?.Attributes?["Name"]?.Value ==
                    corridorName);
        }

        private static double ReadDouble(XmlElement element, string attributeName)
        {
            if (!element.HasAttribute(attributeName))
                return 0;

            return Convert.ToDouble(
                element.Attributes[attributeName].Value,
                CultureInfo.InvariantCulture);
        }

        private static int ReadInt(XmlElement element, string attributeName)
        {
            if (!element.HasAttribute(attributeName))
                return 0;

            return Convert.ToInt32(
                element.Attributes[attributeName].Value,
                CultureInfo.InvariantCulture);
        }



        #endregion
    }
}
