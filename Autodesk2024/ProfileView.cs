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

using CivilConnection.Interop.Wrappers;

namespace CivilConnection
{
    /// <summary>
    /// ProfileView object type.
    /// </summary>
    public class ProfileView
    {
        #region INTERNAL

        internal readonly ProfileViewWrapper _profileView;

        #endregion
        

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => _profileView.DisplayName;
        /// <summary>
        /// Gets the internal element.
        /// </summary>
        /// <value>
        /// The internal element.
        /// </value>
        internal object InternalElement => _profileView;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileView"/> class.
        /// </summary>
        /// <param name="profileView">The profile view.</param>
        internal ProfileView(ProfileViewWrapper profileView)
        {
            _profileView = profileView;
        }

        #endregion

        #region PRIVATE METHODS


        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"ProfileView(Name = {Name})";
        }

        #endregion
    }
}
