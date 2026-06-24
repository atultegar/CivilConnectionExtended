using CivilConnection.Interop.Wrappers.Base;
using System.Collections.Generic;

namespace CivilConnection.Interop.Wrappers
{
    public class BaselineRegionWrapper : ComWrapper
    {
        public BaselineRegionWrapper(object baselineRegion) : base(baselineRegion)
        {
        }

        public double StartStation => (double)ComObject.StartStation;

        public double EndStation => (double)ComObject.EndStation;

        public string AssemblyName => (string)ComObject.AssemblyDbEntity.DisplayName;

        public double[] GetSortedStations() => (double[])ComObject.GetSortedStations();

        public IEnumerable<AppliedAssemblyWrapper> AppliedAssemblies
        {
            get
            {
                foreach (dynamic assembly in ComObject.AppliedAssemblies)
                {
                    yield return new AppliedAssemblyWrapper(assembly);
                }
            }
        }

        public double[] AppliedAssembliesStation => (double[])ComObject.AppliedAssemblies.Stations;
    }
}
