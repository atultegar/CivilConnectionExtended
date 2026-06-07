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

using Autodesk.DesignScript.Runtime;
using CivilConnection.Interop.Wrappers;
using System;

namespace CivilConnection
{
    /// <summary>
    /// SubassemblyParameter obejct type.
    /// </summary>
    public class SubassemblyParameter
    {
        #region INTERNAL

        internal readonly ParameterWrapper _parameter;

        #endregion

        #region PRIVATE PROPERTIES
        
        /// <summary>
        /// Gets the internal element.
        /// </summary>
        /// <value>
        /// The internal element.
        /// </value>
        internal object InternalElement => _parameter;

        #endregion

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => _parameter.DisplayName;
        /// <summary>
        /// Gets the Comment.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Comment => _parameter.Comment;
        /// <summary>
        /// Gets the Description.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Description => _parameter.Description;
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value => _parameter.Value;
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public SubassemblyParameterType Type => ConvertType(_parameter.ParameterType);

        

        //public Parameters[] Parameters {get {return this._parameters}}

        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="SubassemblyParameter"/> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        internal SubassemblyParameter(ParameterWrapper parameter)
        {
            _parameter = parameter;
        }


        #endregion

        #region PRIVATE METHODS

        private static SubassemblyParameterType ConvertType(ParameterType type)
        {
            return type switch
            {
                ParameterType.Bool => SubassemblyParameterType.Bool,
                ParameterType.Double => SubassemblyParameterType.Double,
                ParameterType.Long => SubassemblyParameterType.Long,
                ParameterType.String => SubassemblyParameterType.String,
                _ => throw new NotSupportedException($"Unsupported parameter type: {type}")
            };
        }

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
            return $"SubassemblyParameter(Name={Name}, Value={Value}, Type={Type}, Comment={Comment}, Description={Description})";
        }   
        #endregion
    }

    /// <summary>
    /// SubassemblyParameterType enumerator.
    /// </summary>
    [IsVisibleInDynamoLibrary(false)]
    public enum SubassemblyParameterType
    {
        /// <summary>
        /// Boolean Type
        /// </summary>
        Bool,
        /// <summary>
        /// Double Type
        /// </summary>
        Double,
        /// <summary>
        /// Long Type
        /// </summary>
        Long,
        /// <summary>
        /// Stirng Type
        /// </summary>
        String
    }

}
