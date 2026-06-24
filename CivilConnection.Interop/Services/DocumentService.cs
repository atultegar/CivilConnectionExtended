using CivilConnection.Contracts.Models;
using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Contracts.Models.Requests;
using CivilConnection.Interop.Context;
using CivilConnection.Interop.Converters;
using CivilConnection.Interop.Utilities;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CivilConnection.Interop.Services
{
    public class DocumentService
    {
        private readonly GeometryService _geometryService;
        private readonly CivilPythonService _civilPythonService;

        public DocumentService()
        {
            _geometryService = new GeometryService();
            _civilPythonService = new CivilPythonService();
        }

        public List<CivilDocumentData> GetAll(string? version = null)
        {
            var context = CivilContext.Create(version);

            var output = new List<CivilDocumentData>();
                       

            foreach (dynamic document in context.Host.Application.Documents)
            {
                var wrapper = new DocumentWrapper(document);

                output.Add(DocumentConverter.Convert(wrapper));
            }

            return output;
        }

        public CivilDocumentData ActiveDocument(CivilContext context)
        {
            dynamic document = context.Host.Application.ActiveDocument;

            var wrapper = new DocumentWrapper(document);
            
            return DocumentConverter.Convert(wrapper);
        }

        public List<CivilDocumentData> GetAllDocuments()
        {
            var contexts = CivilContext.CreateAllContext();

            var output = new List<CivilDocumentData>();

            foreach (dynamic context in contexts)
            {
                foreach (dynamic document in context.Host.Application.Documents)
                {
                    var wrapper = new DocumentWrapper(document);

                    output.Add(DocumentConverter.Convert(wrapper));
                }
            }

            return output;
        }

        //public List<CivilDocumentData> GetDocuments(CivilContext context)
        //{
        //    var output = new List<CivilDocumentData>();

        //    dynamic docs = context.Host.Application.Documents;

            

        //    foreach (dynamic doc in docs)
        //    {
        //        var wrapper = new DocumentWrapper(doc);

        //        output.Add(DocumentConverter.Convert(wrapper));
        //    }

        //    return output;
        //}

        public DocumentWrapper GetActiveDocument(CivilContext context)
        {
            var active = context.Host.Application.ActiveDocument;

            return new DocumentWrapper(active);
        }

        public IList<DocumentWrapper> GetDocuments(CivilContext context)
        {
            var output = new List<DocumentWrapper>();

            foreach (var doc in context.Host.Application.Documents)
            {
                output.Add(new DocumentWrapper(doc));
            }

            return output;
        }

        public UnitSettings GetUnitSettings(DocumentWrapper document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            var precision = document.DistancePrecision;

            return new UnitSettings
            {
                DrawingUnit = document.DrawingUnits,
                Precision = precision,
                Accuracy = Math.Pow(10, -precision)
            };
        }

        public AlignmentWrapper CreateAlignmentFromPoints(DocumentWrapper document, string name, IList<PointData> points, string layer)
        {
            var alignments = document.AlignmentsSiteless;

            dynamic alignmentStyle;

            try
            {
                alignmentStyle = document.AlignmentStyles[0];
            }
            catch
            {
                alignmentStyle = document.AlignmentStyles.Add("CivilConnection_AlignmentStyle");
            }

            dynamic labelStyleSet;

            try
            {
                labelStyleSet = document.AlignmentLabelStyleSets[0];
            }
            catch
            {
                labelStyleSet = document.AlignmentLabelStyleSets.Add("CivilConnection_AlignmentLableStyle");
            }

            var polylineHandle = _geometryService.AddLWPolylineByPoints(document, points, layer);

            var polyline = document.HandleToObject(polylineHandle);

            var alignment = document.AddAlignmentFromPolylineEx(name, layer, polyline, alignmentStyle, labelStyleSet, true, false);

            return alignment;
        }

        public string ImportSolid(DocumentWrapper document, SolidImportRequest request)
        {
            if (document == null) 
                throw new ArgumentNullException(nameof(document));

            var existingSolid = request.ReplaceExisting
                ? GetUnionSolid(document, request.ExistingHandles)
                : null;

            var importedHandles = new List<string>();

            foreach (var satFile in request.SatFiles)
            {
                importedHandles.AddRange(_geometryService.ImportSAT(document, satFile, request.Layer));
            }

            string newHandle = MergeImportSolids(document, importedHandles);

            if (existingSolid != null)
            {
                _civilPythonService.ReplaceSolid(document, existingSolid.Handle, newHandle);

                return existingSolid.Handle;
            }

            return newHandle;            
        }

        public string ExportLandFeaturelinesXml(DocumentWrapper document)
        {
            _civilPythonService.ExportLandFeatureLinesToXml(document);

            string xmlPath = Path.Combine(Path.GetTempPath(), "LandFeatureLinesReport.xml");

            Paths.WaitForXml(xmlPath);

            return xmlPath;
        }

        public string AddCivilPoint(DocumentWrapper document, PointData point)
        {
            var civilPoint = document.AddCivilPoint(point);

            return civilPoint.Handle;
        }

        public CivilPointGroupWrapper AddPointGroup(DocumentWrapper document, IEnumerable<PointData> points, string groupName)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            CivilPointGroupWrapper group = null;

            var pointGroups = document.PointGroups;

            if (pointGroups != null)
            {
                if (pointGroups.Count() > 0)
                {
                    group = pointGroups.FirstOrDefault(p => string.Equals(p.Name, groupName, StringComparison.OrdinalIgnoreCase)); 
                }
            }

            if (group == null)
            {
                group = document.AddPointGroup(groupName);
            }

            var pointNumbers = points
                .Select(p => document.AddCivilPoint(p))
                .Where(p => p != null)
                .Select(p => p.Number)
                .ToList();

            group.AddPoints(pointNumbers);

            return group;
        }

        

        #region PRIVATE METHODS
        private SolidWrapper GetUnionSolid(DocumentWrapper document, IList<string> existingHandles)
        {
            SolidWrapper result = null;

            foreach (var handle in existingHandles)
            {
                var solid = document.GetSolidByHandle(handle);

                if (solid == null)
                    continue;

                if (result == null)
                {
                    result = solid;
                }
                else
                {
                    result.Union(solid);
                }
            }

            return result;
        }

        private string MergeImportSolids(DocumentWrapper document, List<string> handles)
        {
            SolidWrapper mergedSolid = null;

            foreach (var handle in handles)
            {
                var solid = document.GetSolidByHandle(handle);

                if (solid == null)
                    continue;

                if (mergedSolid == null)
                {
                    mergedSolid = solid;
                }
                else
                {
                    mergedSolid.Union(solid);
                }
            }

            return mergedSolid?.Handle ?? string.Join(",", handles);
        }

        #endregion
    }
}
