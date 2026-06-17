// Copyright (c) 2016 Autodesk, Inc.
// Copyright (c) 2026 Atul Tegar
//
// Original Author: paolo.serra@autodesk.com
// Maintained and extended by: atul.tegar@gmail.com
// 
// Licensed under the Apache License, Version 2.0 (the "License").
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

using System.IO;

using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using CivilConnection.Interop.Services;

namespace CivilConnection
{
    /// <summary>
    /// Collection of utilities.
    /// </summary>
    [SupressImportIntoVM()]
    [IsVisibleInDynamoLibrary(false)]
    internal class Utils
    {
        #region PRIVATE PROPERTIES


        #endregion

        #region PUBLIC PROPERTIES


        #endregion

        #region CONSTRUCTOR


        #endregion

        #region PRIVATE METHODS


        #endregion

        #region PUBLIC METHODS


        /// <summary>
        /// Checks if two values are almost equal
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public static bool AlmostEqual(double a, double b)
        {
            return Math.Abs(a - b) <= 0.00001;
        }


        /// <summary>
        /// Feets to mm.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public static double FeetToMm(double d)
        {
            return d * 304.8;
        }


        /// <summary>
        /// Mms to feet.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public static double MmToFeet(double d)
        {
            return d / 304.8;
        }


        /// <summary>
        /// Feets to m.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public static double FeetToM(double d)
        {
            return d * 0.3048;
        }


        /// <summary>
        /// ms to feet.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public static double MToFeet(double d)
        {
            return d / 0.3048;
        }


        /// <summary>
        /// Degs to RAD.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public static double DegToRad(double angle)
        {
            return angle / 180 * Math.PI;
        }


        /// <summary>
        /// RADs to deg.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public static double RadToDeg(double d)
        {
            return d * 180 / Math.PI;
        }

        /// <summary>
        /// Function that writes an entry to the log file
        /// </summary>
        /// <param name="message"></param>
        [IsVisibleInDynamoLibrary(false)]
        public static void Log(string message)
        {
            string path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("TMP", EnvironmentVariableTarget.User), "CivilConnection_temp.log");

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(string.Format("[{0}] {1}", DateTime.Now, message));
            }
        }

        /// <summary>
        /// Function that writes an exception entry to the log file
        /// </summary>
        /// <param name="ex">The exception</param>
        [IsVisibleInDynamoLibrary(false)]
        public static void Log(Exception ex)
        {
            string path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("TMP", EnvironmentVariableTarget.User), "CivilConnection_temp.log");

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(string.Format("[{0}] EXCEPTION: {1} {2}", DateTime.Now, ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// Function that writes the starting of a method to the log.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        public static void LogMethodStart(object instance, [CallerMemberName] string methodName = "")
        {
            Log($"{instance.GetType().Name}.{methodName} started...");
        }

        /// <summary>
        /// Function that writes the completion of a method to the log.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        public static void LogMethodEnd(object instance, [CallerMemberName] string methodName = "")
        {
            Log($"{instance.GetType().Name}.{methodName} completed...");
        }

        /// <summary>
        /// Finalizes the Log file.
        /// </summary>
        [IsVisibleInDynamoLibrary(false)]
        [SupressImportIntoVM()]
        public static void InitializeLog()
        {
            string path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("TMP", EnvironmentVariableTarget.User), "CivilConnection_temp.log");

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static string ConvertToSnakeCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            return Regex.Replace(input.Trim(), @"\s+", "_").ToUpper();
        }


        internal static bool IsFilePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return false;

            try
            {
                string fullPath = Path.GetFullPath(path);
                return Path.HasExtension(fullPath) && !string.IsNullOrEmpty(Path.GetFileName(fullPath));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks whether the required CivilPython library is installed and available for use.
        /// </summary>
        /// <remarks>If the library is not found, a warning message is displayed to the user and a log
        /// entry is created. This method should be called before attempting to use CivilPython functionality to ensure
        /// the required dependency is present.</remarks>
        /// <returns>true if the CivilPython library is installed; otherwise, false.</returns>
        internal static bool EnsureCivilPythonInstalled()
        {
            string version = GetCivilPythonVersion();

            var civilPythonServie = new CivilPythonService();

            if (civilPythonServie.IsInstalled(version))
                return true;

            var civilPythonDownloadLocation = "https://github.com/atultegar/CivilPython/releases";

            string message = $"CivilPython {version} is not installed.\n\n" +
                $"Please download it from {civilPythonDownloadLocation}\n\n" +
                $"(Link copied to your clipboard)";

            Utils.Log(message);

            MessageBox.Show(
                message,
                "CivilPython not installed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            Clipboard.SetText(civilPythonDownloadLocation);

            return false;
        }


        internal static string GetCivilPythonVersion()
        {
#if C2023
            return "2023";

#elif C2024
            return "2024";

#elif C2025
            return "2025";

#elif C2026
            return "2026";


#elif C2027
            return "2027";

#else
            throw new NotSupportedException("Unsupported Civil 3D version.");
#endif
        }

        

        internal static void DisposeObjects(params DesignScriptEntity[] geometry)
        {
            foreach (var g in geometry)
            {
                g?.Dispose();
            }
        }

        


        // TODO : Create a set of nodes to process directly LandXML files to extract:
        // Surfaces
        // Alignments
        // Corridors
        // Pipe Networks

        #endregion
    }
}
