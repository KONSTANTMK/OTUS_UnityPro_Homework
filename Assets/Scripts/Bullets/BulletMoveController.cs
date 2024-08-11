using ShootEmUp.GameSystem.Listeners;
using UnityEngine;

namespace ShootEmUp.Bullets
{
    public sealed class BulletMoveController : MonoBehaviour,IGameFixedUpdateListener, IGamePauseListener, IGameResumeListener

    {
        [SerializeField] private Rigidbody2D rigidbodyComponent;
        [SerializeField] private Vector2 savedVelocity;

        public void OnFixedUpdate(float deltaTime)
        {
            savedVelocity = rigidbodyComponent.velocity;
        }
        public void OnPauseGame()
        {
            rigidbodyComponent.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        public void OnResumeGame()
        {
            rigidbodyComponent.constraints = RigidbodyConstraints2D.None;
            rigidbodyComponent.velocity = savedVelocity;
      
        }
        
        public void SetVelocity(Vector2 velocity)
        {
            rigidbodyComponent.velocity = velocity;
        }
    }
}