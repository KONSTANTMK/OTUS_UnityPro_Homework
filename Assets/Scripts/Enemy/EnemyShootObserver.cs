using ShootEmUp.Bullets;
using ShootEmUp.Common;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemy
{
    public class EnemyShootObserver : GameListener, IGameStartListener,IGameFinishListener
    {
        private EnemySpawner enemySpawner;
        private EnemyDestroyer enemyDestroyer;
        private BulletSpawner bulletSpawner;
        
        [Inject]
        public void Construct(EnemySpawner enemySpawner, EnemyDestroyer enemyDestroyer, BulletSpawner bulletSpawner)
        {
            this.enemySpawner = enemySpawner;
            this.enemyDestroyer = enemyDestroyer;
            this.bulletSpawner = bulletSpawner;
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
            enemy.GetComponent<EnemyAttackAgent>().OnFire += bulletSpawner.SpawnBullet;
        }

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<EnemyAttackAgent>().OnFire -= bulletSpawner.SpawnBullet;
        }
    }
}