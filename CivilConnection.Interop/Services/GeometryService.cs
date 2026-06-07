using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection.Interop.Services
{
    public class GeometryService
    {
        #region AUTOCAD GEOMETRY

        public void AddLayer(DocumentWrapper document, string layerName)
        {
            ValidateDocument(document);

            if (string.IsNullOrWhiteSpace(layerName))
                return;

            dynamic acadLayers = document.ComObject.Layers;

            foreach (dynamic layer in acadLayers)
            {
                if (string.Equals(
                    (string)layer.Name,
                    layerName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            acadLayers.Add(layerName);
        }

        public void Addlayers(DocumentWrapper document, IEnumerable<string> layers)
        {
            if (layers == null)
                return;

            
            foreach (string layer in layers
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase))
            {
               AddLayer(document, layer);
            }
        }

        public string AddGeometry(DocumentWrapper document, IGeometryData geometry, string layer)
        {
            switch (geometry)
            {
                case ArcData arc:
                    return AddArc(document, arc, layer);

                case CircleData circle:
                    return AddCircle(document, circle, layer);

                case PolylineData polyline:
                    return AddPolyline(document, polyline, layer);

                case LineData line:
                    return AddLine(document, line, layer);

                default:
                    throw new NotSupportedException(geometry.GetType().Name);
            }
        }

        public string AddDBPoint(DocumentWrapper document, PointData point, string layer)
        {
            ValidateDocument(document);

            dynamic modelSpace = document.ComObject.ModelSpace;

            AddLayer(document, layer);

            dynamic dbPoint = modelSpace.AddPoint(point.ToArray());

            dbPoint.Layer = layer;

            return dbPoint.Handle;
        }

        public IList<string> AddDBPoints(DocumentWrapper document, IList<PointData> points, IList<string> layers)
        {            
            ValidateDocument(document);

            if (points == null)
                throw new ArgumentNullException(nameof(points));

            if (layers == null)
                throw new ArgumentNullException(nameof(layers));

            if (points.Count != layers.Count)
            {
                throw new ArgumentException("Points count must match layers count.");
            }

            Addlayers(document, layers);

            var handles = new List<string>();

            dynamic modelSpace = document.ComObject.ModelSpace;

            for (int i = 0; i < points.Count; i++)
            {
                
                dynamic dbPoint = modelSpace.AddPoint(points[i].ToArray());

                dbPoint.Layer = layers[i];

                handles.Add((string)dbPoint.Handle);
            }

            return handles;
        }

        public string AddLine(DocumentWrapper document, LineData lineData, string layer)
        {
            ValidateDocument(document);

            AddLayer(document, layer);

            dynamic modelSpace = document.ComObject.ModelSpace;

            dynamic line = modelSpace.AddLine(lineData.StartPoint.ToArray(), lineData.EndPoint.ToArray());

            line.Layer = layer;

            return line.Handle;
        }

        public string AddPolyline(DocumentWrapper document, PolylineData polyline, string layer)
        {
            ValidateDocument(document);

            AddLayer(document, layer);

            dynamic modelSpace = document.ComObject.ModelSpace;

            dynamic pline = modelSpace.Add3DPoly(polyline.Vertices);

            pline.Layer = layer;
            pline.Closed = polyline.IsClosed;

            return pline.Handle;
        }

        public string AddCircle(DocumentWrapper document, CircleData circleData, string layer)
        {
            ValidateDocument(document);

            AddLayer(document, layer);

            dynamic modelSpace = document.ComObject.ModelSpace;

            dynamic circle = modelSpace.AddCircle(circleData.Center.ToArray(), circleData.Radius);

            circle.Layer = layer;

            if (circleData.Transform != null)
            {
                circle.TransformBy(circleData.Transform);
            }

            return circle.Handle;
        }

        public string AddArc(DocumentWrapper document, ArcData arcData, string layer)
        {
            ValidateDocument(document);

            dynamic modelSpace = document.ComObject.ModelSpace;

            AddLayer(document, layer);

            dynamic arc = modelSpace.AddArc(
                arcData.Center.ToArray(),
                arcData.Radius,
                arcData.StartAngleRadians,
                arcData.EndAngleRadians);

            arc.Layer = layer;

            if (arcData.Transform != null)
            {
                arc.TransformBy(arcData.Transform);
            }

            return arc.Handle;
        }

        public string AddRegion(DocumentWrapper document, PolylineData curveData, string layer)
        {
            ValidateDocument(document);

            dynamic modelSpace = document.ComObject.ModelSpace;

            AddLayer(document, layer);

            string polylineHandle = AddPolyline(document, curveData, layer);

            dynamic polyline = document.ComObject.HandleToObject(polylineHandle);

            dynamic exploded = polyline.Explode();

            var entities = new object[exploded.Length];

            for (int i = 0; i < exploded.Length; i++)
            {
                entities[i] = exploded[i];
            }

            dynamic regions = modelSpace.AddRegion(entities);

            dynamic region = regions[0];

            region.Layer = layer;

            polyline.Delete();

            foreach (dynamic entity in entities)
            {
                entity.Delete();
            }

            return region.Handle;
        }

        public string ExtrudeCurve(DocumentWrapper document, PolylineData curveData, double height, string layer)
        {
            ValidateDocument(document);

            AddLayer(document, layer);

            string regionHandle = AddRegion(document, curveData, layer);

            dynamic region = document.ComObject.HandleToObject(regionHandle);

            dynamic modelSpace = document.ComObject.ModelSpace;

            dynamic solid = modelSpace.AddExtrudedSolid(region, height, 0);

            solid.Layer = layer;

            region.Delete();

            return solid.Handle;
        }

        public string AddText(DocumentWrapper document, string text, PointData pointData, double height, string layer)
        {
            ValidateDocument(document);

            AddLayer(document, layer);

            dynamic modelSpace = document.ComObject.ModelSpace;

            var acadText = modelSpace.AddText(text, pointData.ToArray(), height);

            acadText.Layer = layer;

            // #TODO : add transformation

            return acadText.Handle;
        }

        public bool CutSolidByPatch(DocumentWrapper document, PolylineData closedCurve, double height, string layer)
        {
            ValidateDocument(document);

            AddLayer(document, layer);

            dynamic modelSpace = document.ComObject.ModelSpace;

            var targetSolids = GetSolids(modelSpace, layer);

            if (!targetSolids.Any())
                return false;

            dynamic cutterSolid = null;
            
            try
            {
                string cutterHandle = ExtrudeCurve(document, closedCurve, height, layer);

                cutterSolid = document.ComObject.HandleToObject(cutterHandle);

                bool result = false;

                foreach (dynamic targetSolid in targetSolids)
                {
                    if (SubtractSolid(targetSolid, cutterSolid))
                        result = true;
                }

                return result;
            }
            finally
            {
                try
                {
                    cutterSolid?.Delete();
                }
                catch { }
                
            }                      
        }

        public bool CutSolidBySolid(DocumentWrapper document, IGeometryData geometry, string layer)
        {
            ValidateDocument(document);

            AddLayer(document, layer);

            dynamic modelSpace = document.ComObject.ModelSpace;

            var targetSolids = GetSolids(modelSpace, layer);

            if (!targetSolids.Any())
                return false;

            dynamic cutterSolid = null;

            //try
            //{
            //    string cutterHandle = ExtrudeCurve(document, closedCurve, height, layer);

            //    cutterSolid = document.HandleToObject(cutterHandle);

            //    bool result = false;

            //    foreach (dynamic targetSolid in targetSolids)
            //    {
            //        if (SubtractSolid(targetSolid, cutterSolid))
            //            result = true;
            //    }

            //    return result;
            //}
            //finally
            //{
            //    try
            //    {
            //        cutterSolid?.Delete();
            //    }
            //    catch { }

            //}

            return false;
        }



        #endregion


        #region PRIVATE METHODS

        private void ValidateDocument(DocumentWrapper document)
        {
            if (document == null)
            {
                throw new Exception("Civil document is null");
            }
        }

        private List<dynamic> GetSolids(dynamic modelSpace, string excludedLayer = null)
        {
            var solids = new List<dynamic>();

            foreach (dynamic entity in modelSpace)
            {
                try
                {
                    if (!entity.EntityName.Contains("Solid"))
                        continue;

                    if (!string.IsNullOrWhiteSpace(excludedLayer) &&
                        entity.Layer.Equals(excludedLayer))
                        continue;

                    solids.Add(entity);
                }
                catch { }
            }

            return solids;
        }

        public bool SubtractSolid(dynamic targetSolid, dynamic cutterSolid)
        {
            try
            {
                bool interference;

                dynamic intersection = targetSolid.CheckInterference(cutterSolid, true, out interference);

                if (!interference)
                    return false;

                targetSolid.Boolean(2, intersection); //AcBooleanType.acSubtraction

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Subtract solid failed: {ex.Message}");
            }
            
        }

        #endregion
    }
}
