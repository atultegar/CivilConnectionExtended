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

using System.Collections.Generic;
using System.Linq;

using Autodesk.DesignScript.Runtime;
using CivilConnection.Interop.Wrappers;
using CivilConnection.Interop.Services;

namespace CivilConnection
{
    /// <summary>
    /// Profile obejct type.
    /// </summary>
    public class Profile
    {
        #region INTERNAL

        internal readonly ProfileWrapper _profile;

        #endregion

        #region SERVICES

        private readonly ProfileService _profileService;

        #endregion

        #region PRIVATE PROPERTIES

        /// <summary>
        /// The entities
        /// </summary>
        private dynamic _entities;

        #endregion

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => _profile.DisplayName;
        /// <summary>
        /// Gets the internal element.
        /// </summary>
        /// <value>
        /// The internal element.
        /// </value>
        internal ProfileWrapper InternalElement => _profile;
        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public double Length => _profile.Length;
        /// <summary>
        /// Gets the maximum elevation.
        /// </summary>
        /// <value>
        /// The maximum elevation.
        /// </value>
        public double MaxElevation => _profile.ElevationMax;
        /// <summary>
        /// Gets the minimum elevation.
        /// </summary>
        /// <value>
        /// The minimum elevation.
        /// </value>
        public double MinElevation => _profile.ElevationMin;
        /// <summary>
        /// Gets the start station.
        /// </summary>
        /// <value>
        /// The start station.
        /// </value>
        public double StartStation => _profile.StartingStation;
        /// <summary>
        /// Gets the end station.
        /// </summary>
        /// <value>
        /// The end station.
        /// </value>
        public double EndStation => _profile.EndingStation; 
        /// <summary>
        /// Gets the weed grade factor.
        /// </summary>
        /// <value>
        /// The weed grade factor.
        /// </value>
        public double WeedGradeFactor => _profile.WeedGradeFactor;

        /// <summary>
        /// Gets the stations of the PVIs.
        /// </summary>
        /// <value>
        /// The PVIStations.
        /// </value>
        public double[] PVIStations => _profile.PVIs.Select(x => x.Station).ToArray();

        /// <summary>
        /// Gets the elevation of the PVIs.
        /// </summary>
        /// <value>
        /// The PVIElevations.
        /// </value>
        public double[] PVIElevations => _profile.PVIs.Select(x => x.Elevation).ToArray();
        
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        /// <param name="profile">The profile.</param>
        internal Profile(ProfileWrapper profile)
        {
            _profile = profile;
            _profileService = new ProfileService();
            _entities = profile.Entities;
        }

        #endregion

        #region PRIVATE METHODS

        //TODO: Get profile curves
        /// <exclude />
        /// 
        //[SupressImportIntoVM]
        //private void XX()
        //{
        //    Utils.Log(string.Format("Profile.XX started...", ""));

        //    IList<Curve> output = new List<Curve>();

        //    for (int i = 0; i < this._entities.Count; ++i)
        //    {
        //        var e = this._entities.Item(i);

        //        if (e.Type == AeccProfileEntityType.aeccProfileEntityTangent)
        //        {
        //            var tangent = e as aeccProfileTangent;

        //            var start = Point.ByCoordinates(tangent.StartStation, tangent.StartElevation);
        //            var end = Point.ByCoordinates(tangent.EndStation, tangent.EndElevation);

        //            output.Add(Line.ByStartPointEndPoint(start, end));
        //        }
        //        else if (e.Type == AeccProfileEntityType.aeccProfileEntityCurveCircular)
        //        {
        //            var arc = e as aeccProfileCurveCircular;

        //            var start = Point.ByCoordinates(arc.StartStation, arc.StartElevation);
        //            var end = Point.ByCoordinates(arc.EndStation, arc.EndElevation);
        //            var pvi = Point.ByCoordinates(arc.PVIStation, arc.PVIElevation);
        //            double radius = arc.Radius;
        //        }
        //    }

        //    Utils.Log(string.Format("Profile.XX completed.", ""));
        //}

        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Gets the elevation at station.
        /// </summary>
        /// <param name="station">The station.</param>
        /// <returns></returns>
        public double GetElevationAtStation(double station)
        {
            Utils.LogMethodStart(this);

            var output = _profileService.GetElevationAtStation(_profile, station);

            Utils.LogMethodEnd(this);

            return output;
        }

        /// <summary>
        /// Gets the elevations of the entities in the profile.
        /// </summary>
        /// <returns></returns>
        /// 
        [SupressImportIntoVM]
        public IList<double> GetEntitiesElevations()
        {
            Utils.LogMethodStart(this);

            var elevations = _profileService.GetEntityElevations(_profile);

            Utils.LogMethodEnd(this);

            return elevations;
        }

        /// <summary>
        /// Gets the stations of the entities in the profile.
        /// </summary>
        /// <returns></returns>
        /// 
        [SupressImportIntoVM]
        public IList<double> GetEntitiesStations()
        {
            Utils.LogMethodStart(this);

            var stations = _profileService.GetEntityStations(_profile);

            Utils.LogMethodEnd(this);

            return stations;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Profile(Name = {Name})";
        }

        #endregion
    }
}

