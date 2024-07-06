using System;
using UnityEngine;
using ShootEmUp.Common;

namespace ShootEmUp.Bullets
{
    public sealed class BulletDestroyer : MonoBehaviour
    {
        public event Action<GameObject> OnBulletDestroyed;
        [SerializeField] private Pool bulletPool;
        
        public void DestroyBullet(GameObject bullet)
        {
            if (this.bulletPool.ActiveEntityes.Remove(bullet))
            {
                bulletPool.ReturnToPull(bullet);
                OnBulletDestroyed?.Invoke(bullet);
            }
        }
    }
}