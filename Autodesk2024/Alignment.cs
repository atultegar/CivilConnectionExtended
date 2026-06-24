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
using System;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection
{
    /// <summary>
    /// Alignment object type.
    /// </summary>
    public class Alignment
    {
        #region PRIVATE MEMBERS

        private readonly AlignmentData _data;

        private readonly AlignmentService _alignmentService;
        
        internal readonly AlignmentWrapper _alignment;

        #endregion

        #region INTERNAL

        internal Alignment(AlignmentService alignmentService)
        {
            _alignmentService = alignmentService;
        }

        internal AlignmentData Data => _data;

        #endregion

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => _alignment.Name;
        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public double Length => _alignment.Length;
        /// <summary>
        /// Gets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public double Start => _alignment.StartingStation;
        /// <summary>
        /// Gets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public double End => _alignment.EndingStation;        


        /// <summary>
        /// Gets the stations of the geometry points.
        /// </summary>
        /// <value>
        /// The GeometryStations.
        /// </value>
        /// 
        public double[] GeometryStations => _data.GeometryStations?.ToArray() ?? Array.Empty<double>(); 

        /// <summary>
        /// Gets the stations of the points of intersection.
        /// </summary>
        /// <value>
        /// The PIStations.
        /// </value>
        /// 
        public double[] PIStations => _data.PIStations?.ToArray() ?? Array.Empty<double>();

        /// <summary>
        /// Gets the stations of the points of superelevation transition.
        /// </summary>
        /// <value>
        /// The SuperTransStations.
        /// </value>
        /// 
        public double[] SuperTransStations => _data.SuperTransitionStations?.ToArray() ?? Array.Empty<double>();

        /// <summary>
        /// Gets the stations of the station equations
        /// </summary>
        /// <value>
        /// The Eqaution Stations
        /// </value>
        /// 
        public double[] EquationStations => _data.EquationStations?.ToArray() ?? Array.Empty<double>();

        /// <summary>
        /// Gets the station ahead based on station equations
        /// </summary>
        /// <value>
        /// The Station Ahead at each Station Equation
        /// </value>
        public double[] StationAhead => _data.StationAhead?.ToArray() ?? Array.Empty<double>();

        #endregion


        #region INTERNAL CONSTRUCTORS
        /// <summary>
        /// Gets the internal element.
        /// </summary>
        /// <value>
        /// The internal element.
        /// </value>
        internal AlignmentWrapper InternalElement => _alignment;

        /// <summary>
        /// Internal constructor.
        /// </summary>
        /// <param name="alignment">The internal AeccAlignment.</param>
        internal Alignment(AlignmentWrapper alignment)
        {
            _alignment = alignment;
            _alignmentService = new AlignmentService();
        }
        #endregion

        #region PUBLIC CONSTRUCTORS
        /// <summary>
        /// Creates an Alignment in the Civil Document starting from a Dynamo polygonal PolyCurve.
        /// </summary>
        /// <param name="civilDocument">The CivilDocument.</param>
        /// <param name="name">The name of the alignment.</param>
        /// <param name="polyCurve">The source PolyCurve.</param>
        /// <param name="layer">The layer.</param>
        /// <returns>
        /// The new Alignment
        /// </returns>
        public static Alignment ByPolygonal(CivilDocument civilDocument, string name, PolyCurve polyCurve, string layer)
        {
            Utils.Log($"Alignment.ByPolygonal start...");

            var documentServie = new DocumentService();

            var totalTransform = RevitUtils.DocumentTotalTransform();

            polyCurve = polyCurve.Transform(totalTransform.Inverse()) as PolyCurve;

            IList<Point> points = polyCurve.Curves().Select<Curve, Point>(c => c.StartPoint).ToList();

            points.Add(polyCurve.EndPoint);

            var pointData = points.Select(GeometryConverter.ToPointData).ToList();

            var alignmentData = documentServie.CreateAlignmentFromPoints(civilDocument.InternalElement, name, pointData, layer);

            var alignment = new Alignment(alignmentData);

            Utils.Log($"Alignment.ByPolygonal completed...");

            return alignment;
        }

        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Returns the list of vertical Profiles associated to the Alignment.
        /// </summary>
        /// <returns>The list of associated Profiles.</returns>
        public IList<Profile> GetProfiles()
        {
            Utils.LogMethodStart(this);

            var profiles = _alignmentService.GetProfiles(_alignment)
                .Select(x => new Profile(x)).ToList();

            Utils.LogMethodEnd(this);

            return profiles;
        }

        /// <summary>
        /// Returns the list of vertical ProfileViews associated to the Alignment.
        /// </summary>
        /// <returns>The list of assocaited ProfileViews.</returns>
        public IList<ProfileView> GetProfileViews()
        {
            Utils.LogMethodStart(this);

            var profileViews = _alignmentService.GetProfileViews(_alignment)
                .Select(x => new ProfileView(x)).ToList();

            Utils.LogMethodEnd(this);

            return profileViews;
        }


        /// <summary>
        /// Factorial function. Returns a double to allow for values bigger than 20!
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private double Factorial(int f)
        {
            if (f < 0)
            {
                throw new Exception("Factorial is undefined for negative numbers.");
            }

            double output = 1;

            if (f == 0)
            {
                return output;
            }
            else
            {
                for (int i = 1; i <= f; i++)
                {
                    output *= i;
                }
            }

            return output;
        }

        /// <summary>
        /// Returns the list of Dynamo curves that defines the Alignment.
        /// </summary>
        /// <param name="tessellation">The length of the tessellation for spirals, by default is 1 unit.</param>
        /// <returns>A list of curves that represent the Alignment.</returns>
        /// <remarks>The tool returns only lines and arcs.</remarks>
        /// 
        public IList<Curve> GetCurves(double tessellation = 1)
        {
            Utils.LogMethodStart(this);

            var data = _alignmentService.GetCurves(_alignment, tessellation);

            var output = data.Select(AlignmentConverter.ToDynamo).ToList();

            Utils.LogMethodEnd(this);

            return output;
        }

        /// <summary>
        /// Returns the Sample Lines parameters associated with the alignment.
        /// </summary>
        /// <returns></returns>
        [MultiReturn(new string[] { "station", "lengthLeft", "lengthRight", "elevationMin", "elevationMax" })]
        public IList<Dictionary<string, object>> SampleLinesParameters()
        {
            Utils.LogMethodStart(this);

            var data = _alignmentService.GetSampleLineParameters(_alignment);

            var output = data.Select(SampleLineGroupConverter.ToDynamo).ToList();

            Utils.LogMethodEnd(this);

            return output;
        }

        /// <summary>
        /// Returns the station, offset and elevation of a point from the alignment.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        [MultiReturn(new string[] { "Station", "Offset", "Elevation" })]
        public Dictionary<string, object> GetStationOffsetElevation(Point point)
        {
            Utils.LogMethodStart(this);

            var soeData = _alignmentService.GetStationOffset(_alignment, point.X, point.Y);

            Utils.LogMethodEnd(this);

            return new Dictionary<string, object>() { { "Station", soeData.Station }, { "Offset", soeData.Offset }, { "Elevation", point.Z } };
        }

        /// <summary>
        /// Returns a CoordinateSystem along the Alignment at the specified station.
        /// </summary>
        /// <param name="station">The station value.</param>
        /// <param name="offset">The offset value.</param>
        /// <param name="elevation">The elevation value.</param>
        /// <returns></returns>
        public CoordinateSystem CoordinateSystemByStation(double station, double offset = 0, double elevation = 0)
        {
            Utils.LogMethodStart(this);

            var pointData = _alignmentService.PointByStationOffset(_alignment, station, offset);

            Point point = GeometryConverter.ToProtoPoint(pointData);

            var pointDataX = _alignmentService.PointByStationOffset(_alignment, station, offset + 1);

            Point pointX = GeometryConverter.ToProtoPoint(pointDataX);

            Vector x = Vector.ByTwoPoints(point, pointX).Normalized();

            Vector y = Vector.ZAxis().Cross(x).Normalized();

            CoordinateSystem cs = CoordinateSystem.ByOriginVectors(point, x, y);

            point.Dispose();
            pointX.Dispose();
            x.Dispose();
            y.Dispose();

            Utils.LogMethodEnd(this);

            return cs;
        }

        /// <summary>
        /// Returns a Point along the Alignment at the specified station.
        /// </summary>
        /// <param name="station">The station value.</param>
        /// <param name="offset">The offset value.</param>
        /// <param name="elevation">The elevation value.</param>
        /// <returns></returns>
        public Point PointByStationOffsetElevation(double station, double offset = 0, double elevation = 0)
        {
            Utils.LogMethodStart(this);

            var pointData = _alignmentService.PointByStationOffset(_alignment, station, offset);

            pointData.Z = elevation;

            Point point = GeometryConverter.ToProtoPoint(pointData);

            Utils.LogMethodEnd(this);

            return point;
        }

        /// <summary>
        /// Convert station value to absolute station value based on station equations
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public double AbsoluteStation(double station)
        {
            int i = 0;

            while (station > StationAhead[i]) 
            {
                if (i < StationAhead.Length - 1)
                {
                    i++;
                }
                else
                {
                    break;
                }
            }
            double distance = station - StationAhead[i - 1];
            double stationValue = EquationStations[i - 1] + distance;

            return stationValue;
        }

        /// <summary>
        /// Convert absolute station value to normal station value
        /// </summary>
        /// <param name="absStation"></param>
        /// <returns></returns>
        public string StationFromAbsoluteStation(double absStation)
        {
            Utils.LogMethodStart(this);

            var data = _alignmentService.StationFromAbsoluteStation(_alignment, absStation);

            Utils.LogMethodEnd(this);

            return data;
        }

        /// <summary>
        /// Public textual representation in the Dynamo node preview.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Alignment(Name = {Name}, Length = {Length}, Start = {Start}, End = {End})";
        }
        #endregion
    }
}
