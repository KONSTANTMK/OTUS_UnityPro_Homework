using System;
using UnityEditor;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        public event Action OnDeath;
        [SerializeField] private HitPointsComponent hitPointsComponent;


        [SerializeField] private GameObject character; 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        
        public bool _fireRequired;
        private void FixedUpdate()
        {
            if (this._fireRequired)
            {
                this.OnFlyBullet();
                this._fireRequired = false;
            }
            
        }
        private void OnFlyBullet()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed
            });
        }
        public void TakeDamage(int damage)
        {
            hitPointsComponent.TakeDamage(damage);
            if (!this.hitPointsComponent.IsHitPointsExists())
            {
                OnDeath?.Invoke();
            }
        }
    }
}