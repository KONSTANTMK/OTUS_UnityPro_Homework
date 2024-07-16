using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.Components;
using ShootEmUp.GameSystem.Listeners;

namespace ShootEmUp.Enemy
{
    public sealed class EnemyDestroyer : MonoBehaviour, IGameFinishListener
    {
        public event Action<GameObject> OnEnemyDestroyed;
        
        [SerializeField] private Pool enemyPool;

        void IGameFinishListener.OnFinishGame() => DestroyAllEnemies();
        private void DestroyAllEnemies()
        {
            for (int i = 0, count = enemyPool.ActiveEntities.Count; i < count; i++)
            {
                var enemy = enemyPool.ActiveEntities.ToArray()[i];
                enemyPool.ReturnToPull(enemy);
                OnEnemyDestroyed?.Invoke(enemy);
            }
        }
        
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