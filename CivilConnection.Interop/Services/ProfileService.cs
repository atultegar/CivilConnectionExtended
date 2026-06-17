using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection.Interop.Services
{
    public class ProfileService
    {
        public double GetElevationAtStation(ProfileWrapper profile, double station)
        {
            return profile.ElevationAt(station);
        }

        public IList<double> GetEntityStations(ProfileWrapper profile)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));

            var stations = new List<double>();

            foreach (var entity in profile.Entities)
            {
                stations.Add(entity.StartStation);

                if (entity.HasHighLowPoint)
                {
                    stations.Add(entity.HighLowPointStation);
                }

                stations.Add(entity.EndStation);
            }

            return stations
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        public IList<double> GetEntityElevations(ProfileWrapper profile)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));

            var elevations = new List<double>();

            var entities = profile.Entities
                .OrderBy(x => x.StartStation)
                .ToList();

            for (int i = 0;i < entities.Count; i++)
            {
                var entity = entities[i];

                elevations.Add(entity.StartElevation);

                if (entity.HasHighLowPoint)
                {
                    elevations.Add(entity.HighLowPointElevation);
                }

                if (i == entities.Count - 1)
                {
                    elevations.Add(entity.EndElevation);
                }
            }

            return elevations;
        }
    }
}
