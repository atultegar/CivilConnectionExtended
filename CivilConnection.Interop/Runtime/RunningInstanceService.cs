using CivilConnection.Interop.Repositories;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace CivilConnection.Interop.Runtime
{
    public static class RunningInstanceService
    {
        public static CivilRuntime Discover()
        {
            var runtime = new CivilRuntime();

            var versions = CivilVersionRepository.GetAll();

            foreach (var versionsInfo in versions)
            {
                try
                {
                    dynamic acadApp = ComInteropHelper.GetRunningInstance(versionsInfo.AutoCADProgId);

                    if (acadApp == null)
                        continue;

                    dynamic civilApp = acadApp.GetInterfaceObject(versionsInfo.ProgId);

                    if (civilApp == null)
                        continue;

                    runtime.Hosts.Add(new CivilHost
                    {
                        VersionInfo = versionsInfo,
                        AcadApplication = acadApp,
                        Application = civilApp,
                        IsRunning = true
                    });
                }
                catch { }
            }            

            return runtime;
        }
    }
}
