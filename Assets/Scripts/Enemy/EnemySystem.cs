using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemy
{
    public class EnemySystem: MonoBehaviour
    {
        private EnemySpawner enemySpawner;
        private EnemySpawnObserver enemySpawnObserver;
        private EnemyShootObserver enemyShootObserver;
        
        [Inject]
        public void Construct(
            EnemySpawner enemySpawner,
            EnemySpawnObserver enemySpawnObserver,
            EnemyShootObserver enemyShootObserver
        )
        {
        }
    }
}