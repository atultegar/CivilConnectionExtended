using CivilConnection.Interop.Wrappers.Base;
using System;

namespace CivilConnection.Interop.Wrappers
{
    public enum ParameterType
    {
        Unknown = 0,
        Bool,
        Double,
        Long,
        String
    }

    public class ParameterWrapper : ComWrapper
    {
        public ParameterWrapper(object parameter) : base(parameter)
        {
        }

        public string DisplayName => (string)ComObject.DisplayName;

        public string Comment => (string)ComObject.Comment;

        public string Description => (string)ComObject.Description;

        public object Value
        {
            get 
            {
                return ParameterType switch
                {
                    ParameterType.Bool => ComObject.Value,
                    ParameterType.Double => ComObject.Value,
                    ParameterType.Long => ComObject.Value,
                    ParameterType.String => ComObject.Value,
                    ParameterType.Unknown => throw new NotSupportedException(),
                    _ => throw new NotSupportedException()
                };
            }
            set
            {
                switch (ParameterType)
                {
                    case ParameterType.Bool:
                        ComObject.Value = Convert.ToBoolean(value);
                        break;

                    case ParameterType.Double:
                        ComObject.Value = Convert.ToDouble(value);
                        break;

                    case ParameterType.Long:
                        ComObject.Value = Convert.ToInt32(value);
                        break;

                    case ParameterType.String:
                        ComObject.Value = Convert.ToString(value);
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public ParameterType ParameterType
        {
            get
            {
                var typeName = ComObject.GetType().Name;

                return typeName switch
                {
                    "AeccParamBool" => ParameterType.Bool,
                    "AeccParamDouble" => ParameterType.Double,
                    "AeccParamLong" => ParameterType.Long,
                    "AeccParamString" => ParameterType.String,
                    _ => ParameterType.Unknown
                };
            }
        }
    }    
}
