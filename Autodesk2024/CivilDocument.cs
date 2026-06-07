// Copyright (c) 2016 Autodesk, Inc.
// Copyright (c) 2026 Atul Tegar
//
// Original Author: paolo.serra@autodesk.com
// Maintained and extended by: atul.tegar@gmail.com
// 
// Licensed under the Apache License, Version 2.0 (the "License").
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Autodesk.AECC.Interop.Land;
using Autodesk.AECC.Interop.Roadway;
using Autodesk.AutoCAD.Interop.Common;
using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using CivilConnection.Contracts.Models;
using CivilConnection.Converters;
using CivilConnection.Interop.Context;
using CivilConnection.Interop.Services;
using CivilConnection.Interop.Wrappers;
using Revit.Elements;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace CivilConnection
{
    /// <summary>
    /// Represents a Civil 3D document inside Dynamo.
    /// </summary>
    public class CivilDocument
    {
        #region INTERNAL FIELDS

        internal readonly CivilContext _context;

        internal readonly DocumentWrapper _document;

        internal readonly CivilDocumentData _data;

        #endregion

        #region SERVICES

        private readonly AlignmentService _alignmentService;

        private readonly CorridorService _corridorService;

        private readonly CivilSurfaceService _surfaceService;

        private readonly CommandService _commandService;

        private readonly GeometryService _geometryService;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the CivilDocument class with the specified context, document, and data.
        /// </summary>
        /// <param name="context">The CivilContext that provides access to civil-specific services and settings for the document.</param>
        /// <param name="document">The underlying dynamic document object to be associated with this CivilDocument instance.</param>
        internal CivilDocument(DocumentWrapper document)
        {
            _document = document;

            _alignmentService = new AlignmentService();

            _corridorService = new CorridorService();

            _surfaceService = new CivilSurfaceService();

            _commandService = new CommandService();

            _geometryService = new GeometryService();
        }

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// The document name.
        /// </summary>
        public string Name => _document.Name;

        /// <summary>
        /// Gets the internal element.
        /// </summary>
        /// <value>
        /// The internal element.
        /// </value>
        internal object InternalElement => _document;

        /// <summary>
        /// The corridors
        /// </summary>
        private AeccCorridors _corridors;

        /// <summary>
        /// The alignments
        /// </summary>
        private AeccAlignmentsSiteless _alignments;

        /// <summary>
        /// The Surfaces
        /// </summary>
        private AeccSurfaces _surfaces;

        #endregion


        #region ALIGNMENTS

        /// <summary>
        /// Gets all alignments.
        /// </summary>
        /// <returns></returns>
        public IList<Alignment> GetAlignments()
        {
            Utils.Log("CivilDocument.GetAlignments started...");

            var output = _alignmentService
                .GetAlignments(_document)
                .Select(x => new Alignment(x))
                .ToList();

            Utils.Log("CivilDocument.GetAlignments completed.");

            return output;
        }

        /// <summary>
        /// Gets alignment by name.
        /// </summary>
        /// <param name="name">The alignment name.</param>
        /// <returns></returns>
        public Alignment GetAlignmentByName(string name)
        {
            return GetAlignments().FirstOrDefault(x => x.Name == name);
        }

        #endregion

        #region CORRIDORS

        /// <summary>
        /// Gets the corridors.
        /// </summary>
        /// <returns></returns>
        public IList<Corridor> GetCorridors()
        {
            Utils.Log("CivilDocument.GetCorridors started...");

            // Cast the lambda to a delegate to avoid CS1977 when the call becomes dynamically dispatched
            var selector = (Func<dynamic, Corridor>)(x => new Corridor(x));

            var output = _corridorService
                .GetCorridors(_document)
                .Select(selector)
                .ToList();

            Utils.Log("CivilDocument.GetCorridors completed.");

            return output;
        }

        /// <summary>
        /// Get the corridor by name.
        /// </summary>
        /// <param name="name">The corridor name.</param>
        /// <returns></returns>
        public Corridor GetCorridorByName(string name)
        {
            return GetCorridors().FirstOrDefault(x => x.Name == name);
        }

        #endregion

        #region SURFACES

        /// <summary>
        /// Gets all surfaces in the document
        /// </summary>
        /// <returns>
        /// List of surfaces
        /// </returns>
        public IList<CivilSurface> GetSurfaces()
        {
            Utils.Log("CivilDocument.GetSurfaces started...");

            // Cast the lambda to a delegate to avoid CS1977 when the call becomes dynamically dispatched
            var selector = (Func<dynamic, CivilSurface>)(x => new CivilSurface(_context, x));

            var output = _surfaceService
                .GetSurfaces(_document)
                .Select(selector)
                .ToList();

            Utils.Log("CivilDocument.GetSurfaces completed.");

            return output;
        }

        /// <summary>
        /// Gets surface by name.
        /// </summary>
        /// <param name="name">The name of the surface</param>
        /// <returns>
        /// Civil Surface
        /// </returns>
        public CivilSurface GetSurfaceByName(string name)
        {
            return GetSurfaces().FirstOrDefault(x => x.Name == name);
        }

        #endregion

        #region COMMANDS

        /// <summary>
        /// Send Command to the Civil Document.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public bool SendCommand(string command)
        {
            Utils.LogMethodStart(this);

            try
            {
                _commandService.SendCommand(_document, command);

                Utils.LogMethodEnd(this);

                return true;
            }
            catch (Exception ex)
            {
                Utils.Log($"ERROR: {ex.Message}");

                return false;
            }
        }

        #endregion

        #region AUTOCAD GEOMETRY

        /// <summary>
        /// Adds a new layer to the Civil Document by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string AddLayer(string name)
        {
            Utils.LogMethodStart(this);

            try
            {
                _geometryService.AddLayer(_document, name);

                Utils.LogMethodEnd(this);

                return name;
            }
            catch (Exception ex)
            {
                Utils.Log($"ERROR: {ex.Message}");

                return $"ERROR: {ex.Message}";
            }
        }

        /// <summary>
        /// Add new layers to the Civil Document by names.
        /// </summary>
        /// <param name="names">Layer names</param>
        /// <returns></returns>
        public IList<string> AddLayers(IList<string> names)
        {
            Utils.LogMethodStart(this);

            try
            {
                _geometryService.Addlayers(_document, names);

                Utils.LogMethodEnd(this);

                return names;
            }
            catch (Exception ex)
            {
                Utils.Log($"ERROR: {ex.Message}");

                return null;
            }
        }

        /// <summary>
        /// Adds the DB Point.
        /// </summary>
        /// <param name="point">The coordinates of the point to add to the database.</param>
        /// <param name="layer">The name of the layer on which to add the point. Defaults to "0" if not specified.</param>
        /// <returns>The identifier of the newly added point entity.</returns>
        public string AddDBPoint(Point point, string layer = "0")
        {
            Utils.LogMethodStart(this);

            var pointData = GeometryConverter.ToPointData(point);

            var handle = _geometryService.AddDBPoint(_document, pointData, layer);

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Adds the DB Points.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="layers"></param>
        /// <returns>Object Handles</returns>
        public List<string> AddDBPoints(List<Point> points, List<string> layers)
        {
            Utils.LogMethodStart(this);

            var pointsData = points.Select(x => GeometryConverter.ToPointData(x)).ToList();

            var handles = _geometryService.AddDBPoints(_document, pointsData, layers);

            Utils.LogMethodEnd(this);

            return handles;
        }

        /// <summary>
        /// Adds the 3D polyline by curve.
        /// </summary>
        /// <param name="curve">The curve.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        public string AddPolylineByCurve(Curve curve, string layer)
        {
            Utils.LogMethodStart(this);

            var geometry = GeometryConverter.Convert(curve);

            var handle = _geometryService.AddGeometry(_document, geometry, layer);

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Adds the 3D polylines by curve.
        /// </summary>
        /// <param name="curves">The curves.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        public string AddPolylineByCurves(IList<Curve> curves, string layer)
        {
            Utils.LogMethodStart(this);

            var polylineData = GeometryConverter.ToPolylineData(curves);

            var handle = _geometryService.AddPolyline(_document, polylineData, layer);

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Adds the arc to the document.
        /// </summary>
        /// <param name="arc">The arc.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        public string AddArc(Arc arc, string layer)
        {
            Utils.LogMethodStart(this);

            var data = GeometryConverter.ToArcData(arc);

            var handle = _geometryService.AddArc(_document, data, layer);

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Adds the circle to the document.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        public string AddCircle(Circle circle, string layer)
        {
            Utils.LogMethodStart(this);

            var circleData = GeometryConverter.ToCircleData(circle);

            var handle = _geometryService.AddCircle(_document, circleData, layer);

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Adds the 2D polyline by points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        public string AddLWPolylineByPoints(IList<Point> points, string layer)
        {
            Utils.LogMethodStart(this);

            var polylineData = GeometryConverter.ToPolylineData(points);

            var handle = _geometryService.AddPolyline(_document, polylineData, layer);

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Adds the region by closed profile.
        /// </summary>
        /// <param name="closedCurve">The closed curve.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        public string AddRegionByPatch(Curve closedCurve, string layer)
        {
            Utils.LogMethodStart(this);

            var curveData = GeometryConverter.ToPolylineData(closedCurve as PolyCurve);

            var handle = _geometryService.AddRegion(_document, curveData, layer);

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Creates a closed profile form the points and adds the extruded solid.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="height">The height. By Default is equal to 1.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        public string AddExtrudedSolidByPoints(IList<Point> points, double height = 1, string layer = "_CivilConnectionSolid")
        {
            Utils.LogMethodStart(this);

            var curveData = GeometryConverter.ToPolylineData(points);

            curveData.IsClosed = true;

            var handle = _geometryService.ExtrudeCurve(_document, curveData, height, layer);

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Adds the extruded solid by closed profile.
        /// </summary>
        /// <param name="closedCurve">The closed curve.</param>
        /// <param name="height">The height. By Default is equal to 1.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        public string AddExtrudedSolidByPatch(Curve closedCurve, double height = 1, string layer = "_CivilConnectionSolid")
        {
            Utils.LogMethodStart(this);

            var curveData = GeometryConverter.ToPolylineData(closedCurve as PolyCurve);

            curveData.IsClosed = true;

            var handle = _geometryService.ExtrudeCurve(_document, curveData, height, layer);

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Adds multiple extruded solid by closed curves.
        /// </summary>
        /// <param name="curves">The curves.</param>
        /// <param name="height">The height. By Default is equal to 1.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        public string AddExtrudedSolidByCurves(IList<Curve> curves, double height = 1, string layer = "_CivilConnectionSolid")
        {
            Utils.LogMethodStart(this);

            var curveData = GeometryConverter.ToPolylineData(curves);

            curveData.IsClosed = true;

            var handle = _geometryService.ExtrudeCurve(_document, curveData, height, layer);

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Creates a text in the CivilDocument and rotates it to match the plane.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="point">The point.</param>
        /// <param name="textHeight">Height of the text.</param>
        /// <param name="layer">The layer.</param>
        /// <param name="cs">The cs.</param>
        /// <returns></returns>
        public string AddText(string text, Point point, double textHeight, string layer, CoordinateSystem cs)
        {
            Utils.LogMethodStart(this);

            var pointData = GeometryConverter.ToPointData(point);

            var handle = _geometryService.AddText(_document, text, pointData, textHeight, layer);

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Creates an extrusion based on a closed curve (Polycurve, Polygon or Rectangle) profile to be used to cut the solids in the Civil Document.
        /// </summary>
        /// <param name="closedCurve">The closed curve.</param>
        /// <param name="height">The height. By Default is equal to 1.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        public bool CutSolidsByPatch(Curve closedCurve, double height = 1, string layer = "_CivilConnectionSolid")
        {
            Utils.LogMethodStart(this);

            var curveData = GeometryConverter.ToPolylineData(closedCurve as PolyCurve);

            curveData.IsClosed = true;

            var handle = _geometryService.CutSolidByPatch(_document, curveData, height, layer);

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Creates an extrusion based on a profile defined by a set of curves profile to be used to cut the solids in the Civil Document.
        /// </summary>
        /// <param name="closedCurves">The closed curves.</param>
        /// <param name="height">The height. By Default is equal to 1.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        public bool CutSolidsByCurves(IList<Curve> closedCurves, double height = 1, string layer = "_CivilConnectionSolid")
        {
            Utils.LogMethodStart(this);

            var curveData = GeometryConverter.ToPolylineData(closedCurves as PolyCurve);

            curveData.IsClosed = true;

            var handle = _geometryService.CutSolidByPatch(_document, curveData, height, layer);

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Creates a solid to be used to cut the solids in the Civil 3D Document.
        /// </summary>
        /// <param name="geometry">The solid geometry.</param>
        /// <param name="layer">The layer where to crerate the cutting solid.</param>
        /// <returns></returns>
        public bool CutSolidsByGeometry(Geometry[] geometry, string layer)
        {

            Utils.LogMethodStart(this);

            // #TODO

            Utils.LogMethodEnd(this);

            return false;
        }

        /// <summary>
        /// Import the geometry from Dynamo into the Civil 3D Document.
        /// </summary>
        /// <param name="geometry">The geometry.</param>
        /// <param name="layer">The layer where to crerate the solid.</param>
        /// <returns></returns>
        public IList<string> ImportGeometry(Geometry[] geometry, string layer)
        {
            return Utils.ImportGeometry(this._document, geometry, layer);
        }

        /// <summary>
        /// Links the geometry associated to a Revit object into Civil 3D.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        public string LinkElement(Revit.Elements.Element element, string parameter, string layer)
        {
            return Utils.ImportElement(this._document, element, parameter, layer);
        }



        /// <summary>
        /// Slices the solids in Civil 3D using a Dynamo Plane.
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <returns></returns>
        public bool SliceSolidsByPlane(Plane plane)
        {
            return Utils.SliceSolidsByPlane(this._document, plane);
        }

        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Creates a LandXML from the Civil Document
        /// </summary>
        /// <returns></returns>
        private string DumpLandXML()
        {
            return Utils.DumpLandXML(this._document);
        }

        /// <summary>
        /// Gets the land featurelines.
        /// </summary>
        /// <param name="xmlPath">The XML path for the LandFeaturerline properties.</param>
        /// <returns></returns>
        /// 
        [IsVisibleInDynamoLibraryAttribute(false)]
        public IList<LandFeatureline> GetLandFeaturelines(string xmlPath = "")
        {
            Utils.Log(string.Format("CivilDocument.GetLandFeaturelines started...", ""));

            if (string.IsNullOrEmpty(xmlPath))
            {
                xmlPath = System.IO.Path.Combine(Environment.GetEnvironmentVariable("TMP", EnvironmentVariableTarget.User), "LandFeatureLinesReport.xml");
            }

            if (!Utils.EnsureCivilPythonInstalled())
            {
                return null;
            }

            this.SendCommand("-ExportLandFeatureLinesToXml\n");

            System.DateTime start = System.DateTime.Now;


            while (true)
            {
                if (System.IO.File.Exists(xmlPath))
                {
                    if (System.IO.File.GetLastWriteTime(xmlPath) > start)
                    {
                        start = System.IO.File.GetLastWriteTime(xmlPath);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Utils.Log("XML acquired.");

            bool result = true;

            IList<LandFeatureline> output = new List<LandFeatureline>();

            if (result)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);

                AcadDatabase db = this._document as AcadDatabase;
                AcadModelSpace ms = db.ModelSpace;

                foreach (AcadEntity e in ms)
                {
                    if (e.EntityName.ToLower().Contains("featureline"))
                    {
                        AeccLandFeatureLine f = e as AeccLandFeatureLine;

                        XmlElement fe = xmlDoc.GetElementsByTagName("FeatureLine").Cast<XmlElement>().First(x => x.Attributes["Handle"].Value == f.Handle.ToString());

                        IList<Point> points = new List<Point>();

                        foreach (XmlElement p in fe.GetElementsByTagName("Point"))
                        {
                            double x = Convert.ToDouble(p.Attributes["X"].Value, CultureInfo.InvariantCulture);
                            double y = Convert.ToDouble(p.Attributes["Y"].Value, CultureInfo.InvariantCulture);
                            double z = Convert.ToDouble(p.Attributes["Z"].Value, CultureInfo.InvariantCulture);

                            points.Add(Point.ByCoordinates(x, y, z));
                        }

                        points = Point.PruneDuplicates(points);

                        if (points.Count > 1)
                        {
                            PolyCurve pc = PolyCurve.ByPoints(points);
                            string style = fe.Attributes["Style"].Value;

                            output.Add(new LandFeatureline(f, pc, style));
                        }

                        foreach (var item in points)
                        {
                            if (item != null)
                            {
                                item.Dispose();
                            }
                        }
                    }
                }

                Utils.Log(string.Format("CivilDocument.GetLandFeaturelines completed.", ""));
            }

            return output;
        }
        #endregion

        #region PUBLIC METHODS
        

        #region AUTOCAD METHODS
        

        

        

        #endregion

        #region CIVIL 3D METHODS

        /// <summary>
        /// Adds the civil point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public string AddCivilPoint(Point point)
        {
            return Utils.AddCivilPointByPoint(this._document, point);
        }

        

        

        /// <summary>
        /// Adds the civil point group.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string AddCivilPointGroup(Point[] points, string name)
        {
            return Utils.AddPointGroupByPoint(this._document, points, name);
        }

        /// <summary>
        /// Gets the Civil point groups.
        /// </summary>
        /// <returns></returns>
        //[MultiReturn(new string[] { "PointGroupNames", "Points" })]
        //public Dictionary<string, object> GetPointGroups()
        //{
        //    var dict = Utils.GetPointGroups(this._document);

        //    return new Dictionary<string, object>() { { "PointGroupNames", dict.Keys }, { "Points", dict.Values } };
        //}


        /// <summary>
        /// Adds the tin surface by points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="name">The name.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        //[IsVisibleInDynamoLibrary(false)]
        public string AddTINSurfaceByPoints(Point[] points, string name, string layer)
        {
            return Utils.AddTINSurfaceByPoints(this._document, points, name, layer);
        }

        /// <summary>
        /// Add PropertySetDefinition to document.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string AddPropertySetDefinition(string path)
        {
            return Utils.CreatePropertySetDefinition(this._document, path);
        }

        /// <summary>
        /// Create propertyset and setting values for objects
        /// </summary>
        /// <param name="psetDefinitionName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public string CreatePropertySets(string psetDefinitionName, string path)
        {
            return Utils.CreatePropertySets(_document, psetDefinitionName, path);
        }

        /// <summary>
        /// Adds a property set definition to the specified objects using the provided parameters and property set
        /// definition name.
        /// </summary>        
        /// <param name="objectHandles">A list of object handles representing the target objects to which the property set will be applied.</param>
        /// <param name="parameters">A list of parameter lists, where each inner list contains parameters to be associated with the corresponding
        /// object.</param>
        /// <param name="propertySetDefName">The name of the property set definition to create and assign to the objects.</param>
        /// <returns>Result of the operation. Returns an error message if the operation fails</returns>
        public string AddPropertySetToObjects(List<object> objectHandles, List<List<Parameter>> parameters, string propertySetDefName)
        {
            string output = string.Empty;

            // Step 1 - GeneratePropSetDefXML
            string psdOutput = RevitUtils.GeneratePropSetDefXML(parameters.FirstOrDefault(), propertySetDefName); // Error is handled within the function

            if (!Utils.IsFilePath(psdOutput))
                return psdOutput;

            // Step 2 - AddPropertySetDefinition
            string psdName = Utils.CreatePropertySetDefinition(this._document, psdOutput);

            if (psdName != propertySetDefName)
                return psdName;

            // Step 3 - GenerateObjectXML
            string objXmlOutput = RevitUtils.GenerateObjectXML(objectHandles, parameters);

            if (!Utils.IsFilePath(objXmlOutput))
                return objXmlOutput;

            // Step 4 - CreatePropertySets
            output = Utils.CreatePropertySets(this._document,psdName, objXmlOutput);

            return output;
        }
        #endregion

        /// <summary>
        /// Public textual representation of the Dynamo node preview
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("CivilDocument(Name = {0})", this.Name);
        }
        #endregion
    }
}
