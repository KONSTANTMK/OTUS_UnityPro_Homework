using ShootEmUp.Common;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemy
{
    public sealed class EnemySystem: MonoBehaviour
    {
        private EnemySpawner enemySpawner;
        private EnemySpawnObserver enemySpawnObserver;
        private EnemyShootObserver enemyShootObserver;
        private EnemyPool enemyPool;

        [SerializeField] private int startInstantiateCount;
        [SerializeField] private GameObject prefabToInstantiate;
        [SerializeField] private Transform poolContainer;
        [SerializeField] private int spawnCountdown;

        [Inject]
        public void Construct(
            EnemySpawner enemySpawner,
            EnemySpawnObserver enemySpawnObserver,
            EnemyShootObserver enemyShootObserver,
            EnemyPool enemyPool
        )
        { 
            this.enemyPool = enemyPool; 
            this.enemyPool.SetPrefab(prefabToInstantiate); 
            this.enemyPool.SetStartInstantiateCount(startInstantiateCount); 
            this.enemyPool.SetPoolContainer(poolContainer);

            this.enemySpawner = enemySpawner;
            this.enemySpawner.SetSpawnCountdown(spawnCountdown);
        }
        
        
    }
}