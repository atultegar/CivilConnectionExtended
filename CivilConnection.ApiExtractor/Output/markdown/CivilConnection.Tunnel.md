# CivilConnection.Tunnel
## Ring
    /// <summary>
    /// Tunnel Ring object type.
    /// </summary>
    
### Copy

        /// <summary>
        /// Creates a copy of the current Ring.
        /// </summary>
        /// <returns></returns>
        
Return Type: Ring
IsPublic: False
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ByDimensions

        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Reutrns a Ring by dimensions.
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <param name="thickness">The thickness.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="cs">The CoordinateSystem.</param>
        /// <returns></returns>
        
Return Type: Ring
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Transform

        /// <summary>
        /// Transforms a Ring by the specified cs.
        /// </summary>
        /// <param name="cs">The cs.</param>
        /// <returns></returns>
        
Return Type: Ring
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### RotateOnFace

        /// <summary>
        /// Rotates the Ring on the starting face by the angle value.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        
Return Type: Ring
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### Following

        /// <summary>
        /// Returns the followings Ring with face rotated by the specified angle.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        
Return Type: Ring
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
## TbmTunnel
    /// <summary>
    /// Tunnel Bored Machine Tunnel object type.
    /// </summary>
    
### ByBaselineRing

        #endregion

        #region PUBLIC CONSTRUCTORS
        /// <summary>
        /// Bies the baseline ring.
        /// </summary>
        /// <param name="baseline">The baseline.</param>
        /// <param name="ring">The ring.</param>
        /// <returns></returns>
        
Return Type: TbmTunnel
IsPublic: True
Parameters: System.Linq.Enumerable+SelectListIterator`2[CivilConnection.ApiExtractor.Models.ParameterInfo,System.String]
### ToString

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
