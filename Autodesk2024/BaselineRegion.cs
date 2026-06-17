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
using Autodesk.DesignScript.Geometry;
using System.Xml;
using System.Globalization;
using CivilConnection.Interop.Wrappers;

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

        #region PRIVATE PROPERTIES

        private Baseline _baseline;
        private double _start;
        private double _end;
        private double[] _stations;
        private IList<Featureline> _featurelines = new List<Featureline>();
        private IList<Subassembly> _subassemblies = new List<Subassembly>();
        // private IList<AppliedAssembly> _appliedAssemblies = new List<AppliedAssembly>();
        private int _index;
        private IList<IList<IList<AppliedSubassemblyShape>>> _shapes = new List<IList<IList<AppliedSubassemblyShape>>>();
        private IList<IList<IList<AppliedSubassemblyLink>>> _links = new List<IList<IList<AppliedSubassemblyLink>>>();
        private string _assembly;

        /// <summary>
        /// Gets the internal element.
        /// </summary>
        /// <value>
        /// The internal element.
        /// </value>
        internal object InternalElement => _baselineRegion;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Gets the region start station.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public double Start => _baselineRegion.StartStation;
        /// <summary>
        /// Gets theregion end station.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public double End => _baselineRegion.EndStation;
        /// <summary>
        /// Gets the region stations.
        /// </summary>
        /// <value>
        /// The stations.
        /// </value>
        public double[] Stations { get { return _stations; } }
        /// <summary>
        /// Gets the region subassemblies.
        /// </summary>
        /// <value>
        /// The subassemblies.
        /// </value>
        public IList<Subassembly> Subassemblies
        {
            get
            {
                if (_subassemblies.Count != 0)
                {
                    return _subassemblies;
                }

                // Calculate these objects only when they are required
                foreach (var asa in _baselineRegion.AppliedAssemblies.First().AppliedSubassemblies)
                {
                    try
                    {
                        // this._appliedAssemblies.Add(new AppliedAssembly(this, a, a.Corridor));  // TODO: verify why this is a list instead of a single applied assembly...

                        try
                        {
                            _subassemblies.Add(new Subassembly(asa.SubassemblyDbEntity, asa.Corridor));
                        }
                        catch (Exception ex)
                        {
                            _subassemblies.Add(null);

                            Utils.Log($"ERROR: {ex.Message}");

                            throw new Exception("Subassemblies Failed\n\n" + ex.Message);
                        }

                        // break;
                    }
                    catch (Exception ex)
                    {
                        Utils.Log(string.Format("ERROR: {0}", ex.Message));

                        throw new Exception("Applied Assemblies Failed\n\n" + ex.Message);
                    }
                }

                return _subassemblies;
            }
        }

        /// <summary>
        /// Gets the relative starting station for the BaselineRegion.
        /// </summary>
        public double RelativeStart { get { return 0; } }

        /// <summary>
        /// Gets the relative ending station for the BaselineRegion.
        /// </summary>
        public double RelativeEnd { get { return _end - _start; } }

        /// <summary>
        /// Gets the normalized starting station for the BaselineRegion.
        /// </summary>
        public double NormalizedStart { get { return 0; } }

        /// <summary>
        /// Gets the normalized starting station for the BaselineRegion.
        /// </summary>
        public double NormalizedEnd { get { return 1; } }

        /// <summary>
        /// Gets the Baselineregion index value.
        /// </summary>
        public int Index { get { return _index; } }

        
        /// <summary>
        /// Gets the Shapes profile of the applied subassemblies in the BaselineRegion.
        /// </summary>
        public IList<IList<IList<AppliedSubassemblyShape>>> Shapes
        {
            get
            {
                if (_shapes.Count != 0)
                {
                    return _shapes;
                }

                Utils.LogMethodStart(this);

                string xmlPath = System.IO.Path.Combine(Environment.GetEnvironmentVariable("TMP", EnvironmentVariableTarget.User), "CorridorShapes.xml");  // Revit 2020 changed the path to the temp at a session level

                Utils.Log(xmlPath);

                // Check for installation of CivilPython
                if (!Utils.EnsureCivilPythonInstalled())
                {
                    return null;
                }

                var doc = _baseline.InternalElement.Alignment.Document;

                doc.SendCommand(string.Format("-ExportSubassemblyShapesToXML\n{0}\n{1}\n{2}\n", _baseline.InternalElement.Corridor.Handle, _baseline.Index, Index));

                DateTime start = DateTime.Now;


                while (true)
                {
                    if (System.IO.File.Exists(xmlPath))
                    {
                        if (System.IO.File.GetLastWriteTime(xmlPath) > start)
                        {
                            start = System.IO.File.GetLastWriteTime(xmlPath);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                Utils.Log("XML acquired.");

                if (System.IO.File.Exists(xmlPath))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);

                    Utils.Log("Processing XML...");

                    foreach (XmlElement corridor in xmlDoc.GetElementsByTagName("Corridor").Cast<XmlElement>().First(x => x.Attributes["Name"].Value == _baseline.CorridorName))
                    {

                        foreach (XmlElement baseline in corridor.GetElementsByTagName("Baseline"))
                        {


                            IList<IList<AppliedSubassemblyShape>> baselineShapes = new List<IList<AppliedSubassemblyShape>>();

                            foreach (XmlElement region in baseline.GetElementsByTagName("Region"))
                            {

                                IList<AppliedSubassemblyShape> regionShapes = new List<AppliedSubassemblyShape>();

                                foreach (XmlElement shape in region.GetElementsByTagName("Shape"))
                                {

                                    IList<Point> points = new List<Point>();

                                    string corrName = shape.Attributes["Corridor"].Value;
                                    string baselineIndex = shape.Attributes["BaselineIndex"].Value;
                                    string regionIndex = shape.Attributes["RegionIndex"].Value;
                                    string assembly = shape.Attributes["AssemblyName"].Value;
                                    string subassembly = shape.Attributes["SubassemblyName"].Value;
                                    string handle = shape.Attributes["Handle"].Value;
                                    string index = shape.Attributes["ShapeIndex"].Value;
                                    double station = Convert.ToDouble(shape.Attributes["Station"].Value, CultureInfo.InvariantCulture);

                                    string name = string.Join("_", corrName, baselineIndex, regionIndex, assembly, subassembly, handle, index);

                                    foreach (XmlElement p in shape.GetElementsByTagName("Point"))
                                    {
                                        double x = 0;
                                        double y = 0;
                                        double z = 0;

                                        try
                                        {
                                            x = Convert.ToDouble(p.Attributes["X"].Value, CultureInfo.InvariantCulture);
                                        }
                                        catch (Exception ex)
                                        {
                                            Utils.Log(string.Format("ERROR: {0} X {1}", station, ex.Message));
                                        }
                                        try
                                        {
                                            y = Convert.ToDouble(p.Attributes["Y"].Value, CultureInfo.InvariantCulture);
                                        }
                                        catch (Exception ex)
                                        {
                                            Utils.Log(string.Format("ERROR: {0} Y {1}", station, ex.Message));
                                        }

                                        try
                                        {
                                            z = Convert.ToDouble(p.Attributes["Z"].Value, CultureInfo.InvariantCulture);  // When Z is NaN because the profile is not defined at station
                                        }
                                        catch (Exception ex)
                                        {
                                            Utils.Log(string.Format("ERROR: {0} Z {1}", station, ex.Message));

                                            continue;
                                        }

                                        points.Add(Point.ByCoordinates(x, y, z));
                                    }

                                    IList<string> codes = new List<string>();

                                    foreach (XmlElement c in shape.GetElementsByTagName("Code"))
                                    {
                                        string code = c.Attributes["Name"].Value;

                                        if (!codes.Contains(code))
                                        {
                                            codes.Add(code);
                                        }
                                    }

                                    points = Point.PruneDuplicates(points);

                                    if (points.Count > 2)  // 20190715
                                    {
                                        PolyCurve pc = PolyCurve.ByPoints(points, true);

                                        AppliedSubassemblyShape appSubShape = null;

                                        try
                                        {
                                            appSubShape = new AppliedSubassemblyShape(name, pc, codes, station);
                                        }
                                        catch (Exception ex)
                                        {
                                            Utils.Log(string.Format("ERROR: {0} {1} {2}", name, station, ex.Message));
                                        }

                                        if (appSubShape != null)
                                        {
                                            regionShapes.Add(appSubShape);
                                        }
                                    }
                                    else
                                    {
                                        string.Format("ERROR: Not enough points to make a closed loop: {0} {1}", name, station);
                                    }

                                    foreach (var item in points)
                                    {
                                        if (item != null)
                                        {
                                            item.Dispose();
                                        }
                                    }
                                }

                                baselineShapes.Add(regionShapes);
                            }

                            _shapes.Add(baselineShapes);
                        }
                    }
                }
                else
                {
                    Utils.Log("ERROR: Failed to locate CorridorShapes.xml in the Temp folder.");
                }

                Utils.LogMethodEnd(this);

                return _shapes;
            }
        }

        
        /// <summary>
        /// Gets the Links profile of the applied subassemblies in the BaselineRegion.
        /// </summary>
        //public List<List<List<Geometry>>> Links
        public IList<IList<IList<AppliedSubassemblyLink>>> Links
        {
            get
            {
                if (this._links.Count != 0)
                {
                    return this._links;
                }

                Utils.LogMethodStart(this);

                IList<IList<IList<AppliedSubassemblyLink>>> corridorLinks = new List<IList<IList<AppliedSubassemblyLink>>>();

                string xmlPath = System.IO.Path.Combine(Environment.GetEnvironmentVariable("TMP", EnvironmentVariableTarget.User), "CorridorLinks.xml");  // Revit 2020 changed the path to the temp at a session level

                Utils.Log(xmlPath);

                // Check for installation of CivilPython
                if (!Utils.EnsureCivilPythonInstalled())
                {
                    return null;
                }

                var doc = _baseline.InternalElement.Alignment.Document;

                doc.SendCommand(string.Format("-ExportSubassemblyLinksToXML\n{0}\n{1}\n{2}\n", _baseline.InternalElement.Corridor.Handle, _baseline.Index, Index));

                DateTime start = DateTime.Now;

                while (true)
                {
                    if (System.IO.File.Exists(xmlPath))
                    {
                        if (System.IO.File.GetLastWriteTime(xmlPath) > start)
                        {
                            start = System.IO.File.GetLastWriteTime(xmlPath);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                Utils.Log("XML acquired.");

                if (System.IO.File.Exists(xmlPath))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);

                    foreach (XmlElement corridor in xmlDoc.GetElementsByTagName("Corridor").Cast<XmlElement>().First(x => x.Attributes["Name"].Value == _baseline.CorridorName))
                    {
                        foreach (XmlElement baseline in corridor.GetElementsByTagName("Baseline"))
                        {
                            IList<IList<AppliedSubassemblyLink>> baselineLinks = new List<IList<AppliedSubassemblyLink>>();

                            foreach (XmlElement region in baseline.GetElementsByTagName("Region"))
                            {
                                IList<AppliedSubassemblyLink> regionLinks = new List<AppliedSubassemblyLink>();

                                foreach (XmlElement link in region.GetElementsByTagName("Link"))
                                {
                                    IList<Point> points = new List<Point>();

                                    string corrName = link.Attributes["Corridor"].Value;
                                    string baselineIndex = link.Attributes["BaselineIndex"].Value;
                                    string regionIndex = link.Attributes["RegionIndex"].Value;
                                    string assembly = link.Attributes["AssemblyName"].Value;
                                    string subassembly = link.Attributes["SubassemblyName"].Value;
                                    string handle = link.Attributes["Handle"].Value;
                                    string index = link.Attributes["LinkIndex"].Value;
                                    double station = Convert.ToDouble(link.Attributes["Station"].Value, CultureInfo.InvariantCulture);

                                    string name = string.Join("_", corrName, baselineIndex, regionIndex, assembly, subassembly, handle, index);

                                    foreach (XmlElement p in link.GetElementsByTagName("Point"))
                                    {
                                        double x = 0;
                                        double y = 0;
                                        double z = 0;

                                        try
                                        {
                                            x = Convert.ToDouble(p.Attributes["X"].Value, CultureInfo.InvariantCulture);
                                        }
                                        catch (Exception ex)
                                        {
                                            Utils.Log($"ERROR: {station} X {ex.Message}");
                                        }
                                        try
                                        {
                                            y = Convert.ToDouble(p.Attributes["Y"].Value, CultureInfo.InvariantCulture);
                                        }
                                        catch (Exception ex)
                                        {
                                            Utils.Log($"ERROR: {station} Y {ex.Message}");
                                        }

                                        try
                                        {
                                            z = Convert.ToDouble(p.Attributes["Z"].Value, CultureInfo.InvariantCulture);  // When Z is NaN because the profile is not defined at station
                                        }
                                        catch (Exception ex)
                                        {
                                            Utils.Log($"ERROR: {station} Z {ex.Message}");
                                            continue;
                                        }

                                        points.Add(Point.ByCoordinates(x, y, z));
                                    }

                                    IList<string> codes = new List<string>();

                                    foreach (XmlElement c in link.GetElementsByTagName("Code"))
                                    {
                                        string code = c.Attributes["Name"].Value;
                                        if (!codes.Contains(code))
                                        {
                                            codes.Add(code);
                                        }
                                    }

                                    points = Point.PruneDuplicates(points);

                                    if (points.Count > 1)
                                    {
                                        PolyCurve pc = PolyCurve.ByPoints(points);

                                        AppliedSubassemblyLink appSubLink = null;

                                        try
                                        {
                                            appSubLink = new AppliedSubassemblyLink(name, pc, codes, station);
                                        }
                                        catch (Exception ex)
                                        {
                                            Utils.Log($"ERROR: {name} {station} {ex.Message}");
                                        }

                                        if (appSubLink != null)
                                        {
                                            regionLinks.Add(appSubLink);
                                        }
                                        else
                                        {
                                            Utils.Log($"ERROR: The AppliedSubassemblyLink is null, Station: {station}");
                                        }
                                    }
                                    else
                                    {
                                        Utils.Log($"ERROR: Not enough points");
                                    }

                                    foreach (var item in points)
                                    {
                                        if(item != null)
                                        {
                                            item.Dispose();
                                        }
                                    }
                                }

                                baselineLinks.Add(regionLinks);
                            }

                            _links.Add(baselineLinks);
                        }
                    }
                }
                else
                {
                    Utils.Log("ERROR: Failed to locate CorridorLinks.xml in the Temp folder.");
                }

                Utils.LogMethodEnd(this);

                return _links;
            }
        }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="baseline">The baseline that holds the baseline region.</param>
        /// <param name="baselineRegion">The internal AeccBaselineRegion</param>
        /// <param name="i">The baseline region index</param>
        internal BaselineRegion(Baseline baseline, BaselineRegionWrapper baselineRegion, int i)
        {
            _baseline = baseline;

            _baselineRegion = baselineRegion;

            _index = i;

            try
            {
                _assembly = baselineRegion.AssemblyName;
            }
            catch (Exception ex)
            {
                Utils.Log($"ERROR: Assembly Name Failed\t{ex.Message}");

                _assembly = _index.ToString();
            }

            try
            {
                _start = baselineRegion.StartStation; //  Math.Round(blr.StartStation, 5);  // TODO get rid of the roundings
            }
            catch (Exception ex)
            {
                Utils.Log($"ERROR: Start Station Failed\t{ex.Message}");

                throw new Exception("Start Station Failed\n\n" + ex.Message);
            }

            try
            {

                this._end = baselineRegion.EndStation; //  Math.Round(blr.EndStation, 5);
            }
            catch (Exception ex)
            {
                Utils.Log($"ERROR: End Station Failed\t{ex.Message}");

                throw new Exception("End Station Failed\n\n" + ex.Message);
            }

            try
            {
                _stations = baselineRegion.GetSortedStations();
            }
            catch (Exception ex)
            {
                Utils.Log($"ERROR: Sorted Stations Failed\t{ex.Message}");

                throw new Exception("Sorted Stations Failed\n\n" + ex.Message);
            }
        }

        #endregion

        #region PUBLIC METHODS
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
