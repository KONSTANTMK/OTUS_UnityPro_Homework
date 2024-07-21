using System;
using UnityEngine;
using ShootEmUp.Components;
using ShootEmUp.Bullets;
using ShootEmUp.GameSystem.Listeners;

namespace ShootEmUp.Enemy
{
    public sealed class EnemyAttackAgent : MonoBehaviour,IGameFinishListener, IGameFixedUpdateListener
    {
        public event Action<Vector2,BulletConfig,Vector2,bool> OnFire;
        
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private TeamComponent teamComponent;
        [SerializeField] private EnemyMoveAgent moveAgent;
        [SerializeField] private float countdown;
        [SerializeField] private BulletConfig bulletConfig;

        private GameObject target;
        private float currentTime;

        public void OnFinishGame()
        {
            currentTime = countdown;
        }
        
        public void OnFixedUpdate(float deltaTime)
        {
            Fire();
        }
        
        public void SetTarget(GameObject target)
        {
            this.target = target;
        }
        
        private void Fire()
        {
            if (!moveAgent.IsReached) return;
            if (!target.GetComponent<HitPointsComponent>().IsAlive()) return;
            currentTime -= Time.fixedDeltaTime;
            if (currentTime <= 0)
            {
                Vector2 position = weaponComponent.Position;
                Vector2 vector = (Vector2) target.transform.position - position;
                Vector2 velocity = vector.normalized;
                currentTime += countdown;
                OnFire?.Invoke(position, bulletConfig, velocity, teamComponent.IsPlayer);
            }
        }
    }
}