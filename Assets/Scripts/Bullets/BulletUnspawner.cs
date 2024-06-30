using System;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.Level;

namespace ShootEmUp.Bullets
{
    public sealed class BulletUnspawner : MonoBehaviour
    {
        public Action<GameObject> OnBulletUnspawned;
        [SerializeField] private LevelBounds levelBounds;
        [Header("Pool")] [SerializeField] private Pool bulletPool;

        private void FixedUpdate()
        {
            IsBulletsInBounds();
        }

        private void IsBulletsInBounds()
        {
            for (int i = 0, count = this.bulletPool.cacheEntities.Count; i < count; i++)
            {
                var bullet = this.bulletPool.cacheEntities[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.UnspawnBullet(bullet);
                }
            }
        }
        
        public void UnspawnBullet(GameObject bullet)
        {
            if (this.bulletPool.activeEntityes.Remove(bullet))
            {
                bulletPool.ReturnToPull(bullet);
                OnBulletUnspawned.Invoke(bullet);
            }
        }
   
    }
}