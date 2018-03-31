using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XPRS.Models.Entities;
using XPRS.Models.Serialized;
using XPRS.Repositories;



namespace XPRS.Controllers
{
    public class ContractorController : ApiController
    {

        private IContractorRepository _repo { get; set; }

        public ContractorController(IContractorRepository repo)
        {
            _repo = repo;
        }
        // GET api/<controller>
        public List<SerializedContractor> Get()
        {
            return _repo.GetContractors();
        }

        // GET api/<controller>/5
        public SerializedContractor Get(int id)
        {
            return _repo.GetContractor(id);
        }

        // POST api/<controller>
        public SerializedContractor Post([FromBody]SerializedContractor value)
        {
            return _repo.CreateContractor(value);
        }

        // PUT api/<controller>/5
        public SerializedContractor Put(int id, [FromBody]SerializedContractor value)
        {
            return _repo.UpdateContractor(value, id);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}