using System;
using System.Collections.Generic;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.Level;

namespace ShootEmUp.Bullets
{
    public class BulletSpawner : MonoBehaviour
    {
        public event Action<GameObject> OnBulletSpawned;
        [Header("Pool")] [SerializeField] private Pool bulletPool;

        [SerializeField] private Transform worldTransform;

        [SerializeField] private LevelBounds levelBounds;

        public void SpawnBullet(BulletConfig config, Vector2 position, Vector2 velocity)
        {
            if (!bulletPool.TryDequeue(out var bulletObject)) return;
            Bullet bulletComponent = bulletObject.GetComponent<Bullet>();
            bulletObject.transform.SetParent(this.worldTransform);
            bulletObject.gameObject.transform.position = position;
            bulletObject.gameObject.GetComponent<SpriteRenderer>().color = config.color;
            bulletObject.gameObject.layer = (int)config.physicsLayer;
            bulletObject.GetComponent<Rigidbody2D>().velocity = velocity * config.speed;
            bulletComponent.damage = config.damage;
            bulletComponent.isPlayer = config.isPlayer;
            this.bulletPool.activeEntityes.Add(bulletObject);
            OnBulletSpawned?.Invoke(bulletObject);
        }
    }
}