using CivilConnection.Contracts.Models;
using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Interop.Wrappers.Base;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;

namespace CivilConnection.Interop.Wrappers
{
    public class DocumentWrapper : ComWrapper
    {
        public DocumentWrapper(object document) : base(document)
        {
            ObjectName = "AeccRoadwayDocument";
        }

        public string Name => (string)ComObject.Name;

        public string FullName => (string)ComObject.FullName;

        public string Path => (string)ComObject.Path;

        public string Version
        {
            get
            {
                var nameString = (string)ComObject.Application.Name;

                return nameString.Substring(nameString.Length - 4, 4);
            }
        }

        public ModelSpaceWrapper ModelSpace => new ModelSpaceWrapper(ComObject.ModelSpace);

        public IEnumerable<EntityWrapper> GetModelSpaceEntities()
        {
            dynamic modelSpace = ComObject.ModelSpace;

            for (int i = 0; i < modelSpace.Count; i++)
            {
                yield return new EntityWrapper(modelSpace.Item(i));
            }
        }

        public IEnumerable<AlignmentWrapper> AlignmentsSiteless
        {
            get
            {
                var alignments = ComObject.AlignmentsSiteless;

                foreach (var alignment in alignments)
                {
                    yield return new AlignmentWrapper(alignment);
                }
            }
        }

        public IEnumerable<CorridorWrapper> Corridors
        {
            get
            {
                foreach (var corridor in ComObject.Corridors)
                {
                    yield return new CorridorWrapper(corridor);
                }
            }
        }

        public IEnumerable<CivilSurfaceWrapper> Surfaces
        {
            get
            {
                var surfaces = ComObject.Surfaces;

                foreach (var surface in surfaces)
                {
                    yield return new CivilSurfaceWrapper(surface);
                }
            }
        }

        public dynamic SurfaceStyles => ComObject.SurfaceStyles;

        //public CivilSurfaceWrapper AddTinSurface(TinCreationDataWrapper data)
        //{
        //    var surface = ComObject.Surfaces.AddTinSurface(data);

        //    return new CivilSurfaceWrapper(surface);
        //}

        public IEnumerable<CivilPointWrapper> Points
        {
            get
            {
                foreach (var point in ComObject.Points)
                {
                    yield return new CivilPointWrapper(point);
                }
            }
        }

        public IEnumerable<CivilPointGroupWrapper> PointGroups
        {
            get
            {
                foreach (var group in ComObject.PointGroups)
                {
                    yield return new CivilPointGroupWrapper(group);
                }
            }
        }

        public DrawingUnitType DrawingUnits
        {
            get
            {
                dynamic units = ComObject.Settings.DrawingSettings.UnitZoneSettings.DrawingUnits;

                return units switch
                {
                    1 => DrawingUnitType.Feet,
                    2 => DrawingUnitType.Meters,
                    3 => DrawingUnitType.Millimeters,
                    4 => DrawingUnitType.Centimeters,
                    5 => DrawingUnitType.Decimeters,
                    6 => DrawingUnitType.Inches,
                    _ => throw new NotSupportedException($"Unsupported drawing unit: {units}")
                };
            }
        }

        public int DistancePrecision =>
            (int)ComObject.Settings.DrawingSettings.AmbientSettings.DistanceSettings.Precision.Value;

        public void SendCommand(string command) => ComObject.SendCommand(command);

        public dynamic HandleToObject(string handle) => (dynamic)ComObject.HandleToObject(handle); 

        public dynamic AlignmentStyles
        {
            get { return ComObject.AlignmentStyles; }
        }

        public dynamic AlignmentLabelStyleSets
        {
            get { return ComObject.AlignmentLabelStyleSets; }
        }

        public AlignmentWrapper AddAlignmentFromPolylineEx(
            string name, 
            string layer, 
            object polyline, 
            object alignmentStyle, 
            object alignmentLabelStyleSet, 
            bool addCurvesBetweenTangents, 
            bool eraseExistingEntities)
        {
            return new AlignmentWrapper(ComObject.AlignmentsSiteless.AddFromPolylineEx(
                name,
                layer,
                polyline,
                alignmentStyle,
                alignmentLabelStyleSet,
                addCurvesBetweenTangents,
                eraseExistingEntities));
        }

        public void Import (string path, PointData insertionPoint, double scaleFactor)
        {
            var g = ComObject.Import(path, insertionPoint, scaleFactor);            
        }

        public SolidWrapper GetSolidByHandle(string handle)
        {
            var obj = HandleToObject(handle);

            if (obj == null)
                return null;

            return new SolidWrapper(obj);
        }

        public IList<SolidWrapper> GetSolids()
        {
            var output = new List<SolidWrapper>();

            foreach (var entity in ModelSpace.ComObject)
            {
                if (entity.EntityName.Contains("Solid"))
                {
                    output.Add(new SolidWrapper(entity));
                }
            }

            return output;
        }

        public void DeleteObject(string handle)
        {
            var obj = HandleToObject(handle);

            if (obj != null)
            {
                obj.Delete();
            }
        }

        public CivilPointWrapper AddCivilPoint(PointData point)
        {
            var p = ComObject.Points.Add(point.ToArray());

            return new CivilPointWrapper(p);
        }

        public CivilPointGroupWrapper AddPointGroup(string groupName)
        {
            var g = ComObject.PointGroups.Add(groupName);

            return new CivilPointGroupWrapper(g);
        }

        public override string ToString()
        {
            return $"Document (Name = {Name})";
        }
    }
}
