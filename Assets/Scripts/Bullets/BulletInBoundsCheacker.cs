using System;
using UnityEngine;
using ShootEmUp.Common;
using ShootEmUp.GameSystem.Listeners;
using ShootEmUp.Level;

namespace ShootEmUp.Bullets
{
    public sealed class BulletInBoundsCheacker : MonoBehaviour,IGameFixedUpdateListener
    {
        public event Action<GameObject> OnBulletOutBounds;
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] private Pool bulletPool;

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