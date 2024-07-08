using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.Components;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;

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
        [SerializeField] private GameManager gameManager;

        void IGameStartListener.OnStartGame() => StartCoroutine(createEnemy(spawnCountdown));
        void IGameFinishListener.OnFinishGame() => StopCoroutine(createEnemy(spawnCountdown));
        void IGamePauseListener.OnPauseGame() => StopCoroutine(createEnemy(spawnCountdown));
        void IGameResumeListener.OnResumeGame() => StartCoroutine(createEnemy(spawnCountdown));
        
        
        private IEnumerator createEnemy(int spawnCountdown)
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
            enemyObject.transform.SetParent(this.worldTransform);
            var spawnPosition = this.enemyPositions.RandomSpawnPosition();
            enemyObject.transform.position = spawnPosition.position;
            var attackPosition = this.enemyPositions.RandomAttackPosition();
            enemyObject.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemyObject.GetComponent<EnemyAttackAgent>().SetTarget(this.character);
            this.enemyPool.ActiveEntityes.Add(enemyObject);
            gameManager.AddListeners(enemyObject.GetComponents<IGameListener>().ToList());
            OnEnemySpawned?.Invoke(enemyObject);
        }
    }
}