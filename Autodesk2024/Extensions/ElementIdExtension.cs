using Autodesk.DesignScript.Runtime;
using Autodesk.Revit.DB;

namespace CivilConnection.Extensions
{
    [SupressImportIntoVM()]
    [IsVisibleInDynamoLibrary(false)]
    internal static class ElementIdExtension
    {
        internal static long GetValue(this ElementId id)
        {
#if (C2026 || C2027)
            return id.Value;
#else
            return id.IntegerValue;
#endif
        }
    }
}
