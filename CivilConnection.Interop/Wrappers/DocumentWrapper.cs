using CivilConnection.Contracts.Models;
using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;

namespace CivilConnection.Interop.Wrappers
{
    public class DocumentWrapper : ComWrapper
    {
        public DocumentWrapper(object document) : base(document)
        {
        }

        public string Name => (string)ComObject.Name;

        public string Path => (string)ComObject.Path;

        public string Version => (string)ComObject.Application.Name;

        public IEnumerable<CivilSurfaceWrapper> Surfaces
        {
            get
            {
                var surfaces = ComObject.Surfaces;

                foreach (var surface in surfaces)
                {
                    yield return new CivilSurfaceWrapper(surface);
                }
            }
        }

        public DrawingUnitType DrawingUnits
        {
            get
            {
                dynamic units = ComObject.Settings.DrawingSettings.UnitZoneSettings.DrawingUnits;

                return units switch
                {
                    1 => DrawingUnitType.Feet,
                    2 => DrawingUnitType.Meters,
                    3 => DrawingUnitType.Millimeters,
                    4 => DrawingUnitType.Centimeters,
                    5 => DrawingUnitType.Decimeters,
                    6 => DrawingUnitType.Inches,
                    _ => throw new NotSupportedException($"Unsupported drawing unit: {units}")
                };
            }
        }

        public int DistancePrecision =>
            (int)ComObject.Settings.DrawingSettings.AmbientSettings.DistanceSettings.Precision.Value;

        public void SendCommand(string command) => ComObject.SendCommand(command);

        public override string ToString()
        {
            return $"Document (Name = {Name})";
        }
    }
}
