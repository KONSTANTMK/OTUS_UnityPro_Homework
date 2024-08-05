using System;
using System.Collections;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.GameSystem.Listeners;
using Zenject;

namespace ShootEmUp.Enemy
{
    public sealed class EnemySpawner : MonoBehaviour, IGameStartListener,IGameFinishListener,IGamePauseListener,IGameResumeListener
    {
        public event Action<GameObject> OnEnemySpawned;
        
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private GameObject character;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private Pool enemyPool;
        [SerializeField] private int spawnCountdown;
        private EnemySpawnObserver enemySpawnObserver;
        
        [Inject]
        public void Construct(EnemySpawnObserver enemySpawnObserver)
        {
            this.enemySpawnObserver = enemySpawnObserver;
        }
        
        public void OnStartGame() => StartCoroutine("createEnemy");
        public void OnFinishGame() => StopCoroutine("createEnemy");
        public void OnPauseGame() => StopCoroutine("createEnemy");
        public void OnResumeGame() => StartCoroutine("createEnemy");
        
        
        private IEnumerator createEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnCountdown);
                SpawnEnemy();
            }
        }
        private void SpawnEnemy()
        {
            if (!enemyPool.TryDequeue(out GameObject enemyObject)) return;
            enemyObject.transform.SetParent(worldTransform);
            var spawnPosition = enemyPositions.RandomSpawnPosition();
            enemyObject.transform.position = spawnPosition.position;
            var attackPosition = enemyPositions.RandomAttackPosition();
            enemyObject.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemyObject.GetComponent<EnemyAttackAgent>().SetTarget(character);
            enemyPool.ActiveEntities.Add(enemyObject);
            OnEnemySpawned?.Invoke(enemyObject);
        }
    }
}