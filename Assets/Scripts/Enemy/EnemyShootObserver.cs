using ShootEmUp.Bullets;
using ShootEmUp.Components;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp.Enemy
{
    public class EnemyShootObserver : MonoBehaviour,IGameStartListener,IGameFinishListener,IGameResumeListener,IGamePauseListener
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private EnemyDestroyer enemyDestroyer;
        [SerializeField] private BulletSpawner bulletSpawner;
        
        void IGameStartListener.OnStartGame() => StartSubscribe();
        void IGameFinishListener.OnFinishGame() => StopSubscribe();
        void IGameResumeListener.OnResumeGame() => StartSubscribe();
        void IGamePauseListener.OnPauseGame() => StopSubscribe();
        
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