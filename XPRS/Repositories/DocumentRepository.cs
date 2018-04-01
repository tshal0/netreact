using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XPRS.Models.Serialized;
using XPRS.Models;
using XPRS.Models.Entities;
using XPRS.Utilities;

namespace XPRS.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        ModelContext _db = new ModelContext();
        public SerializedDocument CreateDocument(HttpPostedFile file)
        {
            Document doc = _db.Documents.Create();
            doc.OriginalFileName = file.FileName;
            doc.UniqueFileName = Path.GetRandomFileName();
            FileUtility.SaveFile(file, doc.UniqueFileName);
            _db.Documents.Add(doc);
            return _db.Documents.Where(d => d.DocumentID == doc.DocumentID).Select(SerializedDocument.GenerationLambda).FirstOrDefault();
        }

        public void DeleteDocument(int docID)
        {
            throw new NotImplementedException();
        }
    }
}