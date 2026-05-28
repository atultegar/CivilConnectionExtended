using Autodesk.AECC.Interop.UiRoadway;
using CivilConnection.Interop.Runtime;
using CivilConnection.Interop.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection.Interop.Context
{
    public sealed class CivilContext : IDisposable
    {
        public CivilHost Host { get; }
        public AcadApplication AcadApplication { get; }

        public AeccRoadwayApplication CivilApplication { get; }

        public AeccRoadwayDocument ActiveDocument { get; }

        public IReadOnlyList<AeccRoadwayDocument> Documents { get; } 


        private CivilContext(
            AcadApplication acadApplication,
            AeccRoadwayApplication civilAppliction,
            IReadOnlyList<AeccRoadwayDocument> documents,
            AeccRoadwayDocument activeDocument)
        {
            AcadApplication = acadApplication;
            CivilApplication = civilAppliction;
            Documents = documents;
            ActiveDocument = activeDocument;
        }

        private CivilContext(CivilHost host)
        {
            Host = host;
        }

        //public static CivilContext Create()
        //{            
        //    var acadApplication = GetAcadApplication();

        //    var civilApplication = GetCivilApplication(acadApplication) as AeccRoadwayApplication;

        //    var documents = new List<AeccRoadwayDocument>();

        //    foreach (AeccRoadwayDocument doc in civilApplication.Documents)
        //    {
        //        documents.Add(doc);
        //    }

        //    var activeDocument = civilApplication.ActiveDocument as AeccRoadwayDocument;

        //    return new CivilContext(acadApplication, civilApplication, documents, activeDocument);
        //}

        public static CivilContext Create(string? version = null)
        {
            var runtime = RunningInstanceService.Discover();

            CivilHost? host;

            if (version == null)
            {
                host = runtime.ActiveHost;
            }
            else
            {   
                host = runtime.Hosts.FirstOrDefault(x => x.VersionInfo.Version == version);
            }

            if (host == null)
                throw new Exception("No Civil3D session found.");

            return new CivilContext(host);
        }

        public static List<CivilContext> CreateAllContext()
        {
            var runtime = RunningInstanceService.Discover();

            var output = new List<CivilContext>();

            foreach (var host in runtime.Hosts)
            {
                output.Add(new CivilContext(host));
            }

            return output;
        }
        

        private static AcadApplication GetAcadApplication()
        {
            try
            {
                return (AcadApplication)
                    Marshal.GetActiveObject("AutoCAD.Application");
            }
            catch
            {
                throw new Exception("Could not connect to AutoCAD.");
            }
        }

        private static object GetCivilApplication(AcadApplication acadApplication)
        {
            var progIds = ProgIdProvider.GetCivilProgIds();

            foreach (var progId in progIds)
            {
                try
                {
                    var app = acadApplication.GetInterfaceObject(progId);

                    if (app != null)
                    {
                        return (AeccRoadwayApplication)app;
                    }
                }
                catch (COMException ex)
                {
                    // Ignore unavailable versions
                    if (!ex.ToString().Contains("0x800401E3"))
                    {
                        throw;
                    }
                }
            }

            throw new Exception("Could not connect to Civil 3D.");
        }

        public void Dispose()
        {
            foreach (var doc in Documents)
            {
                ComUtilities.Release(doc);
            }

            ComUtilities.Release(ActiveDocument);

            ComUtilities.Release(CivilApplication);

            ComUtilities.Release(AcadApplication);
        }
    }
}
