using System;
using UnityEngine;
using ShootEmUp.Components;
using ShootEmUp.GameSystem.Listeners;

namespace ShootEmUp.Bullets
{
    public sealed class Bullet : MonoBehaviour, IGameFinishListener
    {
        public event Action<GameObject> OnCollisionEntered;
        public event Action<GameObject> OnNeedDestroy; 

        [NonSerialized] public bool isPlayer;
        [NonSerialized] public int damage;

        public void OnFinishGame()
        {
            OnNeedDestroy?.Invoke(this.gameObject);
        }

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