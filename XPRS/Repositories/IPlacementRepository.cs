using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XPRS.Models;
using XPRS.Models.Entities;
using XPRS.Models.Serialized;


namespace XPRS.Repositories
{
    public interface IPlacementRepository
    {
        SerializedPlacement CreatePlacement(SerializedPlacement sp);

        SerializedPlacement GetPlacement(int placementID);
        List<SerializedPlacement> GetPlacements();
        List<SerializedPlacement> GetOrderPlacements(int orderID);
        List<SerializedPlacement> GetContractorPlacements(int ctrID);
        
    }
}
