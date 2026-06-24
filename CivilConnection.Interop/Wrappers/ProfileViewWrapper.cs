using CivilConnection.Interop.Wrappers.Base;

namespace CivilConnection.Interop.Wrappers
{
    public class ProfileViewWrapper : ComWrapper
    {
        public ProfileViewWrapper(object profileView) : base(profileView)
        {
        }

        public string DisplayName => (string)ComObject.DisplayName;
    }
}
