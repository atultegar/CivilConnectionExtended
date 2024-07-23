// Copyright (c) 2024 Autodesk, Inc. All rights reserved.
// Author: paolo.serra@autodesk.com, atul.tegar@gmail.com
// 
// Licensed under the Apache License, Version 2.0 (the "License").
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
using Autodesk.AECC.Interop.Land;
using Autodesk.AECC.Interop.UiRoadway;
using Autodesk.AutoCAD.Interop;
using Autodesk.DesignScript.Runtime;
using Autodesk.Revit.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CivilConnection
{
    /// <summary>
    /// CivilApplication object type.
    /// </summary>
    public class CivilApplication
    {
        /// <summary>
        /// The documents in Civil 3D.
        /// </summary>
        public IList<CivilDocument> Documents;
        /// <summary>
        /// The land XML path.
        /// </summary>
        public string LandXMLPath;
        /// <summary>
        /// The active document
        /// </summary>
        AcadDocument ActiveDocument;
        /// <summary>
        /// The active application
        /// </summary>
        AeccRoadwayApplication mApp;
        /// <summary>
        /// Gets the internal element.
        /// </summary>
        /// <value>
        /// The internal element.
        /// </value>
        internal object InternalElement { get { return this.mApp; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rclsid"></param>
        /// <param name="reserved"></param>
        /// <param name="ppunk"></param>
        /// <returns></returns>
        [DllImport("ole32.dll")]
        public static extern int GetActiveObjectExt(ref Guid rclsid, IntPtr reserved, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

        /// <summary>
        /// Gets the application
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        internal AeccRoadwayApplication GetApplication()
        {
            Utils.Log($"GetApplication started...");
            string m_sAcadProdID = "AutoCAD.Application";

            string[] progids = null;

#if C2023
            progids = new string[] {"AeccXUiRoadway.AeccRoadwayApplication.13.5"}; // 2023
#elif C2024
            progids = new string[] { "AeccXUiRoadway.AeccRoadwayApplication.13.6" }; // 2024
#elif C2025
            progids = new string[] { "AeccXUiRoadway.AeccRoadwayApplication.13.7" }; // 2025
#endif
            AcadApplication m_oAcadApp = null;
            try
            {
                object obj = null;
                var type = Type.GetTypeFromProgID(m_sAcadProdID);
                var guid = type.GUID;
                int result = GetActiveObjectExt(ref guid, IntPtr.Zero, out obj);

                if (obj != null)
                {
                    m_oAcadApp = obj as AcadApplication;                    
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

            // Roadway Application

            dynamic output = null;

            foreach (string r_sAeccAppProgId in progids) 
            {
                try
                {
                    output = m_oAcadApp.GetInterfaceObject(r_sAeccAppProgId);

                    if (output != null)
                    {
                        break;
                    }                        
                }
                catch (COMException ex) 
                {
                    if (!ex.ToString().Contains("0x800401E3"))
                    {
                        throw new Exception("Civil 3D Communication Error");
                    }
                }
                catch (Exception) 
                {

                }
            }

            Utils.Log($"GetApplication completed.");
            return output;
        }

        /// <summary>
        /// Creates the connection with the running session of Civil 3D.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public CivilApplication()
        {
            Utils.InitializeLog();
            Utils.Log($"CivilApplication.CivilApplication started...");
            try
            {
                this.mApp = this.GetApplication();
            }
            catch (Exception ex) 
            {
                Utils.Log($"EXCEPTION: {ex.Message}");
            }

            if (this.mApp == null) 
            {
                Utils.Log($"ERROR: Cannot connect to the Civil 3D Application");
                return;
            }

            IList<CivilDocument> documents = new List<CivilDocument>();

            foreach (var doc in this.mApp.Documents) 
            {
                documents.Add(new CivilDocument(doc as AeccRoadwayDocument));
            }

            this.Documents = documents;
            this.ActiveDocument = mApp.ActiveDocument;

            var revitDoc = RevitServices.Persistence.DocumentManager.Instance.CurrentDBDocument;

            RevitServices.Transactions.TransactionManager.Instance.EnsureInTransaction(revitDoc);

            SetUnits(revitDoc);

            RevitServices.Transactions.TransactionManager.Instance.TransactionTaskDone();

            Utils.Log($"CivilApplication.Units completed.");

            SessionVariables.LandXMLPath = System.IO.Path.GetTempPath();
            SessionVariables.IsLandXMLExported = false;
            SessionVariables.CivilApplication = this;
            SessionVariables.ParametersCreated = false;
            SessionVariables.DocumentTotalTransform = null;
            RevitUtils.DocumentTotalTransform();
        }

        /// <summary>
        /// Returns the list of Civil Documents opened in Civil 3D.
        /// </summary>
        /// <returns></returns>
        public IList<CivilDocument> GetDocuments()
        {
            return this.Documents;
        }

        /// <summary>
        /// Returns the Civil Documents opened in Civil 3D with the same name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CivilDocument GetDocumentByName(string name)
        {
            return this.Documents.First(x => x.Name == name);
        }

        /// <summary>
        /// Enables the Run Periodically mode and updates the connection with Civil 3D.
        /// </summary>
        /// <returns></returns>
        [CanUpdatePeriodicallyAttribute(true)]
        public CivilApplication UpdatePeriodically()
        {
            Utils.Log($"CivilApplication.UpdatePeriodically started...");

            return new CivilApplication();
        }

        /// <summary>
        /// Writes a message to the log file
        /// </summary>
        /// <param name="data">The data that is passed through</param>
        /// <param name="message">An optional message to write to the log.</param>
        /// <returns></returns>
        public static object WriteToLog(object data, string message = "")
        {
            Utils.Log(string.Format("{0}{1}", message.Length > 0 ? message + " " : "", data));

            return data;
        }

        /// <summary>
        /// Public textual representation of the Dynamo node preview.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"CivilApplication(ActiveDocument = {this.ActiveDocument.Name})";
        }

        /// <summary>
        /// Setting Revit units based on Civil 3D document units
        /// </summary>
        /// <param name="revitDoc"></param>
        /// <exception cref="Exception"></exception>
        private void SetUnits(Autodesk.Revit.DB.Document revitDoc)
        {
            Autodesk.Revit.DB.Units units = revitDoc.GetUnits();
            var du = this.Documents.First()._document.Settings.DrawingSettings.UnitZoneSettings.DrawingUnits;
            var distPrecision = this.Documents.First()._document.Settings.DrawingSettings.AmbientSettings.DistanceSettings.Precision.Value;
            double accuracy = Math.Pow(10, -distPrecision);

            Utils.Log($"CivilApplication.Units started...");

            var unitTypeMapping = new Dictionary<AeccDrawingUnitType, ForgeTypeId>
            {
                {AeccDrawingUnitType.aeccDrawingUnitMeters, UnitTypeId.Meters },
                {AeccDrawingUnitType.aeccDrawingUnitDecimeters, UnitTypeId.Decimeters },
                {AeccDrawingUnitType.aeccDrawingUnitCentimeters, UnitTypeId.Centimeters },
                {AeccDrawingUnitType.aeccDrawingUnitMillimeters, UnitTypeId.Millimeters },
                {AeccDrawingUnitType.aeccDrawingUnitFeet, UnitTypeId.Feet },
                {AeccDrawingUnitType.aeccDrawingUnitInches, UnitTypeId.Inches }
            };

            if(unitTypeMapping.TryGetValue(du,out var unitTypeId))
            {
                Utils.Log($"Civil Document in {unitTypeId.ToString()}");

                var formatOptions = new FormatOptions(unitTypeId) { Accuracy = accuracy };

                units.SetFormatOptions(SpecTypeId.Length, formatOptions);
            }
            else
            {
                throw new Exception("UNITS ERROR\nThe Civil 3D units of the Active Document are not supported in Revit.\nChange the Civil 3D Units to continue");
            }

            revitDoc.SetUnits(units);
        }
    }
}