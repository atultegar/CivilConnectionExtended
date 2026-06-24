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

using System;
using System.Collections.Generic;
using System.Linq;
using CivilConnection.Interop.Wrappers;
using CivilConnection.Interop.Services;
using CivilConnection.Converters;

namespace CivilConnection
{
    /// <summary>
    /// BaselineRegion object type.
    /// </summary>
    public class BaselineRegion
    {
        #region INTERNAL

        internal readonly BaselineRegionWrapper _baselineRegion;

        #endregion

        #region SERVICES

        private readonly CorridorService _corridorService;

        #endregion

        #region PRIVATE PROPERTIES

        private Baseline _baseline;
        private int _index;

        private double _start;
        private double _end;
        private double[] _stations;
        private string _assembly;

        internal object InternalElement => _baselineRegion;
                
        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Gets the region start station.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public double Start => _start;
        /// <summary>
        /// Gets theregion end station.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public double End => _end;
        /// <summary>
        /// Gets the region stations.
        /// </summary>
        /// <value>
        /// The stations.
        /// </value>
        public double[] Stations => _stations;
        
        /// <summary>
        /// Gets the relative starting station for the BaselineRegion.
        /// </summary>
        public double RelativeStart => 0;

        /// <summary>
        /// Gets the relative ending station for the BaselineRegion.
        /// </summary>
        public double RelativeEnd => End - Start;

        /// <summary>
        /// Gets the normalized starting station for the BaselineRegion.
        /// </summary>
        public double NormalizedStart => 0;

        /// <summary>
        /// Gets the normalized starting station for the BaselineRegion.
        /// </summary>
        public double NormalizedEnd => 1;

        /// <summary>
        /// Gets the Baselineregion index value.
        /// </summary>
        public int Index => _index;

        /// <summary>
        /// Gets the Assembly for the BaselineRegion.
        /// </summary>
        public string AssemblyName => _assembly;

        /// <summary>
        /// Gets the Baseline for BaselineRegion
        /// </summary>
        public Baseline Baseline => _baseline;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="baseline">The baseline that holds the baseline region.</param>
        /// <param name="baselineRegion">The internal AeccBaselineRegion</param>
        /// <param name="i">The baseline region index</param>
        internal BaselineRegion(Baseline baseline, BaselineRegionWrapper baselineRegion, int index)
        {
            _baseline = baseline;
            _baselineRegion = baselineRegion;
            _index = index;

            _assembly = baselineRegion.AssemblyName;

            _start = baselineRegion.StartStation;

            _end = baselineRegion.EndStation;

            _stations = baselineRegion.GetSortedStations();

            _corridorService = new CorridorService();
        }

        #endregion


        #region PUBLIC METHODS

        /// <summary>
        /// Gets the Shapes profile of the applied subassemblies in the BaselineRegion.
        /// </summary>
        public IList<AppliedSubassemblyShape> GetShapes()
        {
            Utils.LogMethodStart(this);

            var data = _corridorService.GetShapes(_baseline.Corridor.InternalElement, _baseline.Index, Index);

            var output = data.Select(AppliedSubassemblyConverter.ToDynamo).ToList();

            Utils.LogMethodEnd(this);

            return output;
        }


        /// <summary>
        /// Gets the Links profile of the applied subassemblies in the BaselineRegion.
        /// </summary>
        //public List<List<List<Geometry>>> Links
        public IList<AppliedSubassemblyLink> GetLinks()
        {
            Utils.LogMethodStart(this);

            var data = _corridorService.GetLinks(_baseline.Corridor.InternalElement, _baseline.Index, Index);

            var output = data.Select(AppliedSubassemblyConverter.ToDynamo).ToList();

            Utils.LogMethodEnd(this);

            return output;
        }

        /// <summary>
        /// Public textual representation of the Dynamo node preview
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"BaselineRegion(Start = {Math.Round(Start, 2).ToString()}, End = {Math.Round(this.End, 2).ToString()})";
        }

        #endregion
    }
}
