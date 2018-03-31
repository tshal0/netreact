using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using XPRS.Models;
using XPRS.Models.Entities;
using XPRS.Models.Serialized;


namespace XPRS.Repositories
{
    public interface IDocumentRepository
    {
        // Create
        SerializedDocument CreateDocument(HttpPostedFile file);        
        // Delete
        void DeleteDocument(int docID);
    }
}
