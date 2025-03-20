using LastBreakthrought.Factory;
using LastBreakthrought.Infrustructure.AssetManagment;
using UnityEngine;

namespace LastBreakthrought.NPC.Enemy.Factory
{
    public class EnemyFactory : BaseFactory<IEnemy>
    {
        public EnemyFactory(IAssetProvider assetProvider) : base(assetProvider){}

        //this method create random enemy base on EnemyPath
        public override IEnemy Create(Vector3 at, Transform parent) => 
            AssetProvider.Instantiate<IEnemy>(AssetPath.GetRandomEnemyPath(), at, parent);
    }
}
