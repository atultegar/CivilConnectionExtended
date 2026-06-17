using CivilConnection.Contracts.Models.Geometry;
using CivilConnection.Contracts.Models.Requests;
using CivilConnection.Interop.Interfaces;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Services
{
    public class IfcService : IIfcService
    {
        public string ExportIFC(DocumentWrapper document, IfcExportRequest request)
        {
            if (document == null) 
                throw new ArgumentNullException(nameof(document));

            var entities = GetExportEntities(document, request);

            if (!entities.Any())
                return string.Empty;

            MoveToRevitCoordinates(entities, request.Origin, request.RotationAngle);

            string outputFile = BuildIfcPath(document, request.OutputFolder);

            try
            {
                document.SendCommand($"-IFCEXPORT\nNumber\n\n{outputFile}\ne\n");

                WaitForFile(outputFile);

                return outputFile;
            }
            finally
            {
                RestoreOriginalCoordinates(entities, request.Origin, request.RotationAngle);
            }


        }

        private void RestoreOriginalCoordinates(IEnumerable<EntityWrapper> entities, PointData origin, double angle)
        {
            var zero = new PointData();

            var reverse = new PointData
            {
                X = -origin.X,
                Y = -origin.Y,
                Z = -origin.Z
            };

            foreach (var entity in entities)
            {
                entity.Move(zero, reverse);

                entity.Rotate(zero, angle);
            }
        }

        private void WaitForFile(string file)
        {
            var timeout = DateTime.Now.AddMinutes(2);

            while (DateTime.Now < timeout)
            {
                if (File.Exists(file))
                    return;

                Thread.Sleep(500);
            }
        }

        private string BuildIfcPath(DocumentWrapper document, string outputFolder)
        {
            string name = document.Name;

            return Path.Combine(outputFolder, $"{name}_Origin.ifc");
        }

        private void MoveToRevitCoordinates(IEnumerable<EntityWrapper> entities, PointData origin, double angle)
        {
            var zero = new PointData();

            foreach (var entity in entities)
            {
                entity.Rotate(zero, -angle);

                entity.Move(zero, origin);
            }
        }

        private IList<EntityWrapper> GetExportEntities(DocumentWrapper document, IfcExportRequest request)
        {
            var entities = new List<EntityWrapper>();

            foreach (var entity in document.GetModelSpaceEntities())
            {
                string name = entity.EntityName;

                bool include =
                    (request.ExportSolids && name.Contains("Solid")) ||
                    (request.ExportSurfaces && name.Contains("Surface")) ||
                    (request.ExportBodies && name.Contains("Body")) ||
                    name.Contains("Face") ||
                    name.Contains("MassElement");

                if (include)
                    entities.Add(entity);
            }

            return entities;
        }
    }
}
