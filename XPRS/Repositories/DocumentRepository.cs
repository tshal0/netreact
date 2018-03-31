using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XPRS.Models.Serialized;
using XPRS.Models;
using XPRS.Models.Entities;

namespace XPRS.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        public SerializedDocument CreateDocument(HttpPostedFile file)
        {
            throw new NotImplementedException();
        }

        public void DeleteDocument(int docID)
        {
            throw new NotImplementedException();
        }
    }
}