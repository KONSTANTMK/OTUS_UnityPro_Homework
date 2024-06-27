using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField]
        private int initialCount = 50;
        
        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;

        private readonly Queue<Bullet> m_bulletPool = new();
        private readonly HashSet<Bullet> m_activeBullets = new();
        private readonly List<Bullet> m_cache = new();
        
        private void Awake()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var bullet = Instantiate(this.prefab, this.container);
                this.m_bulletPool.Enqueue(bullet);
            }
        }
        
        private void FixedUpdate()
        {
            this.m_cache.Clear();
            this.m_cache.AddRange(this.m_activeBullets);

            for (int i = 0, count = this.m_cache.Count; i < count; i++)
            {
                var bullet = this.m_cache[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.UnspawnBullet(bullet, new GameObject());
                }
            }
        }

        public void SpawnBullet(BulletConfig args, Vector2 position, Vector2 velocity)
        {
            if (this.m_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this.worldTransform);
            }
            else
            {
                bullet = Instantiate(this.prefab, this.worldTransform);
            }

            bullet.gameObject.transform.position =position;
            bullet.gameObject.GetComponent<SpriteRenderer>().color=args.color;
            bullet.gameObject.layer=(int)args.physicsLayer;
            bullet.damage = args.damage;
            bullet.isPlayer = args.isPlayer;
            bullet.GetComponent<Rigidbody2D>().velocity =velocity*args.speed;
            
            if (this.m_activeBullets.Add(bullet))
            {
                bullet.GetComponent<Bullet>().OnCollisionEntered += UnspawnBullet;
            }
        }
        public void UnspawnBullet(Bullet bullet, GameObject damagable)
        {
            if (this.m_activeBullets.Remove(bullet))
            {
                bullet.transform.SetParent(this.container);
                this.m_bulletPool.Enqueue(bullet);
                bullet.GetComponent<Bullet>().OnCollisionEntered -= UnspawnBullet;
            }
        }
    }
}