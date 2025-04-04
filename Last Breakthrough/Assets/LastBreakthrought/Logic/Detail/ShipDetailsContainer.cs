using LastBreakthrought.Logic.ShipDetail;
using System.Collections.Generic;

namespace LastBreakthrought.Logic.ShipDetail
{
    public class ShipDetailsContainer
    {
        public List<ShipDetailEntity> ShipDetails {  get;  set; }

        public ShipDetailsContainer() => 
            ShipDetails = new List<ShipDetailEntity>();
    }
}
