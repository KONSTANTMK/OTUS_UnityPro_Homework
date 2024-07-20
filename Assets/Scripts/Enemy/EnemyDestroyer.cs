using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.Components;
using ShootEmUp.GameSystem.Listeners;

namespace ShootEmUp.Enemy
{
    public sealed class EnemyDestroyer : MonoBehaviour
    {
        public event Action<GameObject> OnEnemyDestroyed;
        
        [SerializeField] private Pool enemyPool;
        
        public void DestroyEnemy(GameObject enemy)
        {
            if (enemyPool.ActiveEntities.Remove(enemy))
            {
                enemyPool.ReturnToPull(enemy);
                OnEnemyDestroyed?.Invoke(enemy);
            }
        }
        
    }
}