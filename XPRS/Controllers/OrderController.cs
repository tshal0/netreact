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
    public class OrderController : ApiController
    {
        private IOrderRepository _repo;

        public OrderController(IOrderRepository repo)
        {
            _repo = repo;
        }
        // GET api/<controller>
        public List<SerializedOrder> Get()
        {
            return _repo.GetOrders();
        }

        // GET api/<controller>/5
        public SerializedOrder Get(int id)
        {
            return _repo.GetOrder(id);
        }

        // POST api/<controller>
        public SerializedOrder Post([FromBody]SerializedOrder value)
        {
            return _repo.CreateOrder(value);
        }

        // PUT api/<controller>/5
        public SerializedOrder Put(int id, [FromBody]SerializedOrder value)
        {
            return _repo.UpdateOrder(value, id);
        }

        public class SetPlacementsDTO
        {
            public List<SerializedContractor> Contractors { get; set; }
        }

        [HttpPut]
        public SerializedOrder SetPlacements(int id, [FromBody]SetPlacementsDTO dto)
        {
            return _repo.UpdateOrCreatePlacements(dto.Contractors, id);
        }

        // DELETE api/<controller>/5
        public List<SerializedOrder> Delete(int id)
        {
            _repo.DeleteOrder(id);
            return _repo.GetOrders();
        }
    }
}