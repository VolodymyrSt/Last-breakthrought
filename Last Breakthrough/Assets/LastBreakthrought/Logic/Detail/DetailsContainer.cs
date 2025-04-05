using System.Collections.Generic;

namespace LastBreakthrought.Logic.ShipDetail
{
    public class DetailsContainer
    {
        public List<ShipDetailEntity> Details { get;  set; }

        public DetailsContainer() => 
            Details = new List<ShipDetailEntity>();

        public bool IsSearchedDetailsAllFound(List<ShipDetailEntity> searchedDetails)
        {
            if (searchedDetails.Count > Details.Count)
                return false;

            foreach (var searchedDetail in searchedDetails)
            {
                foreach (var existedDetail in Details)
                {
                    if (searchedDetail.Data.Id == existedDetail.Data.Id)
                    {
                        if (searchedDetail.Quantity <= existedDetail.Quantity)
                            continue;
                        else
                            return false;
                    }
                    else
                        continue;
                }
            }

            return true;
        }

        public void GiveDetails(List<ShipDetailEntity> neededDetails)
        {
            foreach (var neededDetail in neededDetails)
            {
                foreach (var existedDetail in Details)
                {
                    if (neededDetail.Data.Id == existedDetail.Data.Id)
                    {
                        existedDetail.Quantity -= neededDetail.Quantity;

                        if (existedDetail.Quantity <= 0)
                            Details.Remove(existedDetail);
                    }
                    else
                        continue;
                }
            }
        }
    }
}
