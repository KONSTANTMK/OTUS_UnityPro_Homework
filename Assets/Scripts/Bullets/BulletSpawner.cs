using System;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.Enemy;
using Zenject;


namespace ShootEmUp.Bullets
{
    public class BulletSpawner : MonoBehaviour
    {
        public event Action<GameObject> OnBulletSpawned;
        
        private BulletPool bulletPool;
        
        [Inject]
        public void Construct(BulletPool bulletPool)
        {
            this.bulletPool = bulletPool;
        }

        private WorldTransform worldTransform;
        
        [Inject]
        public void Construct(WorldTransform worldTransform)
        {
            this.worldTransform = worldTransform;
        }
        
        public void SpawnBullet(Vector2 position, BulletConfig config, Vector2 velocity, bool isPlayer)
        {
            if (!bulletPool.TryDequeue(out var bulletObject)) return;
            var bulletComponent = bulletObject.GetComponent<Bullet>();
            bulletObject.transform.SetParent(worldTransform.GetTransfrom);
            bulletObject.transform.position = position;
            bulletObject.GetComponent<SpriteRenderer>().color = config.color;
            bulletObject.layer = (int)config.physicsLayer;
            bulletObject.GetComponent<BulletMoveController>().SetVelocity(velocity * config.speed);
            bulletComponent.damage = config.damage;
            bulletComponent.isPlayer = isPlayer;
            this.bulletPool.ActiveEntities.Add(bulletObject);
            OnBulletSpawned?.Invoke(bulletObject);
        }
    }
}