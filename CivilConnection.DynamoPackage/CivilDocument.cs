using CivilConnection.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnectionDynamo
{
    public class CivilDocument
    {
        internal CivilDocumentData Data;

        internal CivilDocument(CivilDocumentData data)
        {
            Data = data;
        }

        public string Name => Data.Name;

        public string FullPath => Data.Path;

        public override string ToString()
        {
            return $"CivilDocument(Name = {this.Name})";
        }
    }
}
