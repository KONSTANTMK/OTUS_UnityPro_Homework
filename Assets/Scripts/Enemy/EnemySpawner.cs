using System;
using System.Collections;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.Components;

namespace ShootEmUp.Enemy
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        public event Action<GameObject> OnEnemySpawned;
        
        [Header("Spawn")]
        
        [SerializeField]
        private EnemyPositions enemyPositions;

        [SerializeField]
        private GameObject character;

        [SerializeField]
        private Transform worldTransform;
        
        [Header("Pool")]
        [SerializeField] private Pool enemyPool;
        
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
            if (!enemyPool.TryDequeue(out GameObject enemyObject)) return;
            enemyObject.transform.SetParent(this.worldTransform);
            var spawnPosition = this.enemyPositions.RandomSpawnPosition();
            enemyObject.transform.position = spawnPosition.position;
            var attackPosition = this.enemyPositions.RandomAttackPosition();
            enemyObject.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemyObject.GetComponent<EnemyAttackAgent>().SetTarget(this.character);
            this.enemyPool.ActiveEntityes.Add(enemyObject);
            OnEnemySpawned?.Invoke(enemyObject);
            /*enemyObject.GetComponent<HitPointsComponent>().HpEmpty += this.OnDestroyed;
            enemyObject.GetComponent<EnemyAttackAgent>().OnFire += enemyManager.Shoot;*/
        }
        
    }
}