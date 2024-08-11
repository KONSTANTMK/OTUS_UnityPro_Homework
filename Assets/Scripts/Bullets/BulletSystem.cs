using ShootEmUp.Bullets;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemy
{
    public sealed class BulletSystem: MonoBehaviour
    {
        private BulletSpawner bulletSpawner;
        private BulletSpawnObserver bulletSpawnObserver;
        private BulletInBoundsObserver bulletInBoundsObserver;
        private BulletPool bulletPool;

        [SerializeField] private int startInstantiateCount;
        [SerializeField] private GameObject prefabToInstantiate;
        [SerializeField] private Transform poolContainer; 

        [Inject]
        public void Construct(
            BulletSpawner bulletSpawner,
            BulletSpawnObserver bulletSpawnObserver,
            BulletInBoundsObserver bulletInBoundsObserver,
            BulletPool bulletPool
        )
        { 
            this.bulletPool = bulletPool; 
            this.bulletPool.SetPrefab(prefabToInstantiate); 
            this.bulletPool.SetStartInstantiateCount(startInstantiateCount); 
            this.bulletPool.SetPoolContainer(poolContainer);
        }
    }
}