using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Models;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
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

        public void FreezeLayers(DocumentWrapper document, string layer)
        {
            var layers = document.ComObject.Layers;

            foreach (var l in layers)
            {
                if (string.Equals(l.Name, layer, StringComparison.OrdinalIgnoreCase))
                    l.Freeze = true;
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

        public string AddLWPolylineByPoints(DocumentWrapper doc, IList<PointData> points, string layer)
        {
            AddLayer(doc, layer);

            dynamic modelSpace = doc.ComObject.ModelSpace;

            double[] vlist = new double[2 * points.Count];

            for (int i = 0; i < points.Count; ++i)
            {
                vlist[2 * i] = points[i].X;
                vlist[2 * i + 1] = points[i].Y;
            }

            var pl = modelSpace.AddLightWeightPolyline(vlist);
            pl.Layer = layer;

            return pl.Handle;
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

            dynamic region = document.HandleToObject(regionHandle);

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

            var targetSolids = GetSolids(document, layer);

            if (!targetSolids.Any())
                return false;

            string cutterHandle = null;
            
            try
            {
                cutterHandle = ExtrudeCurve(document, closedCurve, height, layer);

                if (string.IsNullOrWhiteSpace(cutterHandle))
                    return false;

                var cutterSolid = document.GetSolidByHandle(cutterHandle);

                if (cutterSolid == null) 
                    return false;

                bool success = false;

                foreach (var targetSolid in targetSolids)
                {
                    try
                    {
                        if (!targetSolid.CheckInterference(cutterSolid))
                            success = false;

                        targetSolid.Subtract(cutterSolid);

                        success = true;
                    }
                    catch
                    {
                        
                    }
                }

                return success;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(cutterHandle))
                {
                    try
                    {
                        document.DeleteObject(cutterHandle);
                    }
                    catch { }
                }                
            }                      
        }

        public bool CutSolidBySolid(DocumentWrapper document, GeometryImportRequest request, string layer)
        {
            ValidateDocument(document);


            AddLayer(document, layer);
                        
            var targetSolids = GetSolids(document, layer);

            if (!targetSolids.Any())
                return false;

            string cutterHandle = null;

            try
            {
                cutterHandle = ImportGeometry(document, request, layer).FirstOrDefault();

                if (string.IsNullOrWhiteSpace(cutterHandle))
                    return false;

                var cutterSolid = document.GetSolidByHandle(cutterHandle);

                if (cutterSolid == null)
                    return false;

                bool success = false;

                foreach (var targetSolid in targetSolids)
                {
                    try
                    {
                        if (!targetSolid.CheckInterference(cutterSolid))
                            success = false;

                        targetSolid.Subtract(cutterSolid);

                        success = true;
                    }
                    catch
                    {

                    }
                }

                return success;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(cutterHandle))
                {
                    try
                    {
                        document.DeleteObject(cutterHandle);
                    }
                    catch { }
                }
            }
        }

        public bool SliceSolidsByPlane(DocumentWrapper document, PlaneData plane, bool keepBothSides)
        {
            bool success = false;

            var origin = plane.Origin;

            var xPoint = new PointData
            {
                X = origin.X + plane.XAxis.X,
                Y = origin.Y + plane.XAxis.Y,
                Z = origin.Z + plane.XAxis.Z
            };

            var yPoint = new PointData
            {
                X = origin.X + plane.YAxis.X,
                Y = origin.Y + plane.YAxis.Y,
                Z = origin.Z + plane.YAxis.Z
            };

            foreach (var solid in document.GetSolids())
            {
                try
                {
                    solid.Slice(origin, xPoint, yPoint, keepBothSides);

                    success = true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error: {ex}");
                }
            }

            return success;
        }

        public IList<string> ImportGeometry(DocumentWrapper document, GeometryImportRequest request, string layer)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            if (request == null)
                throw new ArgumentNullException(nameof(document));

            AddLayer(document, layer);

            var handles = new List<string>();

            foreach (var item in request.Geometry ?? Enumerable.Empty<IGeometryData>())
            {
                string handle = item switch
                {
                    PointData point => AddDBPoint(document, point, layer),

                    LineData line => AddLine(document, line, layer),

                    ArcData arc => AddArc(document, arc, layer),

                    CircleData circle => AddCircle(document, circle, layer),

                    PolylineData polyline => AddPolyline(document, polyline, layer)
                };

                if (!string.IsNullOrWhiteSpace(handle))
                {
                    handles.Add(handle);
                }
            }

            foreach (var satFile in request.SatFiles ?? Enumerable.Empty<string>())
            {
                handles.AddRange(
                    ImportSAT(document, satFile, layer));
            }

            return handles;
        }

        public IList<string> ImportSAT(DocumentWrapper document, string satPath, string layer, bool deleteAfterImport = false)
        {
            if (!File.Exists(satPath))
                throw new FileNotFoundException($"SAT file not found: {satPath}");

            var handles = new List<string>();

            try
            {
                var modelSpace = document.ModelSpace;

                int startCount = modelSpace.Count;

                document.Import(satPath, new PointData(), 1);

                int endCount = modelSpace.Count;

                for (int i = startCount; i < endCount; i++)
                {
                    dynamic entity = modelSpace.Item(i);

                    string entityName = entity.EntityName;

                    if (entityName.Contains("Solid") || entityName.Contains("Surface"))
                    {
                        entity.Layer = layer;

                        handles.Add(entity.Handle);
                    }
                }

                return handles;
            }
            finally
            {
                if (deleteAfterImport && File.Exists(satPath))
                    File.Delete(satPath);
            }
        }

        public void TransformEntity(DocumentWrapper document, string handle, CoordinateSystemData cs)
        {
            dynamic entity = document.HandleToObject(handle);

            entity.TransformBy(cs.ToTransformMatrix());
        }

        public bool RotateByVector(DocumentWrapper doc, string handle, VectorData vector)
        {
            dynamic entity = doc.HandleToObject(handle);

            var insertionPoint = new[]
            {
                (double)entity.InsertionPoint[0],
                (double)entity.InsertionPoint[1],
                (double)entity.InsertionPoint[2]
            };

            double angle = Math.Atan2(vector.Y, vector.X);

            entity.Rotate(insertionPoint, angle);

            return true;
        }

        // TODO: in progress
        //public bool AlignToPlane(DocumentWrapper document, string handle, PlaneData plane)
        //{

        //    dynamic entity = document.HandleToObject(handle);

        //    var insertionPoint = new[]
        //    {
        //        (double)entity.InsertionPoint[0],
        //        (double)entity.InsertionPoint[1],
        //        (double)entity.InsertionPoint[2]
        //    };

        //    var entityNormal = new VectorData
        //    {
        //        X = entity.Normal[0],
        //        Y = entity.Normal[1],
        //        Z = entity.Normal[2]
        //    };

        //    var targetNormal = VectorMath.Normalize(plane.)

        //    return true;
        //}

        #endregion


        #region PRIVATE METHODS

        private void ValidateDocument(DocumentWrapper document)
        {
            if (document == null)
            {
                throw new Exception("Civil document is null");
            }
        }

        private List<SolidWrapper> GetSolids(DocumentWrapper document, string excludedLayer = null)
        {
            var solids = new List<SolidWrapper>();

            foreach (var solid in document.GetSolids())
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(excludedLayer) &&
                        solid.Layer.Equals(excludedLayer))
                        continue;

                    solids.Add(solid);
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

        public double DegToRad(double angle)
        {
            return angle / 180 * Math.PI;
        }

        #endregion
    }
}
