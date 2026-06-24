using CivilConnection.Interop.Wrappers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Wrappers
{
    public class ProfilePVIWrapper : ComWrapper
    {
        public ProfilePVIWrapper(object profilePVI) : base(profilePVI)
        {
        }

        public double Station => (double)ComObject.Station;

        public double Elevation => (double)ComObject.Elevation;

        public double GradeIn => (double)ComObject.GradeIn;

        public double GradeOut => (double)ComObject.GradeOut;
    }
}
