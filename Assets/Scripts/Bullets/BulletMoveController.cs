using System.Linq;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp.Bullets
{
    public class BulletMoveController : MonoBehaviour, IGamePauseListener, IGameResumeListener
    {
        [SerializeField] private new Rigidbody2D rigidbody = new();
        private Vector2 oldVelocity;
        void IGamePauseListener.OnPauseGame() => PauseFlying();
        void IGameResumeListener.OnResumeGame() => ResumeFlying();
        
        public void SetVelocity(Vector2 velocity)
        {
            rigidbody.velocity = velocity;
        }
        
        private void PauseFlying()
        {
            oldVelocity = rigidbody.velocity;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        private void ResumeFlying()
        {
            rigidbody.constraints = RigidbodyConstraints2D.None;
            rigidbody.velocity = oldVelocity;
        }
        
    }
}