using System;
using UnityEngine;
using ShootEmUp.Enemy;
using Zenject;

namespace ShootEmUp.Bullets
{
    public sealed class BulletDestroyer
    {
        public event Action<GameObject> OnBulletDestroyed;
        private BulletPool bulletPool;
        
        [Inject]
        public void Construct(BulletPool bulletPool)
        {
            this.bulletPool = bulletPool;
        }
        
        public void DestroyBullet(GameObject bullet)
        {
            if (!bulletPool.ActiveEntities.Remove(bullet)) return;
            bulletPool.ReturnToPull(bullet);
            OnBulletDestroyed?.Invoke(bullet);
        }
    }
}