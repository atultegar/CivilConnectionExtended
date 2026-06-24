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

using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using CivilConnection.Interop.Services;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;

namespace CivilConnection
{
    /// <summary>
    /// LandFeatureline object type.
    /// </summary>
    public class LandFeatureline
    {
        #region INTERNAL

        internal readonly LandFeaturelineWrapper _landFeatureline;

        #endregion

        #region SERVICES

        private readonly GeometryService _geometryService;

        #endregion

        #region PRIVATE PROPERTIES
        
        /// <summary>
        /// The polycurve
        /// </summary>
        private PolyCurve _polycurve;

        /// <summary>
        /// The points of the LandFeatureline
        /// </summary>
        IList<Point> _points = new List<Point>();

        #endregion

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => _landFeatureline.Name;
        
        /// <summary>
        /// Gets the style.
        /// </summary>
        /// <value>
        /// The style.
        /// </value>
        public string Style { get ; }
        /// <summary>
        /// Gets the minimum grade.
        /// </summary>
        /// <value>
        /// The minimum grade.
        /// </value>
        public double MinGrade => _landFeatureline.MiniGrade;
        /// <summary>
        /// Gets the maximum grade.
        /// </summary>
        /// <value>
        /// The maximum grade.
        /// </value>
        public double MaxGrade => _landFeatureline.MaxGrade;
        /// <summary>
        /// Gets the minimum elevation.
        /// </summary>
        /// <value>
        /// The minimum elevation.
        /// </value>
        public double MinElevation => _landFeatureline.MiniElevation;
        /// <summary>
        /// Gets the maximum elevation.
        /// </summary>
        /// <value>
        /// The maximum elevation.
        /// </value>
        public double MaxElevation => _landFeatureline.MaxElevation;

        internal LandFeaturelineWrapper InternalElement => _landFeatureline;

        /// <summary>
        /// Gets the curve.
        /// </summary>
        /// <value>
        /// The curve.
        /// </value>
        public PolyCurve Curve 
        { 
            get 
            {
                if (_polycurve == null)
                {
                    _polycurve = BuildPolyCurve();
                }

                return _polycurve; 
            } 
        }

        

        /// <summary>
        /// Gets the LandFeatureline points.
        /// </summary>
        public IList<Point> Points
        {
            get
            {
                if (_points.Count == 0)
                {
                    _points = GetPoints();
                }

                return this._points;
            }
        }

        
        #endregion

        #region CONSTRUCTOR



        /// <summary>
        /// Initializes a new instance of the <see cref="LandFeatureline"/> class.
        /// </summary>
        /// <param name="fl">The AeccLandFeatureline.</param>
        /// <param name="style">The style name.</param>
        /// 
        [SupressImportIntoVM]
        internal LandFeatureline(LandFeaturelineWrapper fl, string style = "")
        {
            _landFeatureline = fl
                ?? throw new ArgumentNullException(nameof(fl));

            Style = style;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LandFeatureline"/> class.
        /// </summary>
        /// <param name="fl">The AeccLandFeatureline.</param>
        /// <param name="pc">The PolyCurve.</param>
        /// <param name="style">The style name.</param>
        internal LandFeatureline(LandFeaturelineWrapper fl, PolyCurve pc, string style = "") : this(fl, style)
        {
            _polycurve = pc;
        }

        #endregion

        #region PRIVATE METHODS

        private IList<Point> GetPoints()
        {
            var output = new List<Point>();

            var coordinates = _landFeatureline.GetPoints();

            for (int i = 0; i < coordinates.Length; i += 3)
            {
                output.Add(
                    Point.ByCoordinates(
                        coordinates[i],
                        coordinates[i + 1],
                        coordinates[i + 2]));
            }

            return output;
        }


        private PolyCurve BuildPolyCurve()
        {
            var points = GetPoints();

            if (points.Count < 2)
                return null;

            try
            {
                return PolyCurve.ByPoints(Point.PruneDuplicates(points));
            }
            catch
            {
                // Branching featurelines can fail.
                return null;
            }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Gets all the LandFeaturelines from a CivilDocument.
        /// The Style will be empty by default.
        /// Not all the PolyCurves will be branching and it is to be expected to have null values.
        /// </summary>
        /// <param name="civilDocument">The CivilDocument</param>
        /// <returns></returns>
        public static IList<LandFeatureline> GetDocumentLandFeaturelines(CivilDocument civilDocument)
        {
            Utils.Log($"LandFeatureline.GetDocumentLandFeaturelines started...");

            IList<LandFeatureline> output = civilDocument.GetLandFeaturelines();

            Utils.Log($"LandFeatureline.GetDocumentLandFeaturelines completed...");

            return output;
        }

        /// <summary>
        /// Creates LandFeatureline
        /// </summary>
        /// <param name="fl">The featureline COM object</param>
        /// <param name="polyCurve">The polycurve</param>
        /// <returns></returns>
        /// 
        [IsVisibleInDynamoLibrary(false)]
        public static LandFeatureline ByObjectPolyCurve(LandFeaturelineWrapper fl, PolyCurve polyCurve)
        {
            return new LandFeatureline(fl, polyCurve);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"LandFeatureline(Name = {Name}, Style = {Style})";
        }

        #endregion
    }
}
