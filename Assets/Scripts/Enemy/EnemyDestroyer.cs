using System;
using UnityEngine;
using ShootEmUp.Common;

namespace ShootEmUp.Enemy
{
    public sealed class EnemyDestroyer : MonoBehaviour
    {
        public event Action<GameObject> OnEnemyDestroyed;
        
        [SerializeField] private Pool enemyPool;
        
        public void DestroyEnemy(GameObject enemy)
        {
            if (!enemyPool.ActiveEntities.Remove(enemy)) return;
            enemyPool.ReturnToPull(enemy);
            OnEnemyDestroyed?.Invoke(enemy);
        }
    }
}