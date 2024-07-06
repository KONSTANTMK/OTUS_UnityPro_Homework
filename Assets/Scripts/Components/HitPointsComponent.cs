using System;
using UnityEngine;

namespace ShootEmUp.Components
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> HpEmpty;

        [SerializeField] private int hitPoints;

        public bool IsLive()
        {
            return hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            hitPoints -= damage;
            if (hitPoints <= 0)
            {
                HpEmpty?.Invoke(gameObject);
            }
        }
    }
}