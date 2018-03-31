using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XPRS.Models.Serialized;
using XPRS.Models;
using XPRS.Models.Entities;

namespace XPRS.Repositories
{
    public class PlacementRepository : IPlacementRepository
    {

        private ModelContext _db = new ModelContext();

        public SerializedPlacement CreatePlacement(SerializedPlacement sp)
        {
            Placement placement = _db.Placements.Create();
            Contractor ctr = _db.Contractors.Where(c => c.ContractorID == sp.ContractorID).FirstOrDefault();
            Order ord = _db.Orders.Where(o => o.OrderID == sp.OrderID).FirstOrDefault();
            placement.Contractor = ctr;
            placement.Order = ord;
            _db.Placements.Add(placement);
            _db.SaveChanges();

            return GetPlacement(placement.PlacementID);
        }


        public List<SerializedPlacement> GetContractorPlacements(int ctrID)
        {
            throw new NotImplementedException();
        }

        public List<SerializedPlacement> GetOrderPlacements(int orderID)
        {
            throw new NotImplementedException();
        }

        public List<SerializedPlacement> GetPlacements()
        {
            throw new NotImplementedException();
        }

        public SerializedPlacement GetPlacement(int placementID)
        {
            SerializedPlacement sp = _db.Placements
                .AsQueryable()
                .Where(p => p.PlacementID == placementID)
                .Select(SerializedPlacement.GenerationLambda)
                .FirstOrDefault();
            return sp;
        }
    }
}