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
        private void SpawnEnemy()
        {
            Debug.Log("Вошли");
            if (!enemyPool.TryDequeue(out GameObject enemyObject)) return;
            Debug.Log("ВЫШЛИ");
            enemyObject.transform.SetParent(this.worldTransform);
            var spawnPosition = this.enemyPositions.RandomSpawnPosition();
            enemyObject.transform.position = spawnPosition.position;
            var attackPosition = this.enemyPositions.RandomAttackPosition();
            enemyObject.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemyObject.GetComponent<EnemyAttackAgent>().SetTarget(this.character);
            this.enemyPool.activeEntityes.Add(enemyObject);
            enemyObject.GetComponent<HitPointsComponent>().hpEmpty += this.OnDestroyed;
            enemyObject.GetComponent<EnemyAttackAgent>().OnFire += enemyManager.Shoot;
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