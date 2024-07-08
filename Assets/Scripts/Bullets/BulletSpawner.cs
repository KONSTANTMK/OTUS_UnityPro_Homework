using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;

namespace ShootEmUp.Bullets
{
    public class BulletSpawner : MonoBehaviour
    {
        public event Action<GameObject> OnBulletSpawned;
        [SerializeField] private Pool bulletPool;

        [SerializeField] private Transform worldTransform;
        
        public void SpawnBullet(BulletConfig config, Vector2 position, Vector2 velocity, bool isPlayer)
        {
            if (!bulletPool.TryDequeue(out var bulletObject)) return;
            Bullet bulletComponent = bulletObject.GetComponent<Bullet>();
            bulletObject.transform.SetParent(this.worldTransform);
            bulletObject.transform.position = position;
            bulletObject.GetComponent<SpriteRenderer>().color = config.color;
            bulletObject.layer = (int)config.physicsLayer;
            bulletObject.GetComponent<BulletMoveController>().SetVelocity(velocity * config.speed);
            bulletComponent.damage = config.damage;
            bulletComponent.isPlayer = isPlayer;
            this.bulletPool.ActiveEntityes.Add(bulletObject);
            OnBulletSpawned?.Invoke(bulletObject);
        }
    }
}