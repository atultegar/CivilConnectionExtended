using CivilConnection.Interop.Wrappers.Base;
using System.Collections.Generic;

namespace CivilConnection.Interop.Wrappers
{
    public class ProfileWrapper : ComWrapper
    {
        public ProfileWrapper(object profile) : base(profile)
        {
        }

        public string DisplayName => (string)ComObject.DisplayName;

        public double Length => (double)ComObject.Length;

        public double ElevationMax => (double)ComObject.ElevationMax;

        public double ElevationMin => (double)ComObject.ElevationMin;

        public double StartingStation => (double)ComObject.StartingStation;

        public double EndingStation => (double)ComObject.EndingStation;

        public double WeedGradeFactor => (double)ComObject.WeedGradeFactor;

        public double ElevationAt(double station) => (double)ComObject.ElevationAt(station);

        public IEnumerable<ProfileEntityWrapper> Entities
        {
            get
            {
                foreach (dynamic entity in ComObject.Entities)
                {
                    yield return new ProfileEntityWrapper(entity);
                }
            }
        }

        public int EntityCount => (int)ComObject.Entities.Count;

        public IEnumerable<ProfilePVIWrapper> PVIs
        {
            get
            {
                foreach(dynamic pvi in ComObject.PVIs)
                {
                    yield return new ProfilePVIWrapper(pvi);
                }
            }
        }

        public int PVICount => (int)ComObject.PVIs.Count;


    }
}
