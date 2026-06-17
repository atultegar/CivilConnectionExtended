using CivilConnection.Contracts.Models.Requests;
using CivilConnection.Interop.Wrappers;

namespace CivilConnection.Interop.Interfaces
{
    public interface IIfcService
    {
        string ExportIFC(DocumentWrapper document, IfcExportRequest request);
    }
}
