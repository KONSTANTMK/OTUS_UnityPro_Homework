using UnityEngine;
using ShootEmUp.Components;

namespace ShootEmUp.Enemy
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached => isReached; 
        
        [SerializeField] private MoveComponent moveComponent;

        private Vector2 destination;

        private bool isReached;
        
        private void FixedUpdate()
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
            if (isReached)
            {
                return;
            }
            
            Vector2 vector = destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                isReached = true;
                return;
            }

            Vector2 direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.Move(direction);
        }
        

    }
}