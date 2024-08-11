using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.GameSystem.Listeners;
using Zenject;

namespace ShootEmUp.Enemy
{
    public sealed class EnemySpawner : GameListener, IGameFixedUpdateListener
    {
        public event Action<GameObject> OnEnemySpawned;
        
        private EnemyPositions enemyPositions;
        private GameObject character;
        private WorldTransform worldTransform;
        private EnemyPool enemyPool;
        private int spawnCountdown = 4;
        private bool canSpawn = true;
        
        [Inject]
        public void Construct(
            WorldTransform worldTransform,
            Character.Character character,
            EnemyPositions enemyPositions,
            EnemyPool enemyPool
            )
        {
            this.worldTransform = worldTransform;
            this.character = character.gameObject;
            this.enemyPositions = enemyPositions;
            this.enemyPool = enemyPool;
        }
        
        public void OnFixedUpdate(float deltaTime)
        {
            if (canSpawn) SpawnEnemy();
        }
        
        private async void SpawnEnemy()
        {
            if (!enemyPool.TryDequeue(out GameObject enemyObject)) return;
            canSpawn = false;
            enemyObject.transform.SetParent(worldTransform.GetTransfrom);
            var spawnPosition = enemyPositions.RandomSpawnPosition();
            enemyObject.transform.position = spawnPosition.position;
            var attackPosition = enemyPositions.RandomAttackPosition();
            enemyObject.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemyObject.GetComponent<EnemyAttackAgent>().SetTarget(character);
            enemyPool.ActiveEntities.Add(enemyObject);
            OnEnemySpawned?.Invoke(enemyObject);
            await UniTask.WaitForSeconds(spawnCountdown);
            canSpawn = true;
        }
    }
}