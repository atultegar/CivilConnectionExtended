# CivilConnection
## AbstractMEPCurve
    /// <summary>
    /// AbstratMEPCurve object Type. Base class for Revti MEP Curve objects.
    /// </summary>
    /// <seealso cref="Revit.Elements.Element" />
    
### InternalSetMEPCurve

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Internals the set mep curve.
        /// </summary>
        /// <param name="fi">The MEPCurve instance</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InternalSetMEPCurveType

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Internals the type of the set mep curve.
        /// </summary>
        /// <param name="type">The type.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InternalSetPosition

        /// <summary>
        /// Internals the set position.
        /// </summary>
        /// <param name="start">The start point.</param>
        /// <param name="end">The end point.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Connectors

        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Connectors on the MEPCurve.
        /// </summary>
        /// <returns></returns>
        
Return Type: CivilConnection.MEP.Connector[]
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString

        /// <summary>
        /// Returns a text that represents this instance.
        /// </summary>
        /// <returns>
        /// A text that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## Alignment
    /// <summary>
    /// Alignment object type.
    /// </summary>
    
### _GetGeometryStations

Return Type: double[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### _GetPIStations

Return Type: double[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### _GetSuperTransStations

Return Type: double[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### _GetEquationStations

Return Type: double[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### _GetStationsAhead

Return Type: double[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPolygonal
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
        
Return Type: Alignment
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetProfiles
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Returns the list of vertical Profiles associated to the Alignment.
        /// </summary>
        /// <returns>The list of associated Profiles.</returns>
        
Return Type: IList<Profile>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetProfileViews

        /// <summary>
        /// Returns the list of vertical ProfileViews associated to the Alignment.
        /// </summary>
        /// <returns>The list of assocaited ProfileViews.</returns>
        
Return Type: IList<ProfileView>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Factorial


        /// <summary>
        /// Factorial function. Returns a double to allow for values bigger than 20!
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        
Return Type: double
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetCurves

        /// <summary>
        /// Returns the list of Dynamo curves that defines the Alignment.
        /// </summary>
        /// <param name="tessellation">The length of the tessellation for spirals, by default is 1 unit.</param>
        /// <returns>A list of curves that represent the Alignment.</returns>
        /// <remarks>The tool returns only lines and arcs.</remarks>
        /// 
        
Return Type: IList<Curve>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SortCurves


        /// <summary>
        /// Sorts a list of Dynamo Curves.
        /// </summary>
        /// <param name="curves">THe list of Curves.</param>
        /// <returns></returns>
        
Return Type: IList<Curve>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SampleLinesParameters

        /// <summary>
        /// Returns the Sample Lines parameters associated with the alignment.
        /// </summary>
        /// <returns></returns>
        
Return Type: IList<Dictionary<string, object>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetStationOffsetElevation

        /// <summary>
        /// Returns the station, offset and elevation of a point from the alignment.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CoordinateSystemByStation

        /// <summary>
        /// Returns a CoordinateSystem along the Alignment at the specified station.
        /// </summary>
        /// <param name="station">The station value.</param>
        /// <param name="offset">The offset value.</param>
        /// <param name="elevation">The elevation value.</param>
        /// <returns></returns>
        
Return Type: CoordinateSystem
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### PointByStationOffsetElevation

        /// <summary>
        /// Returns a Point along the Alignment at the specified station.
        /// </summary>
        /// <param name="station">The station value.</param>
        /// <param name="offset">The offset value.</param>
        /// <param name="elevation">The elevation value.</param>
        /// <returns></returns>
        
Return Type: Point
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AbsoluteStation

        /// <summary>
        /// Convert station value to absolute station value based on station equations
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### StationFromAbsoluteStation

        /// <summary>
        /// Convert absolute station value to normal station value
        /// </summary>
        /// <param name="absStation"></param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString

        /// <summary>
        /// Public textual representation in the Dynamo node preview.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## AppliedAssembly
    /// <summary>
    /// AppliedAssembly object type.
    /// </summary>
    
### ToString

        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Public textual representation in the Dynamo node preview.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## AbstractAppliedSubassemblyGeometryObject


     /// <summary>
     /// Base class for applied subassemblies geometry objects.
     /// </summary>
    
## AppliedSubassemblyLink

    //[IsVisibleInDynamoLibrary(false)]
     ///<summary>
     ///The Applied Subassembly link object
     ///</summary>
    
### ToString
        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Public textual representation of the Dynamo node preview
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## AppliedSubassemblyShape

    //[IsVisibleInDynamoLibrary(false)]
     ///<summary>
     ///The Applied Subassembly shape object
     ///</summary>
    
### ToString
        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Public textual representation of the Dynamo node preview
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## Baseline

    /// <summary>
    /// Baseline obejct type.
    /// </summary>
    
### BaselinePolyCurves

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Private method to retrieve Baseline PolyCurves.
        /// </summary>
        /// <returns>A list of PolyCurves for each BaselineRegion.</returns>
        /// <remarks>In case of large dataset, the Geometry Working Range wiill return a warning. Set the Geometry Working Range to Medium.</remarks>
        
Return Type: IList<PolyCurve>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetOffsetAlignment

        /// <summary>
        /// Returns the Offset Alignment name.
        /// </summary>
        /// <returns>The names of the offset Alignments, otherwise "None".</returns>
        
Return Type: string
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFeaturelinesFromXML

        /// <summary>
        /// Returns a collection of Featurelines in the Baseline for the given code organized by regions.
        /// </summary>
        /// <param name="code">The code of the Featurelines.</param>
        /// <returns></returns>
        
Return Type: IList<IList<Featureline>>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetBaselineRegions

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Returns the list of BaselineRegions in the Baseline.
        /// </summary>
        /// <returns>A list of BaselineRegions.</returns>
        
Return Type: IList<BaselineRegion>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetBaselineRegionIndexByStation

        /// <summary>
        /// Returns the index of the BaselineRegion in the Baseline that contains the station value.
        /// </summary>
        /// <param name="station">A double that represents a station along the corridor.</param>
        /// <returns>An integer for the BaselineRegion that contains the station value.</returns>
        /// <remarks>If the station input is less than the first station it returns 0. If the station input is greater than the last station it returns the number of BaselineRegions - 1.</remarks>
        
Return Type: int
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### PointByStationOffsetElevation

        /// <summary>
        /// Returns a point relative to the Baseline with station, offset and elevation.
        /// </summary>
        /// <param name="station">The distance measured along the Alignment.</param>
        /// <param name="offset">The horizontal displacement from the Baseline measured at a given station.</param>
        /// <param name="elevation">The vertical displacement from the Baseline measured at a given station.</param>
        /// <returns>A Dynamo Point.</returns>
        
Return Type: Point
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CoordinateSystemByStation

        /// <summary>
        /// Returns the Baseline CoordinateSystem at a station value.
        /// </summary>
        /// <param name="station">The input station.</param>
        /// <returns></returns>
        /// <remarks>if the station falls outside of the corridor it returns the Identity Coordinate System.</remarks>
        
Return Type: CoordinateSystem
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetCoordinateSystemByPoint

        /// <summary>
        /// Returns the closest Baseline CoordinateSystem and uses the point as new origin.
        /// </summary>
        /// <param name="point">The input Point.</param>
        /// <returns>The CoordinateSystem.</returns>
        /// <remarks>if the station falls outside of the corridor it returns the Identity Coordinate System.</remarks>
        
Return Type: CoordinateSystem
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetStationOffsetElevationByPoint

        /// <summary>
        /// Returns the station, offset, elevation of the point with respect to the Baseline.
        /// </summary>
        /// <param name="point">The input Point.</param>
        /// <returns>A double[].</returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetArrayStationOffsetElevationByPoint

        /// <summary>
        /// Gets the array station offset elevation by point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>A double[].</returns>
        
Return Type: double[]
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetOffsetBaselinesAlignments

        /// <summary>
        /// Returns Offset Alignments from the Baseline.
        /// </summary>
        /// <returns>The offset Alignments. Null if there are any.</returns>
        
Return Type: IList<Alignment>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFeaturelinesByCodeStation

        /// <summary>
        /// Gets the featurelines by code and station
        /// </summary>
        /// <param name="code">the Featurelines code.</param>
        /// <param name="station">the station used to select the featurelines.</param>
        /// <returns></returns>
        
Return Type: IList<Featureline>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFeaturelinesByCode_

        /// <summary>
        /// Gets the featurelines by code
        /// </summary>
        /// <param name="code">the Featurelines code.</param>
        /// <returns></returns>
        
Return Type: IList<IList<Featureline>>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFeaturelinesByCode

        /// <summary>
        /// Gets the featurelines by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        
Return Type: IList<IList<Featureline>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString

        /// <summary>
        /// Public textual representation of the Dynamo node preview
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## BaselineRegion
    /// <summary>
    /// BaselineRegion object type.
    /// </summary>
    
### ToString

        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Public textual representation of the Dynamo node preview
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## CivilApplication
    /// <summary>
    /// CivilApplication object type.
    /// </summary>
    
### GetActiveObjectExt

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rclsid"></param>
        /// <param name="reserved"></param>
        /// <param name="ppunk"></param>
        /// <returns></returns>
        
Return Type: int
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetApplication

        /// <summary>
        /// Gets the application
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        
Return Type: AeccRoadwayApplication
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetDocuments

        /// <summary>
        /// Returns the list of Civil Documents opened in Civil 3D.
        /// </summary>
        /// <returns></returns>
        
Return Type: IList<CivilDocument>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetDocumentByName

        /// <summary>
        /// Returns the Civil Documents opened in Civil 3D with the same name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        
Return Type: CivilDocument
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### UpdatePeriodically

        /// <summary>
        /// Enables the Run Periodically mode and updates the connection with Civil 3D.
        /// </summary>
        /// <returns></returns>
        
Return Type: CivilApplication
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### WriteToLog

        /// <summary>
        /// Writes a message to the log file
        /// </summary>
        /// <param name="data">The data that is passed through</param>
        /// <param name="message">An optional message to write to the log.</param>
        /// <returns></returns>
        
Return Type: object
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString

        /// <summary>
        /// Public textual representation of the Dynamo node preview.
        /// </summary>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SetUnits

        /// <summary>
        /// Setting Revit units based on Civil 3D document units
        /// </summary>
        /// <param name="revitDoc"></param>
        /// <exception cref="Exception"></exception>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## CivilDocument
    /// <summary>
    /// The CivilDocument class
    /// </summary>
    
### DumpLandXML
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Creates a LandXML from the Civil Document
        /// </summary>
        /// <returns></returns>
        
Return Type: string
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetLandFeaturelines

        /// <summary>
        /// Gets the land featurelines.
        /// </summary>
        /// <param name="xmlPath">The XML path for the LandFeaturerline properties.</param>
        /// <returns></returns>
        /// 
        
Return Type: IList<LandFeatureline>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetCorridors
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Gets the corridors.
        /// </summary>
        /// <returns></returns>
        
Return Type: IList<Corridor>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetCorridorByName

        /// <summary>
        /// Get the corridor by name.
        /// </summary>
        /// <param name="name">The corridor name.</param>
        /// <returns></returns>
        
Return Type: Corridor
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetAlignments

        /// <summary>
        /// Gets the alignments.
        /// </summary>
        /// <returns></returns>
        
Return Type: IList<Alignment>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetAlignmentByName

        /// <summary>
        /// Gets alignment by name.
        /// </summary>
        /// <param name="name">The alignment name.</param>
        /// <returns></returns>
        
Return Type: Alignment
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetSurfaces

        /// <summary>
        /// Gets all surfaces in the document
        /// </summary>
        /// <returns>
        /// List of surfaces
        /// </returns>
        
Return Type: IList<CivilSurface>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetSurfaceByName

        /// <summary>
        /// Gets surface by name.
        /// </summary>
        /// <param name="name">The name of the surface</param>
        /// <returns>
        /// Civil Surface
        /// </returns>
        
Return Type: CivilSurface
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddArc

        #region AUTOCAD METHODS
        /// <summary>
        /// Adds the arc to the document.
        /// </summary>
        /// <param name="arc">The arc.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddCircle

        /// <summary>
        /// Adds the circle to the document.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddLWPolylineByPoints

        /// <summary>
        /// Adds the 2D polyline by points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddPolylineByCurve

        /// <summary>
        /// Adds the 3D polyline by curve.
        /// </summary>
        /// <param name="curve">The curve.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddPolylineByCurve

        /// <summary>
        /// Adds the 3D polylines by curve.
        /// </summary>
        /// <param name="curves">The curves.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddRegionByPatch

        /// <summary>
        /// Adds the region by closed profile.
        /// </summary>
        /// <param name="closedCurve">The closed curve.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddExtrudedSolidByPoints

        /// <summary>
        /// Creates a closed profile form the points and adds the extruded solid.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="height">The height. By Default is equal to 1.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddExtrudedSolidByPatch

        /// <summary>
        /// Adds the extruded solid by closed profile.
        /// </summary>
        /// <param name="closedCurve">The closed curve.</param>
        /// <param name="height">The height. By Default is equal to 1.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddExtrudedSolidByCurves

        /// <summary>
        /// Adds multiple extruded solid by closed curves.
        /// </summary>
        /// <param name="curves">The curves.</param>
        /// <param name="height">The height. By Default is equal to 1.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddLayer

        /// <summary>
        /// Adds a new layer to the Civil Document by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddText

        /// <summary>
        /// Creates a text in the CivilDocument and rotates it to match the plane.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="point">The point.</param>
        /// <param name="textHeight">Height of the text.</param>
        /// <param name="layer">The layer.</param>
        /// <param name="cs">The cs.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CutSolidsByPatch

        /// <summary>
        /// Creates an extrusion based on a closed curve (Polycurve, Polygon or Rectangle) profile to be used to cut the solids in the Civil Document.
        /// </summary>
        /// <param name="closedCurve">The closed curve.</param>
        /// <param name="height">The height. By Default is equal to 1.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CutSolidsByCurves

        /// <summary>
        /// Creates an extrusion based on a profile defined by a set of curves profile to be used to cut the solids in the Civil Document.
        /// </summary>
        /// <param name="closedCurves">The closed curves.</param>
        /// <param name="height">The height. By Default is equal to 1.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CutSolidsByGeometry

        /// <summary>
        /// Creates a solid to be used to cut the solids in the Civil 3D Document.
        /// </summary>
        /// <param name="geometry">The solid geometry.</param>
        /// <param name="layer">The layer where to crerate the cutting solid.</param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ImportGeometry

        /// <summary>
        /// Import the geometry from Dynamo into the Civil 3D Document.
        /// </summary>
        /// <param name="geometry">The geometry.</param>
        /// <param name="layer">The layer where to crerate the solid.</param>
        /// <returns></returns>
        
Return Type: IList<string>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### LinkElement

        /// <summary>
        /// Links the geometry associated to a Revit object into Civil 3D.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SendCommand

        /// <summary>
        /// Send Command to the Civil Document.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SliceSolidsByPlane

        /// <summary>
        /// Slices the solids in Civil 3D using a Dynamo Plane.
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddCivilPoint

        #endregion

        #region CIVIL 3D METHODS

        /// <summary>
        /// Adds the civil point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddDBPoint

        /// <summary>
        /// Adds the DB Point.
        /// </summary>
        /// <param name="point"></param>
        /// <returns>Object Handle</returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddDBPointsByPoints

        /// <summary>
        /// Adds the DB Points.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="layer"></param>
        /// <returns>Object Handles</returns>
        
Return Type: List<string>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddCivilPointGroup

        /// <summary>
        /// Adds the civil point group.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddTINSurfaceByPoints

        /// <summary>
        /// Gets the Civil point groups.
        /// </summary>
        /// <returns></returns>
        //[MultiReturn(new string[] { "PointGroupNames", "Points" })]
        //public Dictionary<string, object> GetPointGroups()
        //{
        //    var dict = Utils.GetPointGroups(this._document);

        //    return new Dictionary<string, object>() { { "PointGroupNames", dict.Keys }, { "Points", dict.Values } };
        //}


        /// <summary>
        /// Adds the tin surface by points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="name">The name.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        //[IsVisibleInDynamoLibrary(false)]
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddPropertySetDefinition

        /// <summary>
        /// Add PropertySetDefinition to document.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CreatePropertySets

        /// <summary>
        /// Create propertyset and setting values for objects
        /// </summary>
        /// <param name="psetDefinitionName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddPropertySetToObjects

        /// <summary>
        /// Adds a property set definition to the specified objects using the provided parameters and property set
        /// definition name.
        /// </summary>        
        /// <param name="objectHandles">A list of object handles representing the target objects to which the property set will be applied.</param>
        /// <param name="parameters">A list of parameter lists, where each inner list contains parameters to be associated with the corresponding
        /// object.</param>
        /// <param name="propertySetDefName">The name of the property set definition to create and assign to the objects.</param>
        /// <returns>Result of the operation. Returns an error message if the operation fails</returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString
        #endregion

        /// <summary>
        /// Public textual representation of the Dynamo node preview
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## CivilSurface
    /// <summary>
    /// Civil 3D Surface object
    /// </summary>
    
### GetElevationAtPoint
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Get elevation of surface at points
        /// </summary>
        /// <param name="points">The points to process</param>
        /// <returns>
        /// The List of Elevations
        /// </returns>
        
Return Type: List<object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetElevationsAlongLine

        /// <summary>
        /// Gets all surface points along line
        /// </summary>
        /// <param name="lines">The lines to process</param>
        /// <returns>
        /// The List of Points
        /// </returns>
        
Return Type: List<List<Point>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetPointsAlongLine

        /// <summary>
        /// Gets all surface points along line
        /// </summary>
        /// <param name="lines">The lines to process</param>
        /// <returns>
        /// The List of Points
        /// </returns>
        
Return Type: List<List<Point>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetSurfacePoints

        /// <summary>
        /// Get all surface points
        /// </summary>
        /// <returns>
        /// The List of Points
        /// </returns>
        
Return Type: List<Point>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetPointsBetweenLowerUpperLimits

        /// <summary>
        /// Gets all surface points between lower and upper limit points.
        /// </summary>
        /// <param name="lowerLeftPoint">The minmum point.</param>
        /// <param name="upperRightPoint">The maximum pont.</param>
        /// <returns>
        /// The List of Points
        /// </returns>
        
Return Type: List<Point>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetPointsInBoundingBox

        /// <summary>
        /// Gets all surface points in the BoundingBox.
        /// </summary>
        /// <param name="boundingBox">The BoundingBox used for the containment test.</param>
        /// <returns>
        /// The List of Points
        /// </returns>
        
Return Type: List<Point>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetPointsInBoundary

        /// <summary>
        /// Get surface points inside a closed boundary
        /// </summary>
        /// <param name="boundary">A closed curve</param>
        /// <param name="tolerance">A value between 0 and 1 to define the precision of the tessellation along non-straight curves.</param>
        /// <returns>
        /// The List of Points
        /// </returns>
        
Return Type: List<Point>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetTrianglesSurfaces

        /// <summary>
        /// Gets all the triangle surfaces in a CivilSurface
        /// </summary>
        /// <returns></returns>
        
Return Type: IList<Surface>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetTrianglesSurfaces

        /// <summary>
        /// Gets all the triangle surfaces in a CivilSurface via LandXML
        /// </summary>
        /// <param name="landXMLpath">The path to the LandXML that contains the surface export</param>
        /// <param name="onlyVisible">Processes only the visible faces</param>
        /// <returns></returns>
        
Return Type: IList<Surface>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFacesSurfaces

        /// <summary>
        /// Gets all the triangle faces in a CivilSurface via LandXML
        /// </summary>
        /// <param name="landXMLpath">The path to the LandXML that contains the surface export</param>
        /// <param name="onlyVisible">Processes only the visible faces</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### JoinSurfaces

        /// <summary>
        /// Joins the surfaces recursively into a Polysurface
        /// </summary>
        /// <param name="surfaces">The surfaces to join</param>
        /// <param name="limit">The amount of surfaces to join with recursion</param>
        /// <returns></returns>
        
Return Type: IList<Surface>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetIntersectionPoint

        /// <summary>
        /// Get intersection point between the line with start point and direction on the surface 
        /// </summary>
        /// <param name="point">The point to process</param>
        /// <param name="vector">The direction vector</param>
        /// <returns>
        /// The intersection point
        /// </returns>
        
Return Type: Point
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString


        /// <summary>
        /// Public textual representation in the Dynamo node preview.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## Corridor
    /// <summary>
    /// Corridor obejct type.
    /// </summary>
    
### Rebuild

        /// <summary>
        /// Rebuilds the Corridor in Civil 3D.
        /// </summary>
        /// <returns></returns>
        
Return Type: Corridor
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetPointsBySubassembly

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Returns the points that define the subassemblies in a corridor organized by:
        /// Corridor &gt; Baseline &gt; Region &gt; Assembly &gt; Subassembly
        /// </summary>
        /// <returns>
        /// The list of points that define each subassembly in the corridor
        /// </returns>
        /// <search> Subassembly, section</search>
        
Return Type: IList<IList<IList<IList<IList<IList<Point>>>>>>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetCorridorSurfaces

        /// <summary>
        /// Gets the corridor surfaces.
        /// </summary>
        /// <returns></returns>
        
Return Type: IList<IList<Surface>>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetShapesFromXML

        /// <summary>
        /// Returns a collection of AppliedSubassemblyShapes in the Corridor.
        /// </summary>
        /// <returns></returns>
        
Return Type: IList<IList<IList<AppliedSubassemblyShape>>>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetLinksFromXML

        /// <summary>
        /// Returns a collection of AppliedSubassemblyLinks in the Corridor.
        /// </summary>
        /// <returns></returns>
        
Return Type: IList<IList<IList<AppliedSubassemblyLink>>>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### PointByStationOffsetElevation
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
        
Return Type: Point
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CoordinateSystemByStation

        /// <summary>
        /// Returns a CoordinateSystem by station.
        /// </summary>
        /// <param name="baseline">The baseline.</param>
        /// <param name="station">The station.</param>
        /// <returns></returns>
        
Return Type: CoordinateSystem
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CoordinateSystemByPoint

        /// <summary>
        /// Returns a CoordinateSystem by point.
        /// </summary>
        /// <param name="baseline">The baseline.</param>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        
Return Type: CoordinateSystem
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetCodes

        /// <summary>
        /// Gets the PointCodes.
        /// </summary>
        /// <returns></returns>
        
Return Type: IList<string>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFeaturelines

        /// <summary>
        /// Gets the corridor Featurelies organized by Corridor-Baseline-Code-Region
        /// </summary>
        /// <returns></returns>
        
Return Type: IList<IList<IList<Featureline>>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetSubassemblyPoints

        /// <summary>
        /// Gets the subassembly points organized by: Corridor &gt; Baseline &gt; Region &gt; Assembly &gt; Subassembly.
        /// </summary>
        /// <param name="dumpXML">If true dumps a LandXML in the Temp folder.</param>
        /// <returns></returns>
        
Return Type: IList<IList<IList<IList<IList<Point>>>>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetPointsByCode

        /// <summary>
        ///  Gets the subassembly points organized by: Corridor &gt; Baseline &gt; Region &gt; Code.
        ///  It requires to export a LandXML to the %Temp% folder, named like the Civil 3D Document, containing only the corridor data.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        
Return Type: IList<IList<IList<IList<IList<Point>>>>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFeaturelineByPointCodeSide

        /// <summary>
        /// Gets the closest featureline by point code side.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="baselineIndex">Index of the baseline.</param>
        /// <param name="code">The code.</param>
        /// <param name="side">The side.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFeaturelinesByCode

        /// <summary>
        /// Gets the featurelines by Code &gt; Baseline &gt; Region.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        
Return Type: IList<IList<IList<Featureline>>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFeaturelinesByCodeStation

        /// <summary>
        /// Gets the featurelines by Code &gt; Baseline &gt; Region.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="station">The station.</param>
        /// <returns></returns>
        
Return Type: IList<IList<Featureline>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## Featureline
    /// <summary>
    /// Featureline obejct type.
    /// </summary>
    
### ByBaselineLandFeatureline

        #endregion

        #region PRIVATE METHODS


        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Creates a Featureline from a corridor Baseline and a LandFeatureline.
        /// The LandFeatureline name must follow Corridor Name | Corridor Region | Corridor Feature Code | Feature Side | Next Counter | Style Name.
        /// </summary>
        /// <param name="baseline">The baseline.</param>
        /// <param name="landFeatureline">Teh land feature line.</param>
        /// <param name="regionIndex">The region Index</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        /// 
        
Return Type: Featureline
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CoordinateSystemByStation

        // TODO: Define Coordinate System when the featurelines returns on itslef like a U
        // Minimum distance from baseline
        /// <summary>
        /// CoordinateSystem by station.
        /// </summary>
        /// <param name="station">The station.</param>
        /// <param name="vertical">if set to <c>true</c> the ZAxis is [vertical].</param>
        /// <returns></returns>
        
Return Type: CoordinateSystem
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### PointAtStation

        /// <summary>
        /// Point at station.
        /// </summary>
        /// <param name="station">The station.</param>
        /// <returns></returns>
        
Return Type: Point
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### PointByStationOffsetElevation

        /// <summary>
        /// Point the by station offset elevation.
        /// </summary>
        /// <param name="station">The station.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <param name="referToBaseline">if set to <c>true</c> [refer to baseline].</param>
        /// <returns></returns>
        
Return Type: Point
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetStationOffsetElevationByPoint

        /// <summary>
        /// Gets the station, offset and elevation for a point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetPolyCurveByOffsetElevation

        /// <summary>
        /// Gets a PolyCurve obtained by applying the offset and elevation displacement to each point of the Featureline PolyCurve.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <returns></returns>
        
Return Type: PolyCurve
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetPolyCurveByStationsOffsetElevation

        /// <summary>
        /// Gets a PolyCurve obtained by applying the offset and elevation displacement to each point of the Featureline PolyCurve only for the station interval specified.
        /// </summary>
        /// <param name="startStation">The start station.</param>
        /// <param name="endStation">The end station.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <returns></returns>
        
Return Type: PolyCurve
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### PointsByChord

        /// <summary>
        /// Points by chord distance on the Featureline.
        /// </summary>
        /// <param name="curve">The curve to subdivide.</param>
        /// <param name="chord">The chord.</param>
        /// <returns></returns>
        
Return Type: IList<Point>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## LandFeatureline
    /// <summary>
    /// LandFeatureline object type.
    /// </summary>
    
### GetDocumentLandFeaturelines
        #endregion

        #region PRIVATE METHODS


        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Gets all the LandFeaturelines from a CivilDocument.
        /// The Style will be empty by default.
        /// Not all the PolyCurves will be branching and it is to be expected to have null values.
        /// </summary>
        /// <param name="civilDocument">The CivilDocument</param>
        /// <returns></returns>
        
Return Type: IList<LandFeatureline>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByObjectPolyCurve

        /// <summary>
        /// Creates LandFeatureline
        /// </summary>
        /// <param name="fl">The featureline COM object</param>
        /// <param name="polyCurve">The polycurve</param>
        /// <returns></returns>
        /// 
        
Return Type: LandFeatureline
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## Mass
    /// <summary>
    /// Mass obejct type.
    /// </summary>
    /// <seealso cref="Revit.Elements.AbstractFamilyInstance" />
    
### InitMass

        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Initializes the mass.
        /// </summary>
        /// <param name="instance">The instance.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitMass

        /// <summary>
        /// Initializes the mass.
        /// </summary>
        /// <param name="fs">The fs.</param>
        /// <param name="pos">The position.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InternalSetFamilyInstance

        /// <summary>
        /// Internals the set family instance.
        /// </summary>
        /// <param name="fi">The family instance.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InternalSetPosition

        /// <summary>
        /// Method to set position.
        /// </summary>
        /// <param name="fi">The fi.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CloseFamily

        /// <summary>
        /// Closes the family.
        /// </summary>
        /// <param name="name">The name.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CreateModelCurves

        /// <summary>
        /// Utility function that creates model curves in the document.
        /// </summary>
        /// <param name="doc">The Document</param>
        /// <param name="cl">The CurveLoop.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CreateModelCurve

        /// <summary>
        /// Utility function that creates model curves in the document.
        /// </summary>
        /// <param name="doc">The Document</param>
        /// <param name="c">The curve.</param>
        
Return Type: Autodesk.Revit.DB.ModelCurve
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CloseDocument

        /// <summary>
        /// Closes the open family document and returns the family name with extension
        /// </summary>
        /// <param name="app">A reference to the application.</param>
        /// <param name="famName">The title of the family document to close including extension.</param>
        
Return Type: string
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFamilyDocument


        /// <summary>
        /// Returns a reference to a Family document with the specified template and family name.
        /// </summary>
        /// <param name="app">A reference to the application.</param>
        /// <param name="famName">The title of the family document to close including extension.</param>
        /// <param name="familyTemplate">The mass template path.</param>
        /// <param name="family">The reference to the Family object in the model.</param>
        /// <param name="famDoc">The output family document.</param>
        /// <param name="rvtFI">A placeholder for the family instance in the Revit model.</param>
        
Return Type: int
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CleanupFamilyDocument

        /// <summary>
        /// Removes all CurveElements and FreeFormElements in the family document.
        /// </summary>
        /// <param name="famDoc">The family document.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### TryJoinSolids

        /// <summary>
        /// Trying to join the solids in the list.
        /// </summary>
        /// <param name="famDoc">The family document.</param>
        /// <param name="solids">The list of Solid to try to join.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SaveFamily

        /// <summary>
        /// Saves the family
        /// </summary>
        /// <param name="famDoc">The family document.</param>
        /// <param name="famPath">The path where to save the family.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### UpdateFamilyInstance

        /// <summary>
        /// Updates the family and the family instance.
        /// </summary>
        /// <param name="famPath">The path to the family to reload.</param>
        /// <param name="rvtFI">The existing Revit Family Instance.</param>
        /// <param name="found">A boolean value that states if the family was existing or created anew.</param>
        
Return Type: Revit.Elements.FamilyInstance
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByCrossSections

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Creates a free form mass family by cross sections on the fly and inserts it in the project in Revit local coordinates.
        /// </summary>
        /// <param name="crossSections">The cross sections.</param>
        /// <param name="name">The name.</param>
        /// <param name="familyTemplate">The mass template path.</param>
        /// <param name="append">Append the geometry definition to the current geometry in the Family.</param>
        /// <param name="join">If true attempets to join the geometries.</param>
        /// <param name="rebar">Can host rebar.</param>
        /// <returns></returns>
        
Return Type: Revit.Elements.Element
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### BySolid

        /// <summary>
        /// Returns a FamilyInstance by a Dynamo solid in Revit local coordinates.
        /// </summary>
        /// <param name="solid">The Dynamo solid in Revit local coordinates.</param>
        /// <param name="name">The name of the family type.</param>
        /// <param name="familyTemplate">the path to the RFT file to use as a template.</param>
        /// <param name="material">The material to assign to the Revit family type.</param>
        /// <param name="isVoid">If true it will create a void that can be used to cut other Revit elements.</param>
        /// <param name="rebar">Can host rebar.</param>
        /// <param name="mesh">If true it tries to convert the solid to a Revit mesh</param>
        /// <returns></returns>
        
Return Type: Revit.Elements.FamilyInstance
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPathCrossSections

        /// <summary>
        /// Creates a free form mass family by cross sections and path on the fly and inserts it in the project in Revit local coordinates.
        /// </summary>
        /// <param name="pathPoints"></param>
        /// <param name="crossSections"></param>
        /// <param name="name"></param>
        /// <param name="familyTemplate"></param>
        /// <param name="append"></param>
        /// <param name="createForm"></param>
        /// <param name="rebar">Can host Rebar.</param>
        /// <returns></returns>
        
Return Type: Revit.Elements.FamilyInstance
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByLoftCrossSections


        /// <summary>
        /// Creates a free form mass family by cross sections on the fly and inserts it in the project in Revit local coordinates.
        /// </summary>
        /// <param name="crossSections">The cross sections.</param>
        /// <param name="name">The name.</param>
        /// <param name="familyTemplate">The mass template path.</param>
        /// <param name="append">Append the geoemtry definition to the current geometry in the Family.</param>
        /// <param name="rebar">Can host rebar.</param>
        /// <returns></returns>
        
Return Type: Revit.Elements.Element
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByShapesCreaseStations

        // TODO: Check if the family template supports FreeFormElements

        /// <summary>
        /// Creates a free form mass family by cross sections on the fly and inserts it in the project in Revit local coordinates.
        /// </summary>
        /// <param name="shapes">The AppliedSubassemblyShape that represents the cross sections.</param>
        /// <param name="stations">The sequence of stations that defines the creases along the alignment. If null, the loft will be continuous.</param>
        /// <param name="name">The name.</param>
        /// <param name="familyTemplate">The mass template path.</param>
        /// <param name="append">Append the geoemtry definition to the current geometry in the Family.</param>
        /// <param name="rebar">Can host rebar.</param>
        /// <returns></returns>
        
Return Type: Revit.Elements.Element
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByClosedCurvesCreaseStations

        /// <summary>
        /// Creates a free form mass family by cross sections on the fly and inserts it in the project in Revit local coordinates.
        /// </summary>
        /// <param name="closedCurves">The closed curves that represents the cross sections.</param>
        /// <param name="stations">The sequence of stations that defines the creases along the alignment. If null, the loft will be continuous.</param>
        /// <param name="name">The name.</param>
        /// <param name="familyTemplate">The mass template path.</param>
        /// <param name="alignment">The alignment used to calculate the stations.</param>
        /// <param name="append">Append the geoemtry definition to the current geometry in the Family.</param>
        /// <param name="rebar">Can host rebar.</param>
        /// <returns></returns>
        
Return Type: Revit.Elements.Element
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## MultiPoint
    /// <summary>
    /// Shape Point Class
    /// </summary>
    //[IsVisibleInDynamoLibrary(false)]
    
### ToHorizontalFloor

Return Type: Element
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByShapePointArray

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Returns a MultiPoint by a collection of shape points.
        /// </summary>
        /// <param name="shapePointArray">The shape points.</param>
        /// <returns></returns>
        
Return Type: MultiPoint
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByElement

        /// <summary>
        /// Returns a MultiPoint by a collection of shape points.
        /// </summary>
        /// <param name="element">The Revit element.</param>
        /// <returns></returns>
        
Return Type: MultiPoint
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToFloor

        /// <summary>
        /// Converts the MultiPoint into a floor of the specified type.
        /// </summary>
        /// <param name="floorType">Type of the floor.</param>
        /// <param name="level">The level.</param>
        /// <param name="structural">if set to <c>true</c> [structural].</param>
        /// <returns></returns>
        
Return Type: Element
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToAdaptiveComponent

        /// <summary>
        /// Converts the MultiPoint into an adaptive component of the specified type.
        /// </summary>
        /// <param name="familyType">Type of the family.</param>
        /// <returns></returns>
        
Return Type: AdaptiveComponent
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SerializeXML

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SerializeJSON

        /// <summary>
        /// Serializes to json.
        /// </summary>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## OpeningUtils
    /// <summary>
    /// OpeningUtils.
    /// </summary>
    
### GetRectangularOpenings

        #endregion

        #region PRIVATE METHODS


        #endregion

        #region PUBLIC METHODS



        /// <summary>
        /// Gets the rectangular openings.
        /// </summary>
        /// <returns></returns>
        /// <remarks>This method uses walls, doors, windows and generic models bounding boxes to determine the rectangles.
        /// These objects can be in the host file or in linked Revit files.</remarks>
        
Return Type: IList<Autodesk.DesignScript.Geometry.Rectangle>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## Profile
    /// <summary>
    /// Profile obejct type.
    /// </summary>
    
### XX

Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetElevationAtStation

        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Gets the elevation at station.
        /// </summary>
        /// <param name="station">The station.</param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetEntitiesElevations

        /// <summary>
        /// Gets the elevations of the entities in the profile.
        /// </summary>
        /// <returns></returns>
        /// 
        
Return Type: IList<double>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetEntitiesStations

        /// <summary>
        /// Gets the stations of the entities in the profile.
        /// </summary>
        /// <returns></returns>
        /// 
        
Return Type: IList<double>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString



        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## ProfilePVI
    /// <summary>
    /// ProfilePVI object type.
    /// </summary>
    
### ToString

        #endregion

        #region PRIVATE METHODS


        #endregion

        #region PUBLIC METHODS


        //TODO: Get profile curves        

        /// <summary>
        /// Returns a text representation of the object.
        /// </summary>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## ProfileView
    /// <summary>
    /// ProfileView object type.
    /// </summary>
    
### ToString

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
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## ProjectPositionUtils
    /// <summary>
    /// Static Obejct that returns the ProjectPosition of the Revit Document
    /// </summary>
    
### ToString

        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// String representation
        /// </summary>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SetProjectPosition

        /// <summary>
        /// Set the new ProjectLocation of the Revit document origin.
        /// </summary>
        /// <param name="location">The ProjectLocation</param>
        /// <param name="newPosition">The new ProjectPosition of the document origin.</param>
        
Return Type: void
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## RevitFailuresPreprocessor
    /// <summary>
    /// Revit Failure Preprocessor.
    /// </summary>
    /// <seealso cref="Autodesk.Revit.DB.IFailuresPreprocessor" />
    
### PreprocessFailures

        /// <summary>
        /// Preprocesses the failures.
        /// </summary>
        /// <param name="fa">The failure accessor.</param>
        /// <returns></returns>
        
Return Type: FailureProcessingResult
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## RevitFamilyLoadOptions
    /// <summary>
    /// FamilyLoadOptions
    /// </summary>
    /// <seealso cref="Autodesk.Revit.DB.IFamilyLoadOptions" />
    
### OnFamilyFound

        /// <summary>
        /// Called when [family found].
        /// </summary>
        /// <param name="familyInUse">if set to <c>true</c> [family in use].</param>
        /// <param name="overwriteParameters">if set to <c>true</c> [overwrite parameters].</param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### OnSharedFamilyFound

        /// <summary>
        /// Called when [shared family found].
        /// </summary>
        /// <param name="sharedFamily">The shared family.</param>
        /// <param name="familyInUse">if set to <c>true</c> [family in use].</param>
        /// <param name="source">The source.</param>
        /// <param name="overwriteParameters">if set to <c>true</c> [overwrite parameters].</param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## RevitUtils
    /// <summary>
    /// Collection of utilities for the integration with Revit.
    /// </summary>
    
### GetCropBoxFor

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Return element id of crop box for a given view.
        /// The built-in parameter ID_PARAM of the crop box
        /// contains the element id of the view it is used in;
        /// e.g., the crop box 'points' to the view using it
        /// via ID_PARAM. Therefore, we can use a parameter
        /// filter to retrieve all crop boxes with the
        /// view's element id in that parameter.
        /// 
        /// source:
        /// http://thebuildingcoder.typepad.com/blog/2018/02/efficiently-retrieve-crop-box-for-given-view.html
        /// </summary>
        
Return Type: Autodesk.Revit.DB.ElementId
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SetZAxisByVector

        /// <summary>
        /// Rotate the FamilyInstance around the insertion point to match the local Z-Axis with the provided vector.
        /// </summary>
        /// <param name="familyInstance">The FamilyInstance to rotate.</param>
        /// <param name="vector">The Vector used to align hte FmailyInstance local Z-Axis.</param>
        /// <returns></returns>
        /// 
        
Return Type: FamilyInstance
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SectionViewByStation

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Sections the view by station.
        /// </summary>
        /// <param name="baseline">The baseline.</param>
        /// <param name="station">The station.</param>
        /// <param name="lengthLeft">The length left.</param>
        /// <param name="lengthRight">The length right.</param>
        /// <param name="elevationMin">The elevation minimum.</param>
        /// <param name="elevationMax">The elevation maximum.</param>
        /// <param name="depth">The depth.</param>
        /// <returns></returns>
        
Return Type: SectionView
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### UpdateSectionViewByCoordinateSystem

        /// <summary>
        /// Updates the section view by Coordinate System, Min and Max points. The view depth is controlled by the the difference of the Z coordinates of min and max points.
        /// </summary>
        /// <param name="section">The section view.</param>
        /// <param name="cs">The coordinate system with vertical Z axis.</param>
        /// <param name="minPoint">The 2D point in the view coordinates that represents the bottom left corner in the view.</param>
        /// <param name="maxPoint">The 2D point in hte view coordinates that represents the top right corner in the view.</param>
        /// <returns></returns>
        
Return Type: SectionView
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### UpdatePlanViewByCoordinateSystem

        /// <summary>
        /// Updates the section view by Coordinate System, Min and Max points. The view depth is controlled by the the difference of the Z coordinates of min and max points.
        /// </summary>
        /// <param name="plan">The plan view.</param>
        /// <param name="cs">The coordinate system with vertical Z axis.</param>
        /// <param name="minPoint">The 2D point in the view coordinates that represents the bottom left corner in the view.</param>
        /// <param name="maxPoint">The 2D point in tHe view coordinates that represents the top right corner in the view.</param>
        /// <returns></returns>
        
Return Type: FloorPlanView
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AlignmentSampleLinesParameters

        /// <summary>
        /// Returns the sample lines parameters associated to an alignment
        /// </summary>
        /// <param name="alignment">The alignment</param>
        /// <returns></returns>
        
Return Type: IList<Dictionary<string, object>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetSampleLinesSections

        /// <summary>
        /// Returns the sections lines associated to the sample line.
        /// </summary>
        /// <param name="line">The sample line.</param>
        /// <returns></returns>
        
Return Type: IList<IList<Line>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AlignmentSectionsLines

        /// <summary>
        /// Returns the section lines associated to the sample lines in an alignment
        /// </summary>
        /// <param name="alignment">The alignment</param>
        /// <returns></returns>
        
Return Type: IList<Dictionary<double, IList<IList<Line>>>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SampleLinesParameters


        /// <summary>
        /// Returns the Sample Lines parameters associated with the alignment.
        /// </summary>
        /// <param name="baseline">The baseline.</param>
        /// <returns></returns>
        
Return Type: IList<Dictionary<string, object>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### DetailGroupBySectionView

        /// <summary>
        /// Detail group by section view.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns></returns>
        
Return Type: SectionView
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### DocumentTotalTransform

        /// <summary>
        /// Gets the CoordinateSystem of the Revit document total transform.
        /// </summary>
        /// <returns></returns>
        
Return Type: CoordinateSystem
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### DocumentTotalTransformInverse

        /// <summary>
        /// Gets the Inverse CoordinateSystem of the Revit document total transform.
        /// </summary>
        /// <returns></returns>
        
Return Type: CoordinateSystem
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ExtractParamatersByCategory


        /// <summary>
        /// Extracts the location paramaters by category.
        /// </summary>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        
Return Type: object[][]
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ObjectLocationParameters

        /// <summary>
        /// Returns the object location parameters.
        /// </summary>
        /// <param name="update">if set to <c>true</c> [update].</param>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        
Return Type: object[]
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ExportXML

        /// <summary>
        /// Captures the Revit Elements with linear coordinate system parameters.
        /// </summary>
        /// <param name="run">if set to <c>true</c> [run].</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### UpdateDocument

        /// <summary>
        /// Updates the location and the linear coordinate system parameters of all the Revit Elements captured in the project XML.
        /// </summary>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="elements">The elements.</param>
        /// <returns></returns>
        
Return Type: IList<Revit.Elements.Element>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### UpdateObjects

        /// <summary>
        /// Updates the location and the linear coordinate system parameters of a collection of Revit Elements.
        /// </summary>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="elements">A collection of elements.</param>
        /// <param name="normalized">If true it will keep the same proportion along the featureline rather than the exact station.</param>
        /// <remarks>At the end of the update the Station values may be different.</remarks>
        /// <returns></returns>
        
Return Type: IList<Revit.Elements.Element>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AssignFeatureline

        /// <summary>
        /// Uses a featureline to assign or recalculate the linear coordinate system parameters of a Revit Element.
        /// In case of Adaptive Components it calculates the parameters for the first adaptive point only.
        /// In case of Floors it calculates the parameters for the first point of the top face only.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="featureline">The featureline.</param>
        /// <returns></returns>
        
Return Type: object
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFeaturelineByElementCodeSide

        /// <summary>
        /// Given a Revit Element it returns the first Featureline that meets the arguments.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="corridor">The corridor.</param>
        /// <param name="baselineIndex">Index of the baseline.</param>
        /// <param name="code">The code.</param>
        /// <param name="side">The side.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ProcessPointBasedFamilyInstancesByData

        /// <summary>
        /// Processes the point based family instances by data.
        /// </summary>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### FamilyInstanceLocationParametersForUpdate

        /// <summary>
        /// Returns the FamilieInstance location parameters for update.
        /// </summary>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### FamilyInstanceLocationParametersForCreate

        /// <summary>
        /// Returns the FamilieInstance location parameters for creation.
        /// </summary>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CreateFamilyInstance

        /// <summary>
        /// Creates FamilyInstances using the featurelines to define linear coordinate systems and assign the parameters for the update.
        /// </summary>
        /// <param name="run">if set to <c>true</c> [run].</param>
        /// <param name="familyType">Type of the family.</param>
        /// <param name="featureline">The featureline.</param>
        /// <param name="useBaseline">if set to <c>true</c> [use baseline].</param>
        /// <param name="station">The station.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <param name="angleZ">The angle z.</param>
        /// <returns></returns>
        
Return Type: Revit.Elements.FamilyInstance
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### RevitLinkByStationOffsetElevation

        /// <summary>
        /// Insert the Revit Link Instances of a give Revit Link Type in the host file.
        /// </summary>
        /// <param name="revitLinkType">Type of the revit link.</param>
        /// <param name="featureline">The featureline.</param>
        /// <param name="station">The station.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <param name="rotate">if set to <c>true</c> [rotate].</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### NamedSiteByStationOffsetElevation

        /// <summary>
        /// Defines the Named site in the Revit Link.
        /// </summary>
        /// <param name="featureline">The featureline.</param>
        /// <param name="station">The station.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <param name="rotate">if set to <c>true</c> [rotate].</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### RevitLinkParameters

        /// <summary>
        /// Exports the location parameters of Revit Link Instances.
        /// </summary>
        /// <returns></returns>
        
Return Type: IList<IList<object>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ExportIFC

        /// <summary>
        /// Exports the IFC file of the DWG in the folder of the Revit document with in local coordinates.
        /// </summary>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="desktopConnectorFolder"> The Autodesk Desktop Connector folder for the project on the cloud environment (BIM 360, BIM 360 Team, Fusion 360).</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ReplaceIFCLink

        /// <summary>
        /// Replaces the IFC Link with the intermediate RVT document
        /// </summary>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### WallBySurface

        /// <summary>
        /// Creates a wall from a Dynamo surface.
        /// The wall is recreated but not updated. The input surface must be planar and its normal must be orthogonal to the world Z Axis.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="wallType">Type of the wall.</param>
        /// <param name="structural">if set to <c>true</c> [structural].</param>
        /// <returns></returns>
        
Return Type: Wall
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GeneratePropSetDefXML

        /// <summary>
        /// Generate PropertySetDefinition XML from list of parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="propertySetName"></param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GenerateObjectXML

Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## SessionVariables
    /// <summary>
    /// Session Variables utilities.
    /// </summary>
    
## ShapePoint
    /// <summary>
    /// Shape Point Class
    /// </summary>
    //[IsVisibleInDynamoLibrary(false)]

    
### ByPointFeatureline

        #endregion

        #region PRIVATE METHODS


        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Bies the point featureline.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="featureline">The featureline.</param>
        /// <returns></returns>
        
Return Type: ShapePoint
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByFeaturelineStation

        /// <summary>
        /// Creates the shapepoint
        /// </summary>
        /// <param name="featureline"></param>
        /// <param name="station"></param>
        /// <returns>
        /// The ShapePoint
        /// </returns>
        
Return Type: ShapePoint
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Copy

        /// <summary>
        /// Copies this instance.
        /// </summary>
        /// <returns></returns>
        
Return Type: ShapePoint
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SetId

        /// <summary>
        /// Sets the identifier.
        /// </summary>
        /// <param name="newId">The new identifier.</param>
        /// <returns></returns>
        
Return Type: ShapePoint
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SetPoint

        /// <summary>
        /// Sets the point.
        /// </summary>
        /// <returns></returns>
        
Return Type: ShapePoint
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### UpdateByFeatureline
        
        /// <summary>
        /// Calculates the new ShapePoint location on the new Featureline using the Station, Offset and Elevation parameters.
        /// </summary>
        /// <param name="featureline">The featureline used to update the ShapePoint</param>
        /// <returns></returns>
        
Return Type: ShapePoint
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AssignFeatureline


        /// <summary>
        /// Updates the ShapePoint parameters Station, Offset and Elevation against the new Featureline.
        /// </summary>
        /// <param name="featureline">The featureline assigned to the ShapePoint</param>
        /// <returns></returns>
        
Return Type: ShapePoint
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## ShapePointArray
    /// <summary>
    /// Collection of ShapePoints
    /// </summary>
    
### Copy

Return Type: ShapePointArray
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByShapePointList
        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Returns a ShapePointArray object
        /// </summary>
        /// <param name="shapePointList">The list of ShapePoints.</param>
        /// <returns></returns>
        
Return Type: ShapePointArray
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Add

        //[IsVisibleInDynamoLibrary(false)]
        /// <summary>
        /// Adds the specified shape point.
        /// </summary>
        /// <param name="shapePoint">The shape point.</param>
        /// <returns></returns>
        
Return Type: ShapePointArray
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Renumber

        //[IsVisibleInDynamoLibrary(false)]
        /// <summary>
        /// Renumbers the ShapePoints in the instance.
        /// </summary>
        
Return Type: ShapePointArray
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### RemoveAtIndex

        //[IsVisibleInDynamoLibrary(false)]
        /// <summary>
        /// Removes the ShapePoint at index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        
Return Type: ShapePointArray
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SortByStation

        //[IsVisibleInDynamoLibrary(false)]
        /// <summary>
        /// Sorts the by station.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        
Return Type: ShapePointArray
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SplitByFeatureline

        //[IsVisibleInDynamoLibrary(false)]
        /// <summary>
        /// Splits ShpaePoints into subset of ShapePoints by featureline.
        /// </summary>
        /// <returns></returns>
        
Return Type: IList<ShapePointArray>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Reverse

        //[IsVisibleInDynamoLibrary(false)]
        /// <summary>
        /// Reverses this instance.
        /// </summary>
        /// <returns></returns>
        
Return Type: ShapePointArray
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Join

        /// <summary>
        /// Joins two ShapePoints objects into a new one.
        /// </summary>
        /// <param name="other">The other ShapePointArray.</param>
        /// <returns></returns>
        //[IsVisibleInDynamoLibrary(false)]
        
Return Type: ShapePointArray
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## SlopedFloor
    /// <summary>
    /// AbstratMEPCurve object Type. Base class for Revti MEP Curve objects.
    /// </summary>
    /// <seealso cref="Revit.Elements.Element" />
    
### InternalSetFloor

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Set the InternalFloor property and the associated element id and unique id
        /// </summary>
        /// <param name="floor"></param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitFloor

        /// <summary>
        /// Initialize a floor element
        /// </summary>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitFloor

        /// <summary>
        /// Initialize a floor element
        /// </summary>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByOutlineTypeAndLevel

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Create a Revit Floor given it's curve outline and Level
        /// </summary>
        /// <param name="outline">The outline.</param>
        /// <param name="floorType">Type of the floor.</param>
        /// <param name="level">The level.</param>
        /// <param name="structural">if set to <c>true</c> [structural].</param>
        /// <returns>
        /// The floor
        /// </returns>
        
Return Type: SlopedFloor
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## Subassembly
    /// <summary>
    /// Subassembly object type.
    /// </summary>
    
### SetParameterByName

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
        
Return Type: Subassembly
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## SubassemblyParameter
    /// <summary>
    /// SubassemblyParameter obejct type.
    /// </summary>
    
### ToString


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
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## Utils
    /// <summary>
    /// Collection of utilities.
    /// </summary>
    
### AlmostEqual
        #region PRIVATE PROPERTIES


        #endregion

        #region PUBLIC PROPERTIES


        #endregion

        #region CONSTRUCTOR


        #endregion

        #region PRIVATE METHODS


        #endregion

        #region PUBLIC METHODS


        /// <summary>
        /// Checks if two values are almost equal
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### FeetToMm


        /// <summary>
        /// Feets to mm.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### MmToFeet


        /// <summary>
        /// Mms to feet.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### FeetToM


        /// <summary>
        /// Feets to m.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### MToFeet


        /// <summary>
        /// ms to feet.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### DegToRad


        /// <summary>
        /// Degs to RAD.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### RadToDeg


        /// <summary>
        /// RADs to deg.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddLayer


        /// <summary>
        /// Adds the layer.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="layerName">Name of the layer.</param>
        
Return Type: void
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddLayers

Return Type: void
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### FreezeLayers


        /// <summary>
        /// Freezes the layers.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="layer">the name of the layer.</param>
        
Return Type: void
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddText


        /// <summary>
        /// Adds the text.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="text">The text.</param>
        /// <param name="point">The point.</param>
        /// <param name="height">The height.</param>
        /// <param name="layer">The layer.</param>
        /// <param name="cs">The cs.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddArcByArc


        /// <summary>
        /// Adds the arc by arc.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="arc">The arc.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddPointByPoint

        /// <summary>
        /// Adds the point to the document.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="point">The point.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddCivilPointByPoint


        /// <summary>
        /// Adds the land point by point.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddDBPointByPoint

Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddDBPointsByPoints

Return Type: List<string>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddPointGroupByPoint


        /// <summary>
        /// Adds the point group by point.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="points">The points.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetPointGroups


        /// <summary>
        /// Returns the point groups in Civil 3D as Dynamo point lists.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, IList<Point>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddTINSurfaceByPoints

        /// <summary>
        /// Adds a TIN surface by points.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="points">The points.</param>
        /// <param name="name">The name.</param>
        /// <param name="layer">The name of the layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddPolylineByPoints


        /// <summary>
        /// Adds a polyline by points.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="points">The points.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddCircleByCircle


        /// <summary>
        /// Adds a circle entity in Civil 3D by circle.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="c">The c.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddLWPolylineByPoints


        /// <summary>
        /// Adds a light weigth polyline by points.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="points">The points.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddLWPolylineByPolyCurve


        /// <summary>
        /// Adds a light weight polyline by poly curve.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="polycurve">The polycurve.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Rotate3DByCurveNormal


        /// <summary>
        /// Rotates in 3D by curve normal.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="handle">The handle.</param>
        /// <param name="dynCurve">The dyn curve.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### RotateByVector


        /// <summary>
        /// Rotates the by vector.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="handle">The handle.</param>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Rotate3DByPlane


        /// <summary>
        /// Rotate3s the d by plane.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="handle">The handle.</param>
        /// <param name="plane">The plane.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddPolylineByCurve


        /// <summary>
        /// Adds the polyline by curve.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="curve">The curve.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddPolylineByCurves


        /// <summary>
        /// Adds the polyline by curves.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="curves">The curves.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddExtrudedSolidByPoints


        /// <summary>
        /// Adds the extruded solid by points.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="points">The points.</param>
        /// <param name="height">The height.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddRegionByPatch


        /// <summary>
        /// Adds the region by patch.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="curve">The curve.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddExtrudedSolidByPatch


        /// <summary>
        /// Adds the extruded solid by patch.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="curve">The curve.</param>
        /// <param name="height">The height.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### AddExtrudedSolidByCurves


        /// <summary>
        /// Adds the extruded solid by curves.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="curves">The curves.</param>
        /// <param name="height">The height.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CutSolidsByPatch


        /// <summary>
        /// Cuts the solids by patch.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="closedCurve">The closed curve.</param>
        /// <param name="height">The height.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CutSolidsByCurves

        /// <summary>
        /// Cuts the solids by curves.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="closedCurves">The closed curves.</param>
        /// <param name="height">The height.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CutSolidsByGeometry


        /// <summary>
        /// Cuts the solids by geometry.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="geometry">The geometry.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SliceSolidsByPlane


        /// <summary>
        /// Slices the solids by plane.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="plane">The plane..</param>     
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ImportGeometry


        /// <summary>
        /// Imports the geometry.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="geometry">The geometry.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: IList<string>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ImportGeometry

Return Type: IList<string>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ImportGeometryByPath

        /// <summary>
        /// Imports the geometry of a SAT file
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="path"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        
Return Type: IList<string>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ImportElement


        //TODO: this node is not working when the geometry extraction from one of the solids returns a null or raise an exception

        /// <summary>
        /// Imports the element.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="element">The element.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="layer">The layer.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### DumpLandXML


        /// <summary>
        /// Dumps the land XML.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetXmlDocument


        /// <summary>
        /// Gets the XML document.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Error in Loading XML</exception>
        
Return Type: XmlDocument
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetXmlNamespaceManager


        /// <summary>
        /// Gets the XML namespace manager.
        /// </summary>
        /// <param name="xmlDoc">The XML document.</param>
        /// <returns></returns>
        
Return Type: XmlNamespaceManager
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Log


        /// <summary>
        /// Function that writes an entry to the log file
        /// </summary>
        /// <param name="message"></param>
        
Return Type: void
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Log

        /// <summary>
        /// Function that writes an exception entry to the log file
        /// </summary>
        /// <param name="ex">The exception</param>
        
Return Type: void
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitializeLog

        /// <summary>
        /// Finalizes the Log file.
        /// </summary>
        
Return Type: void
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetCorridorSubAssembliesFromLandXML

        /// <summary>
        /// Gets the corridor subassemblies shapes from LandXML.
        /// </summary>
        /// <param name="corridor">The corridor.</param>
        /// <param name="dumpXML">If True exports a LandXML in the Temp folder.</param>
        /// <returns></returns>
        
Return Type: IList<IList<IList<IList<IList<Point>>>>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetCorridorPointsByCodeFromLandXML

        /// <summary>
        /// Gets the corridor points by code from land XML.
        /// </summary>
        /// <param name="corridor">The corridor.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        
Return Type: IList<IList<IList<IList<IList<Point>>>>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFeatureLinesByCodeFromLandXML

        /// <summary>
        /// Gets the feature lines by code from land XML.
        /// </summary>
        /// <param name="corridor">The corridor.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        
Return Type: IList<IList<IList<Featureline>>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFeaturelinesFromLandXML

        /// <summary>
        /// Gets the featurelines from land XML.
        /// </summary>
        /// <param name="corridor">The corridor.</param>
        /// <returns></returns>
        
Return Type: IList<IList<IList<Featureline>>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFeaturelines

        /// <summary>
        /// Gets the featurelies from the corridor organized by Corridor-Baseline-Code-Region
        /// </summary>
        /// <param name="corridor">The corridor.</param>
        /// <returns></returns>
        
Return Type: IList<IList<IList<Featureline>>>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetSurfaceTriangles

        /// <summary>
        /// Gets the triangles from a CivilSurface
        /// </summary>
        /// <param name="surface">The CivilSurface.</param>
        /// <returns></returns>
        
Return Type: IList<Surface>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetSurfaceTrianglesByLandXML


        /// <summary>
        /// Gets the triangles from a CivilSurface
        /// </summary>
        /// <param name="surface">The CivilSurface.</param>
        /// <param name="path">The path to the LandXML that contains the surface export.</param>
        /// <param name="onlyVisible">If true processes the visible triangles in the XML.</param>
        /// <returns></returns>
        
Return Type: IList<Surface>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetFacesLandXML


        /// <summary>
        /// Gets the triangles from a CivilSurface
        /// </summary>
        /// <param name="surface">The CivilSurface.</param>
        /// <param name="path">The path to the LandXML that contains the surface export.</param>
        /// <param name="onlyVisible">If true processes the visible triangles in the XML.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### JoinSurfaces

        /// <summary>
        /// Recursive function to join surfaces into a PolySurface
        /// </summary>
        /// <param name="surfaces">The surface list to process</param>
        /// <param name="limit">The amount of surfaces to join together</param>
        /// <returns></returns>
        
Return Type: IList<Surface>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CreatePropertySetDefinition

Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CreatePropertySets

Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ConvertToSnakeCase

Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### IsFilePath

Return Type: bool
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### EnsureCivilPythonInstalled

        /// <summary>
        /// Checks whether the required CivilPython library is installed and available for use.
        /// </summary>
        /// <remarks>If the library is not found, a warning message is displayed to the user and a log
        /// entry is created. This method should be called before attempting to use CivilPython functionality to ensure
        /// the required dependency is present.</remarks>
        /// <returns>true if the CivilPython library is installed; otherwise, false.</returns>
        
Return Type: bool
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetCivilPythonVersion

Return Type: string
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetPluginRootFolder

Return Type: string
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## TinCreationData

## UtilsObjectsLocation
    /// <summary>
    /// Collection of utilities for obejct location.
    /// </summary>
    
### FeetToMm
        /// <summary>
        /// Feets to mm.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### FeetToM

        /// <summary>
        /// Feets to m.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### MToFeet

        /// <summary>
        /// ms to feet.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### MmToFeet

        /// <summary>
        /// Mms to feet.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### DegToRadians

        /// <summary>
        /// Degs to radians.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### RadiansToDeg

        /// <summary>
        /// Radianses to deg.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        
Return Type: double
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### XYZMToFeet

        /// <summary>
        /// Xyzms to feet.
        /// </summary>
        /// <param name="xyz">The xyz.</param>
        /// <returns></returns>
        
Return Type: XYZ
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### XYZFeetToM

        /// <summary>
        /// Xyzs the feet to m.
        /// </summary>
        /// <param name="xyz">The xyz.</param>
        /// <returns></returns>
        
Return Type: XYZ
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### LocationXML

        /// <summary>
        /// Locations the XML.
        /// </summary>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### UdpateDocumentFromXML

        /// <summary>
        /// Udpates the document from XML.
        /// </summary>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="elements">The elements.</param>
        /// <returns></returns>
        
Return Type: IList<Revit.Elements.Element>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### OptimizedUdpateObjectFromXML

        /// <summary>
        /// Udpates the object from XML.
        /// </summary>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="elements">The element.</param>
        /// <param name="normalized">Boolean value to proces based on the normalized values.</param>
        /// <returns></returns>
        /// <remarks>If the code is "None" the Baseline will be used to calculate the location.</remarks>
        
Return Type: IList<Revit.Elements.Element>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### UpdateShapePoint

        /// <summary>
        /// Updates hte location of a ShapePoint object
        /// </summary>
        /// <param name="civilDocument">The CivilDocument in Civil 3D</param>
        /// <param name="sp">The ShapePoint</param>
        /// <returns></returns>
        
Return Type: ShapePoint
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GroupElements

        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="elements"></param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetElementFeatureline

Return Type: Dictionary<string, object>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### TestUdpateObject

        /// <summary>
        /// Tests the udpate object.
        /// </summary>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// It was not possible to find the specified corridor
        /// or
        /// It was not possible to find the specified featureline
        /// or
        /// It was not possible to find corridor
        /// or
        /// It was not possible to find featureline
        /// </exception>
        
Return Type: Revit.Elements.Element
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetConnectorManager

        /// <summary>
        /// Return the given element's connector manager,
        /// using either the family instance MEPModel or
        /// directly from the MEPCurve connector manager
        /// for ducts and pipes.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        
Return Type: ConnectorManager
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### UpdateMEPCurve

        /// <summary>
        /// Updates the mep curve.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="newCurve">The new curve.</param>
        
Return Type: void
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ReadFamilyInstancesPointBased

        // TODO: test on a large Revit model how long it takes to read the data
        /// <summary>
        /// Reads the family instances point based.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        
Return Type: object[][]
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ObjectLocationParameters

        /// <summary>
        /// Object location parameters.
        /// </summary>
        /// <param name="familyInstance">The family instance.</param>
        /// <returns></returns>
        
Return Type: object[]
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### LinearObjectLocationParameters

        /// <summary>
        /// Linear objects location parameters.
        /// </summary>
        /// <param name="linearMEPCurve">The linear mep curve.</param>
        /// <returns></returns>
        
Return Type: object[]
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetStationOffsetElevation

        /// <summary>
        /// Gets the station offset elevation.
        /// </summary>
        /// <param name="corridor">The corridor.</param>
        /// <param name="baselineIndex">Index of the baseline.</param>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        
Return Type: double[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### RawCreateProjectParameter



        /// <summary>
        /// Returns a raw of parameters to be used for creation for a given parameter Type
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="type">The ForgeTypeId of the parameter</param>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        /// <param name="userModifiable">if set to <c>true</c> [user modifiable].</param>
        /// <param name="cats">The cats.</param>
        /// <param name="group">The group.</param>
        /// <param name="inst">if set to <c>true</c> [inst].</param>
        /// <returns></returns>
        
Return Type: ExternalDefinition
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CheckParameters

        /// <summary>
        /// Checks the parameters.
        /// </summary>
        /// <param name="doc">The document.</param>
        
Return Type: void
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### FamilyInstancesPointBased

        /// <summary>
        /// Families the instances point based.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ReadFamilyInstanceLocationParametersForUpdate

        /// <summary>
        /// Reads the family instance location parameters for update.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ReadFamilyInstanceLocationParametersForCreate

        /// <summary>
        /// Reads the family instance location parameters for create.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### UpdateLinearObjectLocation

        /// <summary>
        /// Updates the linear object location.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="civilDocument">The civil document.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### UpdateFamilyInstance

        /// <summary>
        /// Updates the family instance.
        /// </summary>
        /// <param name="familyInstance">The family instance.</param>
        /// <param name="familyType">Type of the family.</param>
        /// <param name="featureline">The featureline.</param>
        /// <param name="useBaseline">if set to <c>true</c> [use baseline].</param>
        /// <param name="station">The station.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <param name="angleZ">The angle z.</param>
        /// <returns></returns>
        
Return Type: Revit.Elements.FamilyInstance
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CreateFamilyInstance



        /// <summary>
        /// Creates the family instance.
        /// </summary>
        /// <param name="familyType">Type of the family.</param>
        /// <param name="featureline">The featureline.</param>
        /// <param name="useBaseline">if set to <c>true</c> [use baseline].</param>
        /// <param name="station">The station.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <param name="angleZ">The angle z.</param>
        /// <returns></returns>
        
Return Type: Revit.Elements.FamilyInstance
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### RevitLinkByStationOffsetElevation

        /// <summary>
        /// Revit link by station offset elevation.
        /// </summary>
        /// <param name="revitLinkType">Type of the revit link.</param>
        /// <param name="featureline">The featureline.</param>
        /// <param name="station">The station.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <param name="rotate">if set to <c>true</c> [rotate].</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### NamedSiteByStationOffsetElevation

        /// <summary>
        /// Create a Named site by station offset elevation.
        /// </summary>
        /// <param name="featureline">The featureline.</param>
        /// <param name="station">The station.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="elevation">The elevation.</param>
        /// <param name="rotate">if set to <c>true</c> [rotate].</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns></returns>
        
Return Type: string
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ClosestFeaturelineByPoint

        /// <summary>
        /// Given a Point it returns the first the Featureline that meets the argument values.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="corridor">The corridor.</param>
        /// <param name="baselineIndex">Index of the baseline.</param>
        /// <param name="code">The code.</param>
        /// <param name="side">The side.</param>
        /// <returns></returns>
        
Return Type: Featureline
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### FeaturelineByParameter

        /// <summary>
        /// Returns the Featureline that meets the argument values.
        /// </summary>
        /// <param name="corridor">The corridor.</param>
        /// <param name="baselineIndex">Index of the baseline.</param>
        /// <param name="regionIndex">Index of the region.</param>
        /// <param name="code">The code.</param>
        /// <param name="side">The side.</param>
        /// <returns></returns>
        
Return Type: Featureline
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ClosestFeaturelineByElement

        /// <summary>
        /// Given a Revit Element it returns the first Featureline that meets the argument values.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="corridor">The corridor.</param>
        /// <param name="baselineIndex">Index of the baseline.</param>
        /// <param name="code">The code.</param>
        /// <param name="side">The side.</param>
        /// <returns></returns>
        
Return Type: Featureline
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### WallBySurface


        /// <summary>
        /// Create walls from surface.
        /// </summary>
        /// <param name="surface"></param>
        /// <param name="wallType"></param>
        /// <param name="structural"></param>
        /// <returns></returns>
        /// <remarks>The wall is recreated but not updated. The input surface must be planar and its normal must be orthogonal to the world Z Axis.</remarks>
        
Return Type: Revit.Elements.Wall
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## Selectable

## ADSK_Parameter

        /// <summary>
        /// Parameter to collect Name and GUID
        /// </summary>
        
## ADSK_Parameters

        /// <summary>
        /// Class that given a parameter name returns the same guid.
        /// This is to have the same CivilConnection parameters in Revit files and tansfer project standards.
        /// </summary>
        
## MultiPointConverter

    /// <summary>
    /// Json Converter fot MultiPoint objects
    /// </summary>
    
### CanConvert
        /// <summary>
        /// Can Convert
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ReadJson
        /// <summary>
        /// Read Json
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        
Return Type: object
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### WriteJson
        /// <summary>
        /// Write Json
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        
Return Type: void
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## ShapePointArrayConverter

    /// <summary>
    /// Json Converter for ShapePoints objects
    /// </summary>
    
### CanConvert
        /// <summary>
        /// Can Convert
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ReadJson

        /// <summary>
        /// Read Json
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        
Return Type: object
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### WriteJson

        /// <summary>
        /// Write Json
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        
Return Type: void
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## ShapePointConverter

    /// <summary>
    /// Json Converter for ShapePoint objects
    /// </summary>
    
### CanConvert
        /// <summary>
        /// Can Convert
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        
Return Type: bool
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ReadJson


        /// <summary>
        /// Read Json
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        
Return Type: object
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### WriteJson

        /// <summary>
        /// Write Json
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        
Return Type: void
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## UtilsSectionView
    /// <summary>
    /// Collection of utilities for SectionViews.
    /// </summary>
    
### CutLines
        /// <summary>
        /// Returns the cut lines.
        /// </summary>
        /// <param name="viewId">The view identifier.</param>
        
Return Type: void
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CloseCopy

        /// <summary>
        /// Closes the copy of the auxilliaary Revit linked file.
        /// </summary>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SketchOptimizer

        /// <summary>
        /// Optimizes the curves.
        /// </summary>
        /// <param name="ca">The ca.</param>
        /// <param name="cb">The cb.</param>
        /// <returns></returns>
        
Return Type: Curve
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ArcsList

        /// <summary>
        /// Returns a list of optimized arcs.
        /// </summary>
        /// <param name="aa">The aa.</param>
        /// <param name="ab">The ab.</param>
        /// <returns></returns>
        
Return Type: List<Arc>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CutCurvesInView

        /// <summary>
        /// Returns the cutting curves in the view.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="viewId">The view identifier.</param>
        /// <returns></returns>
        
Return Type: IList<Curve>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### SolidEdgesToCurve


        /// <summary>
        /// Returns the Solid edges in the View plane.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="s"></param>
        /// <param name="view"></param>
        /// <param name="cat"></param>
        /// <returns></returns>
        
Return Type: IList<Curve>
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
