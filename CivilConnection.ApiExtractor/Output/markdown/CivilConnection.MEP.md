# CivilConnection.MEP
## CableTray
    /// <summary>
    /// CableTray obejct type.
    /// </summary>
    /// <seealso cref="CivilConnection.AbstractMEPCurve" />
    
### InitObject

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Initialize a CableTray element.
        /// </summary>
        /// <param name="instance"></param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitObject

        /// <summary>
        /// Initialize a CableTray element.
        /// </summary>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetCableTrayByIds

        /// <summary>
        /// Private method to get the CableTrays by ElementId
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        
Return Type: CableTray[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### CableTrayByCurve

        /// <summary>
        /// CableTray by curve.
        /// </summary>
        /// <param name="cableTrayType">Type of the cable tray.</param>
        /// <param name="curve">The curve.</param>
        /// <returns>It Uses the start and end Points of the curve to create the CableTray</returns>
        
Return Type: CableTray
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPolyCurve_

        /// <summary>
        /// Creates a set of CableTrays following a PolyCurve specifying a maximum length.
        /// </summary>
        /// <param name="CableTrayType">The CableTray type.</param>
        /// <param name="polyCurve">The PolyCurve to follow in WCS.</param>
        /// <param name="maxLength">The maximum length of the CableTrays following the PolyCurve.</param>
        /// <returns></returns>
        
Return Type: CableTray[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPoints

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Creates a CableTray by two Points.
        /// </summary>
        /// <param name="cableTrayType">Type of the cable tray.</param>
        /// <param name="start">The start Point in WCS.</param>
        /// <param name="end">The end Point in WCS.</param>
        /// <returns></returns>
        
Return Type: CableTray
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByRevitElement


        /// <summary>
        /// Creates a Conduit by revit Conduit.
        /// </summary>
        /// <param name="element">The MEP Curve from Revit</param>
        /// <returns></returns>
        
Return Type: CableTray
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByCurve

        /// <summary>
        /// Creates a CableTray using the start and end points of a curve.
        /// </summary>
        /// <param name="cableTrayType">The CableTray Type.</param>
        /// <param name="curve">The Curve</param>
        /// <returns></returns>
        
Return Type: CableTray
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByCurveFeatureline

        /// <summary>
        /// CableTray by curve.
        /// </summary>
        /// <param name="cableTrayType">Type of the cable tray.</param>
        /// <param name="curve">The curve.</param>
        /// <param name="featureline">The featureline.</param>
        /// <returns>Associates the CableTray to a Featureline.</returns>
        
Return Type: CableTray
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPolyCurve

       
        /// <summary>
        /// Creates a list of CableTrays from a PolyCurve.
        /// </summary>
        /// <param name="cableTrayType">Type of the cable tray.</param>
        /// <param name="polyCurve">The poly curve.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="featureline">The featureline.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
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
## Conduit
    /// <summary>
    /// Conduit object type.
    /// </summary>
    /// <seealso cref="CivilConnection.AbstractMEPCurve" />
    
### InitObject

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Initialize a Conduit element
        /// </summary>
        /// <param name="instance">The instance.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitObject

        /// <summary>
        /// Initialize a Conduit element
        /// </summary>
        /// <param name="conduitType">Type of the conduit.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetConduitByIds

        /// <summary>
        /// Gets the conduit by ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        
Return Type: Conduit[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPolyCurve

Return Type: Conduit[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPolyCurve

        /// <summary>
        /// Create Conduits following a polycurve
        /// </summary>
        /// <param name="conduitType">The conduit type.</param>
        /// <param name="polyCurve">the Polycurve to follow.</param>
        /// <returns></returns>
        
Return Type: Conduit[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPolyCurve

        /// <summary>
        /// Creates a list of Conduits from a PolyCurve.
        /// </summary>
        /// <param name="conduitType">Type of the conduit.</param>
        /// <param name="polyCurve">The poly curvein WCS.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="featureline">The featureline.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPoints
        #endregion

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Creates a Conduit by two points.
        /// </summary>
        /// <param name="conduitType">Type of the conduit.</param>
        /// <param name="start">The start point.</param>
        /// <param name="end">The end point.</param>
        /// <returns></returns>
        
Return Type: Conduit
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByCurve

        /// <summary>
        /// Creates a Conduit by a curve.
        /// </summary>
        /// <param name="conduitType">Type of the conduit.</param>
        /// <param name="curve">The curve.</param>
        /// <returns></returns>
        
Return Type: Conduit
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByCurveFeatureline

        /// <summary>
        /// Creates a Conduit by a curve.
        /// </summary>
        /// <param name="conduitType">Type of the conduit.</param>
        /// <param name="curve">The curve.</param>
        /// <param name="featureline">The featureline.</param>
        /// <returns></returns>
        
Return Type: Conduit
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByRevitElement

        /// <summary>
        /// Creates a Conduit by revit Conduit.
        /// </summary>
        /// <param name="element">The MEP Curve from Revit</param>
        /// <returns></returns>
        
Return Type: Conduit
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
## Connector
    /// <summary>
    /// Connector object type.
    /// </summary>
    /// <seealso cref="Revit.Elements.Element" />
    
### InternalSetConnector

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Internals the set connector.
        /// </summary>
        /// <param name="fi">The Revit connector</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InternalSetAngle

        /// <summary>
        /// Method to set the angle.
        /// </summary>
        /// <param name="angle">The angle.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InternalSetHeight

        /// <summary>
        ///  Method to set the height.
        /// </summary>
        /// <param name="height">The height.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InternalSetOrigin

        /// <summary>
        /// Internals the set origin.
        /// </summary>
        /// <param name="origin">The origin.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InternalSetRadius

        /// <summary>
        /// Method to set the radius.
        /// </summary>
        /// <param name="radius">The radius.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InternalSetWidth

        /// <summary>
        /// Method to set the width.
        /// </summary>
        /// <param name="width">The width.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitObject

        /// <summary>
        /// Initialize a Connector
        /// </summary>
        /// <param name="instance">The instance.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## Duct
    /// <summary>
    /// Duct object type.
    /// </summary>
    /// <seealso cref="CivilConnection.AbstractMEPCurve" />
    
### InitPipe

        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Initialize a Duct element
        /// </summary>
        /// <param name="instance">The instance.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitPipe

        /// <summary>
        /// Initialize a Duct element
        /// </summary>
        /// <param name="pipeType">Type of the pipe.</param>
        /// <param name="systemType">Type of the system.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="level">The level.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InternalSetPipingSystemType

        /// <summary>
        /// Internals the type of the set piping system.
        /// </summary>
        /// <param name="type">The type.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetDuctsByIds

        /// <summary>
        /// Gets the ducts by ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        
Return Type: Duct[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPoints

        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Creates a pipe by two points.
        /// </summary>
        /// <param name="ductType">Type of the duct.</param>
        /// <param name="mechanicalSystemType">Type of the mechanical system.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        
Return Type: Duct
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByRevitElement

        /// <summary>
        /// Creates a Conduit by revit Conduit.
        /// </summary>
        /// <param name="element">The MEP Curve from Revit</param>
        /// <returns></returns>
        
Return Type: Duct
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByCurve

        /// <summary>
        /// Creates a pipe by curve.
        /// </summary>
        /// <param name="ductType">Type of the duct.</param>
        /// <param name="mechanicalSystemType">Type of the mechanical system.</param>
        /// <param name="curve">The curve.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        
Return Type: Duct
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByCurveFeatureline

        /// <summary>
        /// Creates a pipe by curve.
        /// </summary>
        /// <param name="ductType">Type of the duct.</param>
        /// <param name="mechanicalSystemType">Type of the mechanical system.</param>
        /// <param name="level">The level.</param>
        /// <param name="curve">The curve.</param>
        /// <param name="featureline">The featureline.</param>
        /// <returns></returns>
        
Return Type: Duct
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPolyCurve

        /// <summary>
        /// Creates a pipe by PolyCurve.
        /// </summary>
        /// <param name="ductType">Type of the duct.</param>
        /// <param name="mechanicalSystemType">Type of the mechanical system.</param>
        /// <param name="polyCurve">The poly curve.</param>
        /// <param name="level">The level.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="featureline">The featureline.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## Fitting
    /// <summary>
    /// Fitting obejct type.
    /// </summary>
    /// <seealso cref="Revit.Elements.AbstractFamilyInstance" />
    
### InitFitting

        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Initializes the fitting.
        /// </summary>
        /// <param name="instance">The instance.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitElbowObject

        /// <summary>
        /// Initializes the elbow object.
        /// </summary>
        /// <param name="c1">The first connector.</param>
        /// <param name="c2">The second connector.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitUnionObject

        /// <summary>
        /// Initializes the union object.
        /// </summary>
        /// <param name="c1">The first connector.</param>
        /// <param name="c2">The second connector.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitConnection

        /// <summary>
        /// Initializes the connection.
        /// </summary>
        /// <param name="c1">The first connector.</param>
        /// <param name="c2">The second connector.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitTransitionObject

        /// <summary>
        /// Initializes the transition object.
        /// </summary>
        /// <param name="c1">The first connector.</param>
        /// <param name="c2">The second connector.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitTeeObject

        /// <summary>
        /// Initializes the tee object.
        /// </summary>
        /// <param name="c1">The first connector.</param>
        /// <param name="c2">The second connector.</param>
        /// <param name="c3">The third connector.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitCrossObject

        /// <summary>
        /// Initializes the cross object.
        /// </summary>
        /// <param name="c1">The first connector.</param>
        /// <param name="c2">The second connector.</param>
        /// <param name="c3">The third connector.</param>
        /// <param name="c4">The fourth connector.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitTakeoffObject

        /// <summary>
        /// Initializes the takeoff object.
        /// </summary>
        /// <param name="c1">The conenctor.</param>
        /// <param name="curve">The curve.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Connectors


        /// <summary>
        /// Connectorses this instance.
        /// </summary>
        /// <returns></returns>
        
Return Type: CivilConnection.MEP.Connector[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Elbow
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Creates an elbow fitting.
        /// </summary>
        /// <param name="curve1">The curve1.</param>
        /// <param name="curve2">The curve2.</param>
        /// <returns></returns>
        
Return Type: Fitting
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Union

        /// <summary>
        /// Creates an union fitting.
        /// </summary>
        /// <param name="curve1">The curve1.</param>
        /// <param name="curve2">The curve2.</param>
        /// <returns></returns>
        
Return Type: Fitting
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Transition

        /// <summary>
        /// Creates a transition fitting.
        /// </summary>
        /// <param name="curve1">The curve1.</param>
        /// <param name="curve2">The curve2.</param>
        /// <returns></returns>
        
Return Type: Fitting
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## Pipe
    /// <summary>
    /// Pipe obejct type.
    /// </summary>
    /// <seealso cref="CivilConnection.AbstractMEPCurve" />
    
### InitObject

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Initialize a Pipe element
        /// </summary>
        /// <param name="instance">The instance.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitObject

        /// <summary>
        /// Initialize a Pipe element
        /// </summary>
        /// <param name="pipeType">Type of the pipe.</param>
        /// <param name="pipingSystemType">Type of the piping system.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="level">The level.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InternalSetPipingSystemType

        /// <summary>
        /// Internals the type of the set piping system.
        /// </summary>
        /// <param name="type">The type.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### GetPipesByIds

        /// <summary>
        /// Gets the pipes by ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        
Return Type: Pipe[]
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPoints

        #endregion

        #region PUBLIC METHODS
       
        /// <summary>
        /// Creates a pipe by two points.
        /// </summary>
        /// <param name="pipeType">Type of the pipe.</param>
        /// <param name="pipingSystemType">Type of the piping system.</param>
        /// <param name="start">The start point.</param>
        /// <param name="end">The end point.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        
Return Type: Pipe
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByRevitElement

        /// <summary>
        /// Creates a Conduit by revit Conduit.
        /// </summary>
        /// <param name="element">The MEP Curve from Revit</param>
        /// <returns></returns>
        
Return Type: Pipe
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByCurve

        /// <summary>
        /// Creates a pipe by curve.
        /// </summary>
        /// <param name="pipeType">Type of the pipe.</param>
        /// <param name="pipingSystemType">Type of the piping system.</param>
        /// <param name="curve">The curve.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        
Return Type: Pipe
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByCurveFeatureline

        /// <summary>
        /// Creates a pipe by curve.
        /// </summary>
        /// <param name="pipeType">Type of the pipe.</param>
        /// <param name="pipingSystemType">Type of the piping system.</param>
        /// <param name="level">The level.</param>
        /// <param name="curve">The curve.</param>
        /// <param name="featureline">The featureline.</param>
        /// <returns></returns>
        
Return Type: Pipe
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPolyCurve

        /// <summary>
        /// Creates a pipe by PolyCurve.
        /// </summary>
        /// <param name="pipeType">Type of the pipe.</param>
        /// <param name="pipingSystemType">Type of the piping system.</param>
        /// <param name="polyCurve">The poly curve.</param>
        /// <param name="level">The level.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="featureline">The featureline.</param>
        /// <returns></returns>
        
Return Type: Dictionary<string, object>
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
## PipePlaceHolder
    /// <summary>
    /// PipePlaceHolder obejct type.
    /// </summary>
    /// <seealso cref="CivilConnection.AbstractMEPCurve" />
    
### InitPipe

        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Initialize a Pipe element.
        /// </summary>
        /// <param name="instance">The instance.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InitPipe

        /// <summary>
        /// Initialize a Pipe element.
        /// </summary>
        /// <param name="pipeType">Type of the pipe.</param>
        /// <param name="systemType">Type of the system.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="level">The level.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### InternalSetPipingSystemType

        /// <summary>
        /// Internals the type of the set piping system.
        /// </summary>
        /// <param name="type">The type.</param>
        
Return Type: void
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByPoints

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Creates a PipePlaceholder by two points.
        /// </summary>
        /// <param name="pipeType">Type of the pipe.</param>
        /// <param name="systemType">Type of the system.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        
Return Type: PipePlaceHolder
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
