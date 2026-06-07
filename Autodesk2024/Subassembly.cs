// Copyright (c) 2017 Autodesk, Inc.
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

using CivilConnection.Interop.Services;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection
{
    /// <summary>
    /// Subassembly object type.
    /// </summary>
    public class Subassembly
    {
        #region INTERNAL

        internal readonly SubassemblyWrapper _subassembly;

        #endregion

        #region SERVICES

        private readonly SubassemblyService _subassemblyService;

        #endregion

        #region PRIVATE PROPERTIES

        /// <summary>
        /// The corridor
        /// </summary>
        internal CorridorWrapper _corridor;
        /// <summary>
        /// The parameters
        /// </summary>
        internal IList<SubassemblyParameter> _parameters = new List<SubassemblyParameter>();

        /// <summary>
        /// Gets the internal element.
        /// </summary>
        /// <value>
        /// The internal element.
        /// </value>
        internal object InternalElement => _subassembly;

        #endregion

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => _subassembly.Name;
        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public IList<SubassemblyParameter> Parameters { get { return this._parameters; } }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="Subassembly"/> class.
        /// </summary>
        /// <param name="subassembly">The subassembly.</param>
        /// <param name="corridor">The corridor.</param>
        internal Subassembly(SubassemblyWrapper subassembly, CorridorWrapper corridor)
        {
            _subassembly = subassembly;
            _corridor = corridor;

            _subassemblyService = new SubassemblyService();

            _parameters = subassembly.Parameters
                .Select(x => new SubassemblyParameter(x))
                .ToList();
        }

        #endregion

        #region PRIVATE METHODS


        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Sets SubassemblyParameter value by name.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The value.</param>
        /// <param name="rebuild">if set to <c>true</c> [rebuild].</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// The parameter name cannot be null
        /// or
        /// The value cannot be null
        /// or
        /// or
        /// </exception>
        public Subassembly SetParameterByName(string name, object value, bool rebuild = false)
        {
            Utils.LogMethodStart(this);

            _subassemblyService.SetParameter(_subassembly, name, value, rebuild);

            Utils.LogMethodEnd(this);

            return this;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Subassembly(Name={Name}, Corridor={_corridor.Name})";
        }
        #endregion
    }
}
