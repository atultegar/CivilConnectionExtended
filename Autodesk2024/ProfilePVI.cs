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

using Autodesk.DesignScript.Runtime;
using CivilConnection.Interop.Wrappers;

namespace CivilConnection
{
    /// <summary>
    /// ProfilePVI object type.
    /// </summary>
    [IsVisibleInDynamoLibrary(false)]
    public class ProfilePVI
    {
        #region INTERNAL

        internal readonly ProfilePVIWrapper _pvi;

        #endregion
        

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the station.
        /// </summary>
        /// <value>
        /// The station.
        /// </value>
        public double Station { get { return _pvi.Station; } }
        /// <summary>
        /// Gets the internal element.
        /// </summary>
        /// <value>
        /// The internal element.
        /// </value>
        internal ProfilePVIWrapper InternalElement  => _pvi;
        /// <summary>
        /// Gets the elevation.
        /// </summary>
        /// <value>
        /// The elevation.
        /// </value>
        public double Elevation { get { return _pvi.Elevation; } }
        /// <summary>
        /// Gets the grade in.
        /// </summary>
        /// <value>
        /// The grade in.
        /// </value>
        public double GradeIn { get { return _pvi.GradeIn; } }
        /// <summary>
        /// Gets the grade out.
        /// </summary>
        /// <value>
        /// The grade out.
        /// </value>
        public double GradeOut { get { return _pvi.GradeOut; } }

        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilePVI"/> class.
        /// </summary>
        /// <param name="pvi">The pvi.</param>
        internal ProfilePVI(ProfilePVIWrapper pvi)
        {
            _pvi = pvi;
        }

        #endregion

        #region PRIVATE METHODS


        #endregion

        #region PUBLIC METHODS


        //TODO: Get profile curves        

        /// <summary>
        /// Returns a text representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ProfilePVI(Station = {Station}, Elevation = {Elevation}, GradeIn = {GradeIn}, GradeOut = {GradeOut})";
        }
        #endregion
    }
}
