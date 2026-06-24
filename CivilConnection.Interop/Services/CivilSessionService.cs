using CivilConnection.Contracts.Interfaces;
using CivilConnection.Contracts.Models;
using CivilConnection.Interop.Context;
using CivilConnection.Interop.Converters;
using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Services
{
    public class CivilSessionService : ICivilSessionService
    {
        public CivilSessionData GetSession()
        {
            var context = CivilContext.Create();

            dynamic document = context.Host.Application.ActiveDocument;

            var docWrapper = new DocumentWrapper(document);

            var activeDoc = DocumentConverter.Convert(docWrapper);


            var data = new CivilSessionData
            {
                IsConnected = true,

                ActiveDocumentName = activeDoc.Name,

                CivilVersion = context.Host.Application.Version,
            };

            foreach (dynamic doc in context.Host.Application.Documents)
            {
                var wrapper = new DocumentWrapper(doc);

                data.Documents.Add(DocumentConverter.Convert(wrapper));
            }
            return data;
        }
    }
}
