using System.Linq;
using ShootEmUp.Components;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemy
{
    public class EnemySpawnObserver : MonoBehaviour,IGameStartListener, IGameFinishListener
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private EnemyDestroyer enemyDestroyer;
        private GameManager gameManager;
        
        [Inject]
        public void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }
        
        public void OnStartGame() => StartSubscribe();
        public void OnFinishGame() => StopSubscribe();
        
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
            gameManager.AddListeners(enemy.GetComponents<IGameListener>().ToList());
            enemy.GetComponent<Enemy>().OnNeedDestroy += enemyDestroyer.DestroyEnemy;
            enemy.GetComponent<HitPointsComponent>().HpEmpty += enemyDestroyer.DestroyEnemy;
            
        }

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<Enemy>().OnNeedDestroy -= enemyDestroyer.DestroyEnemy;
            enemy.GetComponent<HitPointsComponent>().HpEmpty -= enemyDestroyer.DestroyEnemy;
        }
    }
}