using System;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.Enemy;
using ShootEmUp.GameSystem.Listeners;
using ShootEmUp.Level;
using Zenject;

namespace ShootEmUp.Bullets
{
    public sealed class BulletInBoundsCheacker : GameListener,IGameFixedUpdateListener
    {
        public event Action<GameObject> OnBulletOutBounds;
        private LevelBounds levelBounds;
        private BulletPool bulletPool;
        
        [Inject]
        public void Construct(BulletPool bulletPool, LevelBounds levelBounds)
        {
            this.bulletPool = bulletPool;
            this.levelBounds = levelBounds;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            IsBulletsInBounds();
        }

        private void IsBulletsInBounds()
        {
            for (int i = 0, count = bulletPool.CacheEntities.Count; i < count; i++)
            {
                var bullet = bulletPool.CacheEntities[i];
                if (!levelBounds.InBounds(bullet.transform.position))
                {
                    if (!bullet.GetComponent<Bullet>().isPlayer)
                    {
                        Debug.Log("Враг промахнулся");
                    }
                    OnBulletOutBounds?.Invoke(bullet);
                }
            }
        }
    }
}