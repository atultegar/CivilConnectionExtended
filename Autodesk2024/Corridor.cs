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

using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Converters;
using CivilConnection.Interop.Services;
using CivilConnection.Interop.Wrappers;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection
{

    /// <summary>
    /// Corridor obejct type.
    /// </summary>
    public class Corridor
    {
        #region INTERNAL

        internal readonly CorridorWrapper _corridor;

        #endregion

        #region SERVICES

        private readonly CorridorService _corridorService;
        private readonly BaselineService _baselineService;

        #endregion

        #region PRIVATE PROPERTIES

        /// <summary>
        /// The baselines
        /// </summary>
        internal IList<Baseline> _baselines;
        /// <summary>
        /// The document
        /// </summary>
        internal DocumentWrapper _document;
        /// <summary>
        /// Corridor Applied Subassembly Shapes
        /// </summary>
        private IList<IList<IList<AppliedSubassemblyShape>>> _shapes = new List<IList<IList<AppliedSubassemblyShape>>>();
        /// <summary>
        /// Corridor Applied Subassembly Links
        /// </summary>
        private IList<IList<IList<AppliedSubassemblyLink>>> _links = new List<IList<IList<AppliedSubassemblyLink>>>();
        /// <summary>
        /// Indicates if the corridor feature lines have been already extracted
        /// </summary>
        internal bool _corridorFeaturelinesXMLExported;
        #endregion

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the baselines.
        /// </summary>
        /// <value>
        /// The baselines.
        /// </value>
        public IList<Baseline> Baselines { get { return _baselines; } }
        /// <summary>
        /// Gets the Corridor name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => _corridor.DisplayName;
        /// <summary>
        /// Gets the internal element.
        /// </summary>
        /// <value>
        /// The internal element.
        /// </value>
        internal CorridorWrapper InternalElement  => _corridor;

        /// <summary>
        /// Gets the corridor applied subassembly shapes.
        /// </summary>
        public IList<IList<IList<AppliedSubassemblyShape>>> Shapes
        {
            get
            {
                if (_shapes.Count == 0)
                {
                    _shapes = GetShapesFromXML();
                }

                return _shapes;
            }
        }

        /// <summary>
        /// Gets the corridor applied subassembly links.
        /// </summary>
        public IList<IList<IList<AppliedSubassemblyLink>>> Links
        {
            get
            {
                if (_links.Count == 0)
                {
                    _links = GetLinksFromXML();
                }

                return _links;
            }
        }
        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="Corridor"/> class.
        /// </summary>
        /// <param name="corridor">The corridor.</param>
        internal Corridor(CorridorWrapper corridor)
        {
            _corridor = corridor;

            List<Baseline> bls = new List<Baseline>();

            int index = 0;
            foreach (var b in corridor.Baselines)
            {
                bls.Add(new Baseline(b, index, this));
                ++index;
            }

            _baselines = bls;
            _corridorFeaturelinesXMLExported = false;

            _corridorService = new CorridorService();
            _baselineService = new BaselineService();
        }

        /// <summary>
        /// Rebuilds the Corridor in Civil 3D.
        /// </summary>
        /// <returns></returns>
        public Corridor Rebuild()
        {
            this.InternalElement.Rebuild();
            return this;
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Gets the corridor surfaces.
        /// </summary>
        /// <returns></returns>
        private IList<CorridorSurfaceData> GetCorridorSurfaces() //revise if needed
        {
            Utils.LogMethodStart(this);

            var output = _corridorService.GetCorridorSurfaces(_corridor);

            Utils.LogMethodEnd(this);
                        
            return output;
        }

        /// <summary>
        /// Returns a collection of AppliedSubassemblyShapes in the Corridor.
        /// </summary>
        /// <returns></returns>
        private IList<IList<IList<AppliedSubassemblyShape>>> GetShapesFromXML()
        {
            Utils.LogMethodStart(this);

            var data = _corridorService.GetShapes(_corridor);

            var output = data
                .Select(baseline => (IList<IList<AppliedSubassemblyShape>>)baseline
                    .Select(region => (IList<AppliedSubassemblyShape>) region
                        .Select(AppliedSubassemblyConverter.ToDynamo)
                        .ToList())
                    .ToList())
                .ToList();

            Utils.LogMethodEnd(this);

            return output;
        }

        /// <summary>
        /// Returns a collection of AppliedSubassemblyLinks in the Corridor.
        /// </summary>
        /// <returns></returns>
        private IList<IList<IList<AppliedSubassemblyLink>>> GetLinksFromXML()
        {
            Utils.LogMethodStart(this);

            var data = _corridorService.GetLinks(_corridor);

            var output = data
                .Select(baseline => (IList<IList<AppliedSubassemblyLink>>)baseline
                    .Select(region => (IList<AppliedSubassemblyLink>)region
                        .Select(AppliedSubassemblyConverter.ToDynamo)
                        .ToList())
                    .ToList())
                .ToList();

            Utils.LogMethodEnd(this);

            return output;
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Returns a Point by station offset elevation.
        /// </summary>
        /// <param name="baseline">The baseline.</param>
        /// <param name="station">The station.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <returns></returns>
        public Point PointByStationOffsetElevation(Baseline baseline, double station = 0, double offset = 0, double elevation = 0)
        {
            return baseline.PointByStationOffsetElevation(station, offset, elevation);
        }

        /// <summary>
        /// Returns a CoordinateSystem by station.
        /// </summary>
        /// <param name="baseline">The baseline.</param>
        /// <param name="station">The station.</param>
        /// <returns></returns>
        public CoordinateSystem CoordinateSystemByStation(Baseline baseline, double station = 0)
        {
            return baseline.CoordinateSystemByStation(station);
        }

        /// <summary>
        /// Returns a CoordinateSystem by point.
        /// </summary>
        /// <param name="baseline">The baseline.</param>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public CoordinateSystem CoordinateSystemByPoint(Baseline baseline, Point point)
        {
            return baseline.GetCoordinateSystemByPoint(point);
        }

        /// <summary>
        /// Gets the PointCodes.
        /// </summary>
        /// <returns></returns>
        public IList<string> GetCodes()
        {
            Utils.LogMethodStart(this);

            var codes = _corridorService.GetCodes(_corridor);

            Utils.LogMethodEnd(this);

            return codes;
        }

        /// <summary>
        /// Gets the corridor Featurelies organized by Corridor-Baseline-Code-Region
        /// </summary>
        /// <returns></returns>
        public IList<IList<IList<Featureline>>> GetFeaturelines()
        {
            Utils.LogMethodStart(this);

            var data = _corridorService.GetFeaturelines(_corridor);

            var output = FeaturelineConverter.ToDynamo(data, this.Baselines);

            Utils.LogMethodEnd(this);

            return output;
        }

        /// <summary>
        /// Gets the subassembly points organized by: Corridor &gt; Baseline &gt; Region &gt; Assembly &gt; Subassembly.
        /// </summary>
        /// <returns></returns>
        public IList<IList<IList<IList<Point>>>> GetSubassemblyPoints()
        {
            Utils.LogMethodStart(this);

            var shapeData = _corridorService.GetShapes(_corridor);

            var output = shapeData
                .Select(baseline => (IList<IList<IList<Point>>>)baseline
                    .Select(region => (IList<IList<Point>>)region
                        .Select(shape => (IList<Point>)shape.BoundaryPoints
                            .Select(GeometryConverter.ToProtoPoint).ToList())
                        .ToList())
                    .ToList())
                .ToList();

            Utils.LogMethodEnd(this);

            return output;
        }

        /// <summary>
        ///  Gets the subassembly points organized by: Corridor &gt; Baseline &gt; Region &gt; Code.
        ///  It requires to export a LandXML to the %Temp% folder, named like the Civil 3D Document, containing only the corridor data.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public IList<IList<IList<IList<Point>>>> GetPointsByCode(string code)
        {
            Utils.LogMethodStart(this);

            var featurelines = GetFeaturelinesByCode(code);

            var output = featurelines
                .Select(baseline => (IList<IList<IList<Point>>>)baseline
                    .Select(region => (IList<IList<Point>>)region
                        .Select(fl => fl.Points.ToList()).ToList())
                    .ToList())
                .ToList();

            return output;
        }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Corridor(Name = {Name})";
        }

        /// <summary>
        /// Gets the closest featureline by point code side.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="baselineIndex">Index of the baseline.</param>
        /// <param name="code">The code.</param>
        /// <param name="side">The side.</param>
        /// <returns></returns>
        [MultiReturn(new string[] { "Featureline" })]
        public Dictionary<string, object> GetFeaturelineByPointCodeSide(Point point, int baselineIndex, string code, string side)
        {
            Utils.LogMethodStart(this);

            var baselineWrapper = _corridor.Baselines.First(x => x.Index == baselineIndex);

            var baseline = new Baseline(baselineWrapper, baselineIndex, this);

            var featurelineData = _corridorService.ClosestFeaturelineByPoint(_corridor, GeometryConverter.ToPointData(point), baselineIndex, code, side);

            var output = FeaturelineConverter.ToDynamo(featurelineData, baseline);

            Utils.LogMethodEnd(this);

            return new Dictionary<string, object>() { { "Featureline", output } };
        }

        /// <summary>
        /// Gets the featurelines by Code &gt; Baseline &gt; Region.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public IList<IList<IList<Featureline>>> GetFeaturelinesByCode(string code)
        {
            Utils.LogMethodStart(this);

            var data = _corridorService.GetFeaturelinesByCode(_corridor, code);

            var featurelines = FeaturelineConverter.ToDynamo(data, Baselines);

            Utils.LogMethodEnd(this);

            return featurelines;
        }

        /// <summary>
        /// Gets the featurelines by Code &gt; Baseline &gt; Region.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="station">The station.</param>
        /// <returns></returns>
        public IList<IList<Featureline>> GetFeaturelinesByCodeStation(string code, double station)
        {
            Utils.LogMethodStart(this);

            var data = _corridorService.GetFeaturelinesByCodeStation(_corridor, code, station);

            var baseline = Baselines.First(x => x.Start <= station && x.End >= station);

            var featurelines = FeaturelineConverter.ToDynamo(data, baseline);

            Utils.LogMethodEnd(this);

            return featurelines;
        }

        #endregion
    }
}
