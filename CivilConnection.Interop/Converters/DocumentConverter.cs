using CivilConnection.Contracts.Models;
using CivilConnection.Interop.Wrappers;

namespace CivilConnection.Interop.Converters
{
    public static class DocumentConverter
    {
        public static CivilDocumentData Convert(DocumentWrapper wrapper)
        {
            return new CivilDocumentData
            {
                Name = wrapper.Name,
                Path = wrapper.Path,
                Version = wrapper.Version
            };
        }
    }
}
