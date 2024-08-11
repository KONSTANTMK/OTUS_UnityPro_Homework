using System;
using System.Linq;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.Enemy;
using ShootEmUp.GameSystem.Listeners;
using Zenject;

namespace ShootEmUp.Bullets
{
    public sealed class BulletDestroyer : MonoBehaviour
    {
        public event Action<GameObject> OnBulletDestroyed;
        [SerializeField] private Pool bulletPool;
        
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