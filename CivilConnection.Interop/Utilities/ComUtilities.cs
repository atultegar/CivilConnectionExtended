using System.Runtime.InteropServices;

namespace CivilConnection.Interop.Utilities
{
    public static class ComUtilities
    {
        public static void Release(object? obj)
        {
            try
            {
                if (obj != null && Marshal.IsComObject(obj))
                {
                    Marshal.ReleaseComObject(obj);
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
