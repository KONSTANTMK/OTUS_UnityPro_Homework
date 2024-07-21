using System;
using System.Linq;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.GameSystem.Listeners;

namespace ShootEmUp.Bullets
{
    public sealed class BulletDestroyer : MonoBehaviour
    {
        public event Action<GameObject> OnBulletDestroyed;
        [SerializeField] private Pool bulletPool;
        
        public void DestroyBullet(GameObject bullet)
        {
            if (!bulletPool.ActiveEntities.Remove(bullet)) return;
            bulletPool.ReturnToPull(bullet);
            OnBulletDestroyed?.Invoke(bullet);
        }
    }
}