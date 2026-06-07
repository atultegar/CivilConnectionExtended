using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using System.Collections.Generic;

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
    }
}
