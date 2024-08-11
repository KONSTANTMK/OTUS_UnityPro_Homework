using UnityEngine;
using ShootEmUp.Bullets;
using ShootEmUp.Components;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;
using Zenject;

namespace ShootEmUp.Character
{
    public sealed class Player : MonoBehaviour, IGameFixedUpdateListener, IGameFinishListener
    {
        [SerializeField] private Vector2 startPosition;
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private TeamComponent teamComponent;
        [SerializeField] private BulletConfig bulletConfig;
        private InputManager inputManager;
        private BulletSpawner bulletSpawner;
        
        
        [Inject]
        public void Construct(InputManager inputManager, BulletSpawner bulletSpawner)
        {
            this.inputManager = inputManager;
            this.bulletSpawner = bulletSpawner;
        }
        
        public void OnFixedUpdate(float deltaTime)
        {
            Move();
        }

        public void OnFinishGame()
        {
            gameObject.transform.position = startPosition;
        }

        private void Move()
        {
            moveComponent.Move(
                new Vector2(inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }

        public void Shot()
        {
            bulletSpawner.SpawnBullet(weaponComponent.Position, bulletConfig, 
                weaponComponent.Rotation * Vector3.up, teamComponent.IsPlayer);
        }
    }
}