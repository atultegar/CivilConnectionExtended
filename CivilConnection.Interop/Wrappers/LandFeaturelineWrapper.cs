using CivilConnection.Interop.Wrappers.Base;

namespace CivilConnection.Interop.Wrappers
{
    public class LandFeaturelineWrapper : ComWrapper
    {
        public LandFeaturelineWrapper(object landFeatureline) : base(landFeatureline)
        {
        }

        public string Name => (string)ComObject.Name;

        public double MiniGrade => (double)ComObject.MiniGrade;

        public double MiniElevation => (double)ComObject.MiniElevation;

        public double MaxElevation => (double)ComObject.MaxElevation;

        public double MaxGrade => (double)ComObject.MaxGrade;

        public double[] GetPoints()
        {
            return (double[])ComObject.GetPoints(1);
        }
    }
}
