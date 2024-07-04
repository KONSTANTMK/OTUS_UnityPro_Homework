using System;
using UnityEngine;

namespace ShootEmUp.Components
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> hpEmpty;

        [SerializeField] private int hitPoints;

        public bool IsLive()
        {
            return this.hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            this.hitPoints -= damage;
            if (this.hitPoints <= 0)
            {
                this.hpEmpty?.Invoke(this.gameObject);
            }
        }
    }
}