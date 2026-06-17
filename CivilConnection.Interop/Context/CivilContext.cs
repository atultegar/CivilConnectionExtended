using CivilConnection.Interop.Runtime;
using CivilConnection.Interop.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection.Interop.Context
{
    public sealed class CivilContext
    {
        public CivilHost Host { get; }
        

        private CivilContext(CivilHost host)
        {
            Host = host;
        }

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
    }
}
