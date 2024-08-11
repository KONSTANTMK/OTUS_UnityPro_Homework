using System;
using Cysharp.Threading.Tasks;
using ShootEmUp.Character;
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
        private GameObject player;
        private WorldTransform worldTransform;
        private EnemyPool enemyPool;
        private int spawnCountdown;
        private bool canSpawn = true;
        
        [Inject]
        public void Construct(
            WorldTransform worldTransform,
            Player player,
            EnemyPositions enemyPositions,
            EnemyPool enemyPool
            )
        {
            this.worldTransform = worldTransform;
            this.player = player.gameObject;
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
            enemyObject.GetComponent<EnemyAttackAgent>().SetTarget(player);
            enemyPool.ActiveEntities.Add(enemyObject);
            OnEnemySpawned?.Invoke(enemyObject);
            await UniTask.WaitForSeconds(spawnCountdown);
            canSpawn = true;
        }

        public void SetSpawnCountdown(int spawnCountdown)
        {
            this.spawnCountdown = spawnCountdown;
        }
    }
}