using ShootEmUp.Bullets;
using ShootEmUp.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp.Enemy
{
    public class EnemyShootObserver : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private EnemyDestroyer enemyDestroyer;
        [SerializeField] private BulletSpawner bulletSpawner;

        private void OnEnable()
        {
            enemySpawner.OnEnemySpawned += OnSpawned;
            enemyDestroyer.OnEnemyDestroyed += OnDestroyed;
        }

        private void OnDisable()
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