using System;

namespace CivilConnection.Interop.Runtime
{
    internal static class ComInteropHelper
    {
        [DllImport("ole32.dll")]
        private static extern int CLSIDFromProgIDEx(
            [MarshalAs(UnmanagedType.LPWStr)]
            string progId,
            out Guid clsid);

        [DllImport("ole32.dll")]
        public static extern int GetActiveObjectExt(
            ref Guid rclsid, 
            IntPtr reserved, 
            [MarshalAs(UnmanagedType.Interface)] 
            out object ppunk);

        public static object? GetRunningInstance(string progId)
        {
            try
            {
                var type = Type.GetTypeFromProgID(progId);
                var guid = type.GUID;

                GetActiveObjectExt(ref guid, IntPtr.Zero, out object obj);

                return obj;
            }
            catch
            {
                return null;
            }
        }
    }
}
