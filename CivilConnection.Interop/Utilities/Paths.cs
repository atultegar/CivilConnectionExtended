using System;
using System.IO;
using System.Threading;

namespace CivilConnection.Interop.Utilities
{
    public static class Paths
    {
        internal static string GetCorridorShapesXmlPath()
        {
            return Path.Combine(
                Environment.GetEnvironmentVariable(
                    "TMP",
                    EnvironmentVariableTarget.User),
                "CorridorShapes.xml");  // Revit 2020 changed the path to the temp at a session level
        }

        internal static string GetCorridorLinksXmlPath()
        {
            return Path.Combine(
                Environment.GetEnvironmentVariable(
                    "TMP",
                    EnvironmentVariableTarget.User),
                "CorridorLinks.xml");  // Revit 2020 changed the path to the temp at a session level
        }

        internal static string GetCorridorFeaturelinesXmlPath()
        {
            return Path.Combine(
                Environment.GetEnvironmentVariable(
                    "TMP",
                    EnvironmentVariableTarget.User),
                "CorridorFeatureLines.xml");  // Revit 2020 changed the path to the temp at a session level
        }

        internal static string GetCorridorFeaturelineXmlPath(string corridorName)
        {
            return Path.Combine(
                Environment.GetEnvironmentVariable(
                    "TMP",
                    EnvironmentVariableTarget.User),
                $"CorridorFeatureLines_{corridorName}.xml");  // Revit 2020 changed the path to the temp at a session level
        }

        internal static void WaitForXml(params string[] paths)
        {
            var timeout = DateTime.Now.AddSeconds(30);

            DateTime start = DateTime.Now;

            while (DateTime.Now < timeout)
            {
                foreach (var path in paths)
                {
                    if (File.Exists(path))
                    {
                        return;
                    }
                }

                Thread.Sleep(250);
            }

            throw new TimeoutException("XML export timed out.");
        }

    }
}
