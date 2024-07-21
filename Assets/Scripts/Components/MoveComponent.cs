using UnityEngine;

namespace ShootEmUp.Components
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbodyComponent;

        [SerializeField] private float speed = 5.0f;

        public void Move(Vector2 vector)
        {
            Vector2 nextPosition = rigidbodyComponent.position + vector * speed;
            rigidbodyComponent.MovePosition(nextPosition);
        }
    }
}