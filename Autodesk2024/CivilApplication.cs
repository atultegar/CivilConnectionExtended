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


using Autodesk.DesignScript.Runtime;
using Autodesk.Revit.DB;
using CivilConnection.Contracts.Models;
using CivilConnection.Interop.Context;
using CivilConnection.Interop.Services;
using RevitServices.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CivilConnection
{
    /// <summary>
    /// CivilApplication object type.
    /// </summary>
    public class CivilApplication
    {
        #region PRIVATE FIELDS

        private readonly CivilContext _context;

        private readonly DocumentService _documentService;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// The documents in Civil 3D.
        /// </summary>
        public IList<CivilDocument> Documents { get; private set; }

        /// <summary>
        /// The active document
        /// </summary>
        public CivilDocument ActiveDocument { get; private set; }

        /// <summary>
        /// The land XML path.
        /// </summary>
        public string LandXMLPath { get; private set; }

        /// <summary>
        /// Gets the Civil 3D version.
        /// </summary>
        public string Version => _context?.Host?.VersionInfo.Version;

        /// <summary>
        /// Gets the AutoCAD version.
        /// </summary>
        public string AcadVersion => _context?.Host?.VersionInfo.AutoCADVersion;

        /// <summary>
        /// Gets the Civil version.
        /// </summary>
        public string CivilVersion => _context?.Host?.VersionInfo.CivilVersion;
                        
        /// <summary>
        /// Gets the internal COM application.
        /// </summary>
        /// <value>
        /// The internal element.
        /// </value>
        internal object InternalElement => _context?.Host?.Application;

        internal CivilContext Context => _context;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Creates the connection with the running sessions of Civil 3D.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public CivilApplication()
        {
            Utils.InitializeLog();
            Utils.Log($"CivilApplication started...");
            try
            {
                string revitVersion = DocumentManager.Instance.CurrentDBDocument.Application.VersionNumber.ToString();

                var contexts = CivilContext.CreateAllContext();

                Utils.Log($"Revit Version: {revitVersion}");

                if (contexts.Count == 0)
                    throw new Exception("No running Civil 3D instances found.");

                _context = contexts.FirstOrDefault(x => x.Host.VersionInfo.Version.StartsWith(revitVersion)) ?? contexts.FirstOrDefault();

                Utils.Log($"Selected Civil 3D Host: {_context.Host.VersionInfo.Version}");
               
                _documentService = new DocumentService();

                Initialize();
            }
            catch (Exception ex) 
            {
                Utils.Log($"ERROR: {ex.Message}");

                throw new Exception("Could not connect to a running Civil 3D instance", ex);
            }            
        }
        
        internal CivilApplication(string version)
        {
            Utils.InitializeLog();
            Utils.Log($"CivilApplication({version}) started...");
            try
            {
                _context = CivilContext.Create(version);

                _documentService = new DocumentService();

                Initialize();
            }
            catch (Exception ex)
            {
                Utils.Log($"ERROR: {ex.Message}");

                throw new Exception($"Could not connect to Civil 3D {version}\n" + ex.Message, ex);
            }
        }

        /// <summary>
        /// Creates the connection to a specific Civil 3D version.
        /// </summary>
        /// <param name="version">
        /// Civil 3D version year.
        /// Example: 2023, 2024, 2025...
        /// </param>
        public static CivilApplication ByVersion(string version)
        {
            return new CivilApplication(version);
        }

        #endregion

        #region INITIALIZATION

        /// <summary>
        /// Initializes services and documents.
        /// </summary>
        private void Initialize()
        {
            LoadDocuments();

            InitializeSession();

            InitializeRevitUnits();
        }

        /// <summary>
        /// Loads Civil documents.
        /// </summary>
        private void LoadDocuments()
        {
            var docs = _documentService.GetDocuments(_context);

            Documents = docs
                .Select(x => new CivilDocument(x))
                .ToList();

            var activeDoc = _documentService.GetActiveDocument(_context);

            if (activeDoc != null)
            {
                ActiveDocument = new CivilDocument(activeDoc);
            }
        }

        /// <summary>
        /// Initializes session variables.
        /// </summary>
        private void InitializeSession()
        {
            LandXMLPath = Path.GetTempPath();

            SessionVariables.LandXMLPath = LandXMLPath;
            SessionVariables.IsLandXMLExported = false;
            SessionVariables.CivilApplication = this;
            SessionVariables.ParametersCreated = false;
            SessionVariables.DocumentTotalTransform = null;
        }

        /// <summary>
        /// Initializes Revit units based on Civil document.
        /// </summary>
        private void InitializeRevitUnits()
        {
            try
            {
                if (Documents == null || Documents.Count == 0)
                    return;

                var revitDoc = RevitServices.Persistence.DocumentManager.Instance.CurrentDBDocument;

                if (revitDoc == null)
                    return;

                RevitServices.Transactions.TransactionManager.Instance.EnsureInTransaction(revitDoc);

                SetUnits(revitDoc);

                RevitServices.Transactions.TransactionManager.Instance.TransactionTaskDone();

                Utils.Log($"CivilApplication.Units completed.");
            }
            catch (Exception ex)
            {
                Utils.Log($"SetUnits ERROR: {ex}");
            }
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Returns all Civil documents.
        /// </summary>
        /// <returns></returns>
        public IList<CivilDocument> GetDocuments()
        {
            return Documents;
        }

        /// <summary>
        /// Returns Civil document by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CivilDocument GetDocumentByName(string name)
        {
            return Documents.First(x => x.Name == name);
        }

        /// <summary>
        /// Reconnects to Civil 3D.
        /// </summary>
        /// <returns></returns>
        [CanUpdatePeriodically(true)]
        public CivilApplication UpdatePeriodically()
        {
            Utils.Log("CivilApplication.UpdatePeriodically started...");

            if (!string.IsNullOrWhiteSpace(Version))
            {
                return new CivilApplication(Version);
            }

            return new CivilApplication();
        }

        /// <summary>
        /// Writes message to log.
        /// </summary>
        /// <param name="data">The data that is passed through</param>
        /// <param name="message">An optional message to write to the log.</param>
        /// <returns></returns>
        public static object WriteToLog(object data, string message = "")
        {
            Utils.Log($"{(message.Length > 0 ? message + " " : "")}{data}");

            return data;
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Sets Revit units based on Civil 3D document units.
        /// </summary>
        /// <param name="revitDoc"></param>
        /// <exception cref="Exception"></exception>
        private void SetUnits(Document revitDoc)
        {
            if (Documents == null || Documents.Count == 0) 
                return;

            var firstDoc = Documents.First();

            var settings = _documentService.GetUnitSettings(firstDoc._document);

            ApplyRevitUnits(revitDoc, settings);
            
        }

        private static void ApplyRevitUnits(Document revitDoc, UnitSettings settings)
        {
            Units units = revitDoc.GetUnits();

            var map = new Dictionary<DrawingUnitType, ForgeTypeId>
            {
                { DrawingUnitType.Feet, UnitTypeId.Feet },
                { DrawingUnitType.Meters, UnitTypeId.Meters },
                { DrawingUnitType.Millimeters, UnitTypeId.Millimeters },
                { DrawingUnitType.Centimeters, UnitTypeId.Centimeters },
                { DrawingUnitType.Decimeters, UnitTypeId.Decimeters },
                { DrawingUnitType.Inches, UnitTypeId.Inches }
            };

            if (!map.TryGetValue(settings.DrawingUnit, out var unitTypeId))
            {
                throw new Exception("The Civil 3D units are not supported in Revit.");
            }

            var formatOptions = new FormatOptions(unitTypeId)
            {
                Accuracy = settings.Accuracy
            };

            units.SetFormatOptions(SpecTypeId.Length, formatOptions);

            revitDoc.SetUnits(units);
        }

        #endregion

        #region OVERRIDES

        /// <summary>
        /// Public textual representation of the Dynamo node preview.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"CivilApplication(ActiveDocument = {this.ActiveDocument.Name})";
        }

        #endregion
    }
}