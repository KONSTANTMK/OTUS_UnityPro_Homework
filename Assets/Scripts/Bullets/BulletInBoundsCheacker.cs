using System;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.Enemy;
using ShootEmUp.GameSystem.Listeners;
using ShootEmUp.Level;
using Zenject;

namespace ShootEmUp.Bullets
{
    public sealed class BulletInBoundsCheacker : MonoBehaviour,IGameFixedUpdateListener
    {
        public event Action<GameObject> OnBulletOutBounds;
        [SerializeField] private LevelBounds levelBounds;
        private BulletPool bulletPool;
        
        [Inject]
        public void Construct(BulletPool bulletPool)
        {
            this.bulletPool = bulletPool;
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