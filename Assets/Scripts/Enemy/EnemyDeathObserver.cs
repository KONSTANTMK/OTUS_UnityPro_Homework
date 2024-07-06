using ShootEmUp.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp.Enemy
{
    public class EnemyDeathObserver : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private EnemyDestroyer enemyDestroyer;

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
            enemy.GetComponent<HitPointsComponent>().HpEmpty += enemyDestroyer.DestroyEnemy;
        }

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().HpEmpty -= enemyDestroyer.DestroyEnemy;
        }
    }
}