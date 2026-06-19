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

using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using CivilConnection.Contracts.Models;
using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Contracts.Models.Requests;
using CivilConnection.Converters;
using CivilConnection.Interop.Context;
using CivilConnection.Interop.Models;
using CivilConnection.Interop.Services;
using CivilConnection.Interop.Wrappers;
using CivilConnection.Services;
using Revit.Elements;
using System;
using System.Collections.Generic;
using System.Linq;

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

        private readonly DocumentService _documentService;

        private readonly LandXmlService _landXmlService;

        private readonly CivilPythonService _civilPythonService;
    
        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the CivilDocument class with the specified context, document, and data.
        /// </summary>
        /// <param name="document">The underlying dynamic document object to be associated with this CivilDocument instance.</param>
        internal CivilDocument(DocumentWrapper document)
        {
            _document = document;

            _documentService = new DocumentService();

            _alignmentService = new AlignmentService();

            _corridorService = new CorridorService();

            _surfaceService = new CivilSurfaceService();

            _commandService = new CommandService();

            _geometryService = new GeometryService();

            _landXmlService = new LandXmlService();

            _civilPythonService = new CivilPythonService();
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
        internal DocumentWrapper InternalElement => _document;

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
            Utils.LogMethodStart(this);

            try
            {
                var corridors = _corridorService
                    .GetCorridors(_document)
                    .Select(x => new Corridor(x))
                    .ToList();

                Utils.LogMethodEnd(this);

                return corridors;
            }
            catch (Exception ex)
            {

                Utils.Log($"ERROR: {ex.Message}");
                return new List<Corridor>();
            }
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

            var output = _surfaceService
                .GetSurfaces(_document)
                .Select(x => new CivilSurface(x))
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

            return handles.ToList();
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

            var pointsData = points.Select(GeometryConverter.ToPointData).ToList();

            var handle = _geometryService.AddLWPolylineByPoints(_document, pointsData, layer);

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
        /// <param name="layer">The layer where to create the cutting solid.</param>
        /// <returns></returns>
        public bool CutSolidsByGeometry(Geometry[] geometry, string layer = "_CivilConnectionSolid")
        {
            Utils.LogMethodStart(this);

            var satFiles = new List<string>();

            try
            {
                foreach (var g in geometry)
                {
                    if (g is Solid solid)
                    {
                        var sat = DynamoGeometryService.ExportSolidToSat(solid);

                        satFiles.Add(sat);
                    }
                }

                var request = new GeometryImportRequest
                {
                    SatFiles = satFiles,
                    DeleteSatFilesAfterImport = true
                };

                return _geometryService.CutSolidBySolid(_document, request, layer);
            }
            finally
            {
                Utils.LogMethodEnd(this);
            }
        }

        /// <summary>
        /// Import the geometry from Dynamo into the Civil 3D Document.
        /// </summary>
        /// <param name="geometry">The geometry.</param>
        /// <param name="layer">The layer where to create the solid.</param>
        /// <returns></returns>
        public IList<string> ImportGeometry(Geometry[] geometry, string layer = "_CivilConnectionSolid")
        {
            var geometryData = new List<IGeometryData>();
            var satFiles = new List<string>();

            foreach (var g in geometry)
            {
                if (g is Solid solid)
                {
                    var sat = DynamoGeometryService.ExportSolidToSat(solid);

                    satFiles.Add(sat);
                }
                else
                {
                    geometryData.Add(GeometryConverter.Convert(g as Curve));
                }
            }

            var geometryRequest = new GeometryImportRequest
            {
                Geometry = geometryData,
                SatFiles = satFiles,
                DeleteSatFilesAfterImport = true
            };

            return _geometryService.ImportGeometry(_document, geometryRequest, layer);
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
            Utils.LogMethodStart(this);

            var totalTransform = RevitUtils.DocumentTotalTransform();

            var totalTransformInverse = RevitUtils.DocumentTotalTransformInverse();

            try
            {
                var existingHandles = RevitUtils.GetHandlesFromParameter(element, parameter);

                var satFiles = DynamoGeometryService.ExportToSat(element);

                if (!satFiles.Any())
                {
                    Utils.Log($"No SAT files were generated.");
                    return string.Empty;
                }

                var request = new SolidImportRequest
                {
                    ExistingHandles = existingHandles,
                    SatFiles = satFiles,
                    Layer = layer,
                    ReplaceExisting = existingHandles.Any()
                };

                string handles = _documentService.ImportSolid(_document, request);

                element.SetParameterByName(parameter, handles);

                Utils.LogMethodEnd(this);

                return handles;
            }
            catch (Exception ex)
            {
                Utils.Log($"ERROR: {ex}");

                throw;
            }
        }

        /// <summary>
        /// Slices the solids in Civil 3D using a Dynamo Plane.
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <returns></returns>
        public bool SliceSolidsByPlane(Plane plane)
        {
            Utils.LogMethodStart(this);

            var planeData = GeometryConverter.ToPlaneData(plane);

            var output = _geometryService.SliceSolidsByPlane(_document, planeData, true);

            Utils.LogMethodEnd(this);

            return output;
        }

        #endregion

        #region PRIVATE METHODS
        
        /// <summary>
        /// Gets the land featurelines.
        /// </summary>
        /// <param name="xmlPath">The XML path for the LandFeaturerline properties.</param>
        /// <returns></returns>
        /// 
        [IsVisibleInDynamoLibrary(false)]
        public IList<LandFeatureline> GetLandFeaturelines(string xmlPath = "")
        {
            Utils.LogMethodStart(this);

            if (string.IsNullOrWhiteSpace(xmlPath))
            {
                xmlPath = _documentService.ExportLandFeaturelinesXml(_document);
            }

            var data = _landXmlService.ReadLandFeaturelines(xmlPath);

            var output = data.Select(x => FeaturelineConverter.ToDynamo(x, _document)).ToList();

            Utils.LogMethodEnd(this);

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
            Utils.LogMethodStart(this);

            var handle = _documentService.AddCivilPoint(_document, GeometryConverter.ToPointData(point));

            Utils.LogMethodEnd(this);

            return handle;
        }

        /// <summary>
        /// Adds the civil point group.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string AddCivilPointGroup(Point[] points, string name)
        {
            Utils.LogMethodStart(this);

            var pointsData = points.Select(GeometryConverter.ToPointData).ToList();

            var pointGroupWrapper = _documentService.AddPointGroup(_document, pointsData, name);

            Utils.LogMethodEnd(this);

            return pointGroupWrapper.Name;
        }

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
            Utils.LogMethodStart(this);

            var pointsData = points.Select(GeometryConverter.ToPointData).ToList();

            var tinSurfaceWrapper = _surfaceService.CreateTinSurfaceFromPoints(_document, pointsData, name, layer);

            Utils.LogMethodEnd(this);

            return tinSurfaceWrapper.Handle;
        }

        /// <summary>
        /// Add PropertySetDefinition to document.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string AddPropertySetDefinition(string path)
        {
            Utils.LogMethodStart(this);

            var output = _civilPythonService.CreatePropertySetDefinition(_document, path);

            Utils.LogMethodEnd(this);

            return output;
        }

        /// <summary>
        /// Create propertyset and setting values for objects
        /// </summary>
        /// <param name="psetDefinitionName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public string CreatePropertySets(string psetDefinitionName, string path)
        {
            Utils.LogMethodStart(this);

            var output = _civilPythonService.CreatePropertySets(_document, psetDefinitionName, path);

            Utils.LogMethodEnd(this);

            return output;
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
            string psdName = _civilPythonService.CreatePropertySetDefinition(_document, psdOutput);


            if (psdName != propertySetDefName)
                return psdName;

            // Step 3 - GenerateObjectXML
            string objXmlOutput = RevitUtils.GenerateObjectXML(objectHandles, parameters);

            if (!Utils.IsFilePath(objXmlOutput))
                return objXmlOutput;

            // Step 4 - CreatePropertySets
            output = _civilPythonService.CreatePropertySets(_document, psdName, objXmlOutput);

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
            return $"CivilDocument(Name = {Name})";
        }
        #endregion
    }
}
