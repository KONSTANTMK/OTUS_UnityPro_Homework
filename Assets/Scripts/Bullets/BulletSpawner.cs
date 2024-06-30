using System;
using System.Collections.Generic;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.Level;

namespace ShootEmUp.Bullets
{
    public class BulletSpawner : MonoBehaviour
    {
        public Action<GameObject> OnBulletSpawned;
        [Header("Pool")] [SerializeField] private Pool bulletPool;

        [SerializeField] private Transform worldTransform;

        [SerializeField] private LevelBounds levelBounds;

        public void SpawnBullet(BulletConfig config, Vector2 position, Vector2 velocity)
        {
            Queue<GameObject> pool = bulletPool.GetPool();
            if (pool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this.worldTransform);
                bullet.gameObject.transform.position = position;
                bullet.gameObject.GetComponent<SpriteRenderer>().color = config.color;
                bullet.gameObject.layer = (int)config.physicsLayer;
                bullet.GetComponent<Bullet>().damage = config.damage;
                bullet.GetComponent<Bullet>().isPlayer = config.isPlayer;
                bullet.GetComponent<Rigidbody2D>().velocity = velocity * config.speed;
                this.bulletPool.activeEntityes.Add(bullet);
                OnBulletSpawned.Invoke(bullet);
            }
        }
    }
}