using Autodesk.DesignScript.Geometry;
using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Interop.Wrappers;
using Dynamo.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CivilConnection.Converters
{
    internal static class FeaturelineConverter
    {
        internal static Featureline ToDynamo(FeaturelineData data, Baseline baseline)
        {
            var points = data.Points
                .Select(p => Point.ByCoordinates(p.X, p.Y, p.Z))
                .ToList();

            var polyCurve = PolyCurve.ByPoints(points);

            var side = data.Side < 0
                ? Featureline.SideType.Left
                : Featureline.SideType.Right;

            return new Featureline(
                baseline,
                polyCurve,
                data.Code,
                side,
                data.RegionIndex);
        }

        internal static IList<IList<Featureline>> ToDynamo(IList<IList<FeaturelineData>> data, Baseline baseline)
        {
            if (data == null)
                return new List<IList<Featureline>>();

            return data
                .Select(region =>
                    (IList<Featureline>)region
                        .Select(x => ToDynamo(x, baseline))
                        .ToList())
                .ToList();
        }
        

        internal static IList<IList<IList<Featureline>>> ToDynamo(IList<IList<IList<FeaturelineData>>> data, IList<Baseline> baselines)
        {

            if (data == null)
                return new List<IList<IList<Featureline>>>();

            var output = new List<IList<IList<Featureline>>>();

            for (int i = 0; i < data.Count; i++)
            {
                var baseline = baselines[i];

                output.Add(ToDynamo(data[i], baseline));
            }

            return output;
        }

        internal static LandFeatureline ToDynamo(LandFeaturelineData data, DocumentWrapper document)
        {
            var points = data.Points.Select(GeometryConverter.ToProtoPoint).ToList();

            var polyCurve = PolyCurve.ByPoints(Point.PruneDuplicates(points));

            var wrapper = document.HandleToObject(data.Handle);

            return new LandFeatureline(new LandFeaturelineWrapper(wrapper), polyCurve, data.Style);
        }
    }
}
