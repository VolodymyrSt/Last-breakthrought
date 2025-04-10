using System;
using System.Collections.Generic;
using UnityEngine;

namespace LastBreakthrought.Logic.ShipDetail
{
    [CreateAssetMenu(fileName = "New RequireDetailsForCreating", menuName = "RequireDetailsForCreating")]
    public class RequireDetailsForCreating : ScriptableObject
    {
        public List<DetailEntity> NeededDetails = new();

        public List<ShipDetailEntity> GetRequiredShipDetails()
        {
            List <ShipDetailEntity> neededDetails = new ();

            foreach (var detail in NeededDetails)
                neededDetails.Add(new ShipDetailEntity(detail.Data, detail.Quantity));

            return neededDetails;
        }
    }

    [Serializable]
    public struct DetailEntity
    {
        public ShipDetailSO Data;
        public int Quantity;
    }
}
