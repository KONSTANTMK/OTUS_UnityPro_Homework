using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemyPool enemyPool;

        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private BulletConfig bulletConfig;
        
        private readonly HashSet<GameObject> activeEnemies = new();

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = this.enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (this.activeEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().hpEmpty += this.OnDestroyed;
                        enemy.GetComponent<EnemyAttackAgent>().OnFire += this.OnFire;
                    }    
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().hpEmpty -= this.OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= this.OnFire;
                enemyPool.UnspawnEnemy(enemy);
            }
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 velocity)
        {
            bulletSystem.SpawnBullet(bulletConfig, position, velocity);
        }
    }
}