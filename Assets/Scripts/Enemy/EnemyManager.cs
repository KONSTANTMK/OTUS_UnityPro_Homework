using UnityEngine;
using ShootEmUp.Bullets;

namespace ShootEmUp.Enemy
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private BulletSpawner bulletSpawner;
        [SerializeField] private BulletConfig bulletConfig;
        public void Shoot(GameObject enemy, Vector2 position, Vector2 velocity)
        {
            bulletSpawner.SpawnBullet(bulletConfig, position, velocity);
        }
    }
}