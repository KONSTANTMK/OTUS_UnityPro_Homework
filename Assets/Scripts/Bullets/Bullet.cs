using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnCollisionEntered;

        [NonSerialized] public bool isPlayer;
        
        [NonSerialized] public int damage;

        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out HitPointsComponent damagable))
            {
                damagable.TakeDamage(damage);
            }
            OnCollisionEntered?.Invoke(this);
        }
    }
}