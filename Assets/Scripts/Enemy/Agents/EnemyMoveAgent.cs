using UnityEngine;
using ShootEmUp.Components;
using ShootEmUp.GameSystem.Listeners;

namespace ShootEmUp.Enemy
{
    public sealed class EnemyMoveAgent : MonoBehaviour,IGameFixedUpdateListener,IGameFinishListener
    {
        [SerializeField] private MoveComponent moveComponent;
        
        private Vector2 destination;
        private bool isReached;
        
        public bool IsReached => isReached; 

        public void OnFinishGame()
        {
            destination = Vector2.zero;
            isReached = false;
        }
        
        public void OnFixedUpdate(float deltaTime)
        {
            Move();
        }
        
        public void SetDestination(Vector2 endPoint)
        {
            destination = endPoint;
            isReached = false;
        }
        private void Move()
        {
            if (isReached) return;
            var vector = destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                isReached = true;
                return;
            }
            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.Move(direction);
        }
        

    }
}