using System;
using UnityEngine;
using ShootEmUp.Components;
using ShootEmUp.Bullets;

namespace ShootEmUp.Enemy
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public event Action<BulletConfig,Vector2,Vector2,bool> OnFire;
        
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private TeamComponent teamComponent;
        [SerializeField] private EnemyMoveAgent moveAgent;
        [SerializeField] private float countdown;
        [SerializeField] private BulletConfig bulletConfig;

        private GameObject target;
        private float currentTime;
        
        private void FixedUpdate()
        {
            Fire();
        }
        
        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        public void Reset()
        {
            currentTime = countdown;
        }
        
        private void Fire()
        {
            if (!moveAgent.IsReached)
            {
                return;
            }
            
            if (!target.GetComponent<HitPointsComponent>().IsLive())
            {
                return;
            }

            currentTime -= Time.fixedDeltaTime;
            if (currentTime <= 0)
            {
                Vector2 position = weaponComponent.Position;
                Vector2 vector = (Vector2) target.transform.position - position;
                Vector2 velocity = vector.normalized;
                OnFire?.Invoke(bulletConfig, position, velocity, teamComponent.IsPlayer);
                currentTime += countdown;
            }
        }
    }
}