using CivilConnection.Interop.Context;
using CivilConnection.Interop.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CivilConnectionDynamo
{
    public class CivilApplication
    {
        private readonly CivilContext _context;

        private readonly DocumentService _documentService;

        public IList<CivilDocument> Documents { get; private set; }

        internal CivilDocument ActiveDocument { get; private set; }

        public string LandXMLPath { get; private set; }

        internal object InternalElement => _context.Host.Application;

        public CivilApplication()
        {
            _context = CivilContext.Create();

            _documentService = new DocumentService();

            Documents = _documentService.GetAllDocuments().Select(x =>  new CivilDocument(x)).ToList();

            ActiveDocument = new CivilDocument(_documentService.ActiveDocument());

            InitializeSession();

            InitializeRevitUnits();
        }

        public CivilApplication(string version)
        {
            _context = CivilContext.Create(version);

            _documentService = new DocumentService();

            Documents = _documentService
                .GetDocuments(_context)
                .Select(x => new CivilDocument(x))
                .ToList();

            ActiveDocument = new CivilDocument(_documentService.GetActiveDocument(_context));

            InitializeSession();

            InitializeRevitUnits();
        }

        private void InitializeSession()
        {
            LandXMLPath = Path.GetTempPath();

            //#TODO SessionVariables
        }

        private void InitializeRevitUnits()
        {
            // #TODO
        }

        public IList<CivilDocument> GetDocuments()
        {
            return Documents;
        }

        public CivilDocument GetDocumentByName(string name)
        {
            return Documents.First(x => x.Name == name);
        }

        //[CanUpdatePeriodically(true)]
        public CivilApplication UpdatePeriodically()
        {
            return new CivilApplication();
        }

        public static object WriteToLog(object data, string message = "")
        {
            return data;
        }

        public override string ToString()
        {
            return $"Civil 3D {_context.Host.VersionInfo.Version} (ActiveDocument = {this.ActiveDocument.Name})";
        }
    }
}
