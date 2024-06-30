using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.Components;

namespace ShootEmUp.Enemy
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField]
        private EnemyPositions enemyPositions;

        [SerializeField]
        private GameObject character;

        [SerializeField]
        private Transform worldTransform;
        
        [Header("Pool")]
        [SerializeField] private Pool enemyPool;
        [SerializeField] private EnemyManager enemyManager;
        
        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                SpawnEnemy();
            }
        }
        public void SpawnEnemy()
        {
            Queue<GameObject> pool = enemyPool.GetPool();
            
            if (pool.TryDequeue(out var enemy))
            {
                enemy.transform.SetParent(this.worldTransform);
                var spawnPosition = this.enemyPositions.RandomSpawnPosition();
                enemy.transform.position = spawnPosition.position;
                var attackPosition = this.enemyPositions.RandomAttackPosition();
                enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
                enemy.GetComponent<EnemyAttackAgent>().SetTarget(this.character);
                this.enemyPool.activeEntityes.Add(enemy);
                enemy.GetComponent<HitPointsComponent>().hpEmpty += this.OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire += enemyManager.Shoot;
            }
        }
        
        public void OnDestroyed(GameObject enemy)
        {
            if (enemyPool.activeEntityes.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().hpEmpty -= this.OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= enemyManager.Shoot;
                enemyPool.ReturnToPull(enemy);
            }
        }
    }
}