using CivilConnection.Contracts.Models;
using CivilConnection.Contracts.Models.Civil;
using CivilConnection.Interop.Context;
using CivilConnection.Interop.Converters;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CivilConnection.Interop.Services
{
    public class DocumentService
    {
        public List<CivilDocumentData> GetAll(string? version = null)
        {
            var context = CivilContext.Create(version);

            var output = new List<CivilDocumentData>();
                       

            foreach (dynamic document in context.Host.Application.Documents)
            {
                var wrapper = new DocumentWrapper(document);

                output.Add(DocumentConverter.Convert(wrapper));
            }

            return output;
        }

        public CivilDocumentData ActiveDocument(CivilContext context)
        {
            dynamic document = context.Host.Application.ActiveDocument;

            var wrapper = new DocumentWrapper(document);
            
            return DocumentConverter.Convert(wrapper);
        }

        public List<CivilDocumentData> GetAllDocuments()
        {
            var contexts = CivilContext.CreateAllContext();

            var output = new List<CivilDocumentData>();

            foreach (dynamic context in contexts)
            {
                foreach (dynamic document in context.Host.Application.Documents)
                {
                    var wrapper = new DocumentWrapper(document);

                    output.Add(DocumentConverter.Convert(wrapper));
                }
            }

            return output;
        }

        //public List<CivilDocumentData> GetDocuments(CivilContext context)
        //{
        //    var output = new List<CivilDocumentData>();

        //    dynamic docs = context.Host.Application.Documents;

            

        //    foreach (dynamic doc in docs)
        //    {
        //        var wrapper = new DocumentWrapper(doc);

        //        output.Add(DocumentConverter.Convert(wrapper));
        //    }

        //    return output;
        //}

        public DocumentWrapper GetActiveDocument(CivilContext context)
        {
            var active = context.Host.Application.ActiveDocument;

            return new DocumentWrapper(active);
        }

        public IList<DocumentWrapper> GetDocuments(CivilContext context)
        {
            var output = new List<DocumentWrapper>();

            foreach (var doc in context.Host.Application.Documents)
            {
                output.Add(new DocumentWrapper(doc));
            }

            return output;
        }

        public UnitSettings GetUnitSettings(DocumentWrapper document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            var precision = document.DistancePrecision;

            return new UnitSettings
            {
                DrawingUnit = document.DrawingUnits,
                Precision = precision,
                Accuracy = Math.Pow(10, -precision)
            };
        }
    }
}
