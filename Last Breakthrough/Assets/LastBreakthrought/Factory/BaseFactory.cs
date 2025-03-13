using LastBreakthrought.Infrustructure.AssetManagment;
using UnityEngine;

namespace LastBreakthrought.Factory
{
    public abstract class BaseFactory<T> where T : class 
    {
        protected IAssetProvider AssetProvider;
        public BaseFactory(IAssetProvider assetProvider) => 
            AssetProvider = assetProvider;

        public abstract T Create(Vector3 at, Transform parent);

        public virtual void SpawnAt(Vector3 at, Transform parent)
        {
            var ship = Create(at , parent);
        }
        public virtual void Spawn()
        {
            var ship = Create(Vector3.zero, null);
        }
    }
}
