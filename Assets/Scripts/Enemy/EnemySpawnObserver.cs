using System.Linq;
using ShootEmUp.Common;
using ShootEmUp.Components;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemy
{
    public class EnemySpawnObserver : GameListener, IGameStartListener, IGameFinishListener
    {
        private EnemySpawner enemySpawner;
        private EnemyDestroyer enemyDestroyer;
        
        [Inject]
        public void Construct(EnemySpawner enemySpawner, EnemyDestroyer enemyDestroyer)
        {
            this.enemySpawner = enemySpawner;
            this.enemyDestroyer = enemyDestroyer;
        }
        
        public void OnStartGame() => StartSubscribe();
        public void OnFinishGame() => StopSubscribe();
        
        private void StartSubscribe()
        {
            enemySpawner.OnEnemySpawned += OnSpawned;
            enemyDestroyer.OnEnemyDestroyed += OnDestroyed;
        }

        private void StopSubscribe()
        {
            enemySpawner.OnEnemySpawned -= OnSpawned;
            enemyDestroyer.OnEnemyDestroyed -= OnDestroyed;
        }

        private void OnSpawned(GameObject enemy)
        {
            gameManager.AddListeners(enemy.GetComponents<IGameListener>().ToList());
            enemy.GetComponent<Enemy>().OnNeedDestroy += enemyDestroyer.DestroyEnemy;
            enemy.GetComponent<HitPointsComponent>().HpEmpty += enemyDestroyer.DestroyEnemy;
            
        }

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<Enemy>().OnNeedDestroy -= enemyDestroyer.DestroyEnemy;
            enemy.GetComponent<HitPointsComponent>().HpEmpty -= enemyDestroyer.DestroyEnemy;
        }
    }
}