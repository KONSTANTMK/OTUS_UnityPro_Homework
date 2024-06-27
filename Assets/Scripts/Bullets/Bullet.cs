using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, GameObject> OnCollisionEntered;

        [NonSerialized] public bool isPlayer;
        [NonSerialized] public int damage;

        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Пуля попала в "+collision.gameObject.name);
            this.OnCollisionEntered?.Invoke(this, collision.gameObject);
        }
    }
}