using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XPRS.Models.Serialized;
using XPRS.Models;
using XPRS.Models.Entities;

namespace XPRS.Repositories
{
    public class ContractorRepository : IContractorRepository
    {
        private ModelContext _db = new ModelContext();

        public SerializedContractor CreateContractor(SerializedContractor sc)
        {
            Contractor ctr = _db.Contractors.Create();
            ctr.Name = sc.Name;
            _db.Contractors.Add(ctr);
            return GetContractor(ctr.ContractorID);

        }

        public void DeleteOrder(int ctrID)
        {
            throw new NotImplementedException();
        }

        public SerializedContractor GetContractor(int ctrID)
        {
            SerializedContractor sc = _db.Contractors.AsQueryable().Where(c => c.ContractorID == ctrID).Select(SerializedContractor.GenerationLambda).FirstOrDefault();
            return sc;
        }

        public List<SerializedContractor> GetContractors()
        {
            return _db.Contractors.AsQueryable().Select(SerializedContractor.GenerationLambda).ToList();
        }

        public SerializedContractor UpdateContractor(SerializedContractor sc, int ctrID)
        {
            Contractor ctr = _db.Contractors.Where(c => c.ContractorID == ctrID).FirstOrDefault();
            ctr.Name = sc.Name;
            _db.SaveChanges();
            return GetContractor(ctrID);
        }
    }
}