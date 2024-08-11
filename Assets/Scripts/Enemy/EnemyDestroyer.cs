using System;
using UnityEngine;
using ShootEmUp.Common;
using Zenject;

namespace ShootEmUp.Enemy
{
    public sealed class EnemyDestroyer : GameListener
    {
        public event Action<GameObject> OnEnemyDestroyed;
        
        private EnemyPool enemyPool;
        
        [Inject]
        public void Construct(EnemyPool enemyPool)
        {
            this.enemyPool = enemyPool;
        }
        
        public void DestroyEnemy(GameObject enemy)
        {
            if (!enemyPool.ActiveEntities.Remove(enemy)) return;
            enemyPool.ReturnToPull(enemy);
            OnEnemyDestroyed?.Invoke(enemy);
        }
    }
}