using System;
using System.Linq;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.GameSystem.Listeners;

namespace ShootEmUp.Bullets
{
    public sealed class BulletDestroyer : MonoBehaviour, IGameFinishListener
    {
        public event Action<GameObject> OnBulletDestroyed;
        [SerializeField] private Pool bulletPool;

        void IGameFinishListener.OnFinishGame() => DestroyAllBullets();
        private void DestroyAllBullets()
        {
            for (int i = 0, count = bulletPool.ActiveEntities.Count; i < count; i++)
            {
                var bullet = bulletPool.ActiveEntities.ToArray()[i];
                bulletPool.ReturnToPull(bullet);
                OnBulletDestroyed?.Invoke(bullet);
            }
        }
        public void DestroyBullet(GameObject bullet)
        {
            if (this.bulletPool.ActiveEntities.Remove(bullet))
            {
                bulletPool.ReturnToPull(bullet);
                OnBulletDestroyed?.Invoke(bullet);
            }
        }
    }
}