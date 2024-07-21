using System;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;

namespace ShootEmUp.Components
{
    public sealed class HitPointsComponent : MonoBehaviour,IGameStartListener
    {
        public event Action<GameObject> HpEmpty;
        [SerializeField] private int maxHitPoints;

        private int hitPoints;

        public void OnStartGame() => ResetHitPoints();

        public bool IsAlive()
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

        public void ResetHitPoints()
        {
            hitPoints = maxHitPoints;
        }
    }
}