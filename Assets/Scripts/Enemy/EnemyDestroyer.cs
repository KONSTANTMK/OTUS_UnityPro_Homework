using System;
using System.Collections;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.Components;

namespace ShootEmUp.Enemy
{
    public sealed class EnemyDestroyer : MonoBehaviour
    {
        public event Action<GameObject> OnEnemyDestroyed;
        
        [SerializeField] private Pool enemyPool;
        
        public void DestroyEnemy(GameObject enemy)
        {
            if (enemyPool.ActiveEntityes.Remove(enemy))
            {
                enemyPool.ReturnToPull(enemy);
                OnEnemyDestroyed?.Invoke(enemy);
            }
        }
    }
}