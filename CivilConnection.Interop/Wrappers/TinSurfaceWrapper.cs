using CivilConnection.Interop.Wrappers.Base;

namespace CivilConnection.Interop.Wrappers
{
    public class TinSurfaceWrapper : ComWrapper
    {
        public TinSurfaceWrapper(object obj) : base(obj)
        {
            ObjectName = "AeccTinSurface";
        }

        public string Description
        {
            get => (string)ComObject.Description;
            set => ComObject.Description = value;
        }

        public void AddPointGroup(CivilPointGroupWrapper pointGroup)
        {
            ComObject.PointGroups.Add(pointGroup.ComObject);
        }

        public void Rebuild()
        {
            ComObject.Rebuild();
        }
    }
}
