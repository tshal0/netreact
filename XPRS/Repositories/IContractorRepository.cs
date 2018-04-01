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
    public interface IContractorRepository
    {
        // Get

        SerializedContractor GetContractor(int ctrID);
        List<SerializedContractor> GetContractors();
        

        // Create

        SerializedContractor CreateContractor(SerializedContractor sc);

        // Update 

        SerializedContractor UpdateContractor(SerializedContractor sc, int ctrID);
        //SerializedContractor UploadDocuments(List<HttpPostedFile> files, int ctrID);

        // Delete

        void DeleteOrder(int ctrID);
    }
}
