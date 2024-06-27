using UnityEngine;

namespace ShootEmUp
{
    public sealed class Character : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent hitPointsComponent;
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private BulletConfig bulletConfig;

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            moveComponent.MoveByRigidbodyVelocity(
                new Vector2(inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }

        public void Shot()
        {
            bulletSystem.SpawnBullet(bulletConfig, weaponComponent.Position,
                weaponComponent.Rotation * Vector3.up);
        }
    }
}