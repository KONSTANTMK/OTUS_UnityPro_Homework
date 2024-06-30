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

        private readonly Queue<Bullet> bulletPool = new();
        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cacheBullets = new();
        
        private void Awake()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var bullet = Instantiate(this.prefab, this.container);
                this.bulletPool.Enqueue(bullet);
            }
        }
        
        private void FixedUpdate()
        {
            this.cacheBullets.Clear();
            this.cacheBullets.AddRange(this.activeBullets);
            IsBulletsInBounds();

        }

        public void SpawnBullet(BulletConfig args, Vector2 position, Vector2 velocity)
        {
            if (this.bulletPool.TryDequeue(out var bullet))
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
            
            if (this.activeBullets.Add(bullet))
            {
                bullet.GetComponent<Bullet>().OnCollisionEntered += UnspawnBullet;
            }
        }
        public void UnspawnBullet(Bullet bullet)
        {
            if (this.activeBullets.Remove(bullet))
            {
                bullet.transform.SetParent(this.container);
                this.bulletPool.Enqueue(bullet);
                bullet.GetComponent<Bullet>().OnCollisionEntered -= UnspawnBullet;
            }
        }

        private void IsBulletsInBounds()
        {
            for (int i = 0, count = this.cacheBullets.Count; i < count; i++)
            {
                var bullet = this.cacheBullets[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.UnspawnBullet(bullet);
                }
            }
        }
        
    }
}