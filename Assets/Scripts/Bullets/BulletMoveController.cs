using System.Linq;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp.Bullets
{
    public class BulletMoveController : MonoBehaviour,IGameFixedUpdateListener, IGamePauseListener, IGameResumeListener

    {
        [FormerlySerializedAs("rigidbody")] [SerializeField]
        private Rigidbody2D rigidbodyComponent;
        [SerializeField]
        private Vector2 savedVelocity;

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