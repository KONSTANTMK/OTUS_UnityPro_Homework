using System;
using UnityEngine;
using ShootEmUp.Components;

namespace ShootEmUp.Bullets
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<GameObject> OnCollisionEntered;

        [NonSerialized] public bool isPlayer;

        [NonSerialized] public int damage;

        [SerializeField] private new Rigidbody2D rigidbody2D;

        [SerializeField] private SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out HitPointsComponent damagable))
            {
                damagable.TakeDamage(damage);
            }

            OnCollisionEntered?.Invoke(this.gameObject);
        }
        
    }
}