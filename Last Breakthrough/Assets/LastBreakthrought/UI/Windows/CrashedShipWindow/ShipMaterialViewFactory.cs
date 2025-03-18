using LastBreakthrought.Factory;
using LastBreakthrought.Infrustructure.AssetManagment;
using LastBreakthrought.UI.ShipMaterial;
using UnityEngine;

namespace LastBreakthrought.Logic.ShipMaterial
{
    public class ShipMaterialViewFactory : BaseFactory<ShipMaterialHandler>
    {
        public ShipMaterialViewFactory(IAssetProvider assetProvider) : base(assetProvider) { }

        public override ShipMaterialHandler Create(Vector3 at, Transform parent) =>
            AssetProvider.Instantiate<ShipMaterialHandler>(AssetPath.ShipMaterialViewPath, at, parent);
    }
}