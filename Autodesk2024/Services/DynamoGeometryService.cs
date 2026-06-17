using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using System;
using System.Collections.Generic;
using System.IO;

namespace CivilConnection.Services
{
    [SupressImportIntoVM()]
    [IsVisibleInDynamoLibrary(false)]
    internal static class DynamoGeometryService
    {
        /// <summary>
        /// Recursive function to join surfaces into a PolySurface
        /// </summary>
        /// <param name="surfaces">The surface list to process</param>
        /// <param name="limit">The amount of surfaces to join together</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public static IList<Surface> JoinSurfaces(IList<Surface> surfaces, int limit = 100)  // 20190922
        {
            if (surfaces == null || surfaces.Count == 0)
            {
                return new List<Surface>();
            }

            if (surfaces.Count == 1)
            {
                return surfaces;
            }

            var result = new List<Surface>();

            for (int i = 0; i < surfaces.Count; i = i + limit)
            {
                var batch = new List<Surface>();

                for (int j = i; j < i + limit && j < surfaces.Count; j++)
                {
                    batch.Add(surfaces[j]);
                }

                PolySurface ps = PolySurface.ByJoinedSurfaces(batch);

                result.Add(ps);
            }

            return JoinSurfaces(result, limit);            
        }

        public static IList<string> ExportToSat(Revit.Elements.Element element)
        {
            var satFiles = new List<string>();

            foreach (var solid in element.Solids)
            {
                satFiles.Add(ExportSolidToSat(solid));
            }

            return satFiles;
        }

        public static string ExportSolidToSat(Solid solid)
        {
            string sat = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.sat");

            try
            {
                Geometry.ExportToSAT(new[] { solid }, sat);
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR: {ex.Message}");
            }            

            return sat;
        }
        
    }
}
