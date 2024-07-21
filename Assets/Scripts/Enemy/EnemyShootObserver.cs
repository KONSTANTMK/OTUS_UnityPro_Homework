using ShootEmUp.Bullets;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;

namespace ShootEmUp.Enemy
{
    public class EnemyShootObserver : MonoBehaviour,IGameStartListener,IGameFinishListener
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private EnemyDestroyer enemyDestroyer;
        [SerializeField] private BulletSpawner bulletSpawner;
        
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