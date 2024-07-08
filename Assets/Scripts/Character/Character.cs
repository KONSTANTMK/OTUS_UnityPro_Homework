using UnityEngine;
using ShootEmUp.Bullets;
using ShootEmUp.Components;
using ShootEmUp.GameSystem;
using UnityEngine.Serialization;

namespace ShootEmUp.Character
{
    public sealed class Character : MonoBehaviour
    {
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private TeamComponent teamComponent;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private BulletSpawner bulletSpawner;
        [SerializeField] private BulletConfig bulletConfig;

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            moveComponent.Move(
                new Vector2(inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }

        public void Shot()
        {
            bulletSpawner.SpawnBullet(bulletConfig, weaponComponent.Position,
                weaponComponent.Rotation * Vector3.up, teamComponent.IsPlayer);
        }
    }
}