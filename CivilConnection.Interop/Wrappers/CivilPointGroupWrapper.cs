using CivilConnection.Interop.Wrappers.Base;
using System.Collections.Generic;
using System.Linq;

namespace CivilConnection.Interop.Wrappers
{
    public class CivilPointGroupWrapper : ComWrapper
    {
        public CivilPointGroupWrapper(object obj) : base(obj)
        {
            ObjectName = "AeccPointGroup";
        }

        public string Name => (string)ComObject.Name;

        public string IncludeNumbers
        {
            get => ComObject?.QueryBuilder.IncludeNumbers?.ToString() ?? string.Empty;

            set => ComObject.QueryBuilder.IncludeNumbers = value;
        }

        public void AddPoints(IEnumerable<int> pointNumbers)
        {
            if (pointNumbers == null)
                return;

            var numbers = pointNumbers
                .Distinct()
                .Select(x => x.ToString())
                .ToList();

            if (numbers.Count == 0)
                return;

            IncludeNumbers = string.IsNullOrWhiteSpace(IncludeNumbers)
                ? string.Join(",", numbers)
                : $"{IncludeNumbers},{string.Join(",", numbers)}";
        }

        public void ClearPoints()
        {
            IncludeNumbers = string.Empty;
        }

        public override string ToString()
        {
            return $"CivilPointGroup(Name={Name})";
        }
    }
}
