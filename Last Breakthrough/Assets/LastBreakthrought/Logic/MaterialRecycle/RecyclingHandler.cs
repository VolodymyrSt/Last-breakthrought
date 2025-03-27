using LastBreakthrought.Logic.ShipDetail;
using LastBreakthrought.Logic.ShipMaterial.ScriptableObjects;
using System.Collections.Generic;

namespace LastBreakthrought.Other.MaterialRecycle
{
    public class RecyclingHandler
    {
        private ShipDetailsContainer _shipDetailsContainer;

        public RecyclingHandler(ShipDetailsContainer shipDetailsContainer)
        {
            _shipDetailsContainer = shipDetailsContainer;
        }

        public void RecycleEntireMaterials(List<ShipMaterialEntity> shipMaterialsEntity)
        {
            foreach (var detail in _shipDetailsContainer.ShipDetails)
            {
                foreach (var material in shipMaterialsEntity)
                {
                    if (material.Data.CraftDetail.Id == detail.Data.Id)
                    {
                        detail.Quantity += material.Quantity;
                        material.Quantity -= material.Quantity;
                    }
                    else
                    {
                        //create detaiview
                    }
                }
            }
        }

        public void RecycleEntireMaterial(ShipMaterialEntity shipMaterialEntity)
        {
            foreach (var detail in _shipDetailsContainer.ShipDetails)
            {
                if (shipMaterialEntity.Data.CraftDetail.Id == detail.Data.Id)
                {
                    detail.Quantity += shipMaterialEntity.Quantity;
                    shipMaterialEntity.Quantity -= shipMaterialEntity.Quantity;
                    break;
                }
                else
                {
                    //create detaiview
                }
            }
        }
    }

    public class ShipDetailsContainer
    {
        public List<ShipDetailEntity> ShipDetails {  get;  set; }

        public ShipDetailsContainer() => 
            ShipDetails = new List<ShipDetailEntity>();
    }
}
