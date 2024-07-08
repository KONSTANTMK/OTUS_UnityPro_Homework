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
        void IGameResumeListener.OnResumeGame() => SetVelocity(oldVelocity);
        
        private void PauseFlying()
        {
            oldVelocity = rigidbody.velocity;
            rigidbody.velocity = new Vector2(0,0);
        }
        
        public void SetVelocity(Vector2 velocity)
        {
            rigidbody.velocity = velocity;
        }
        
    }
}