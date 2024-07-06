using UnityEngine;

namespace ShootEmUp.Components
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;

        [SerializeField] private float speed = 5.0f;

        public void Move(Vector2 vector)
        {
            Vector2 nextPosition = rigidbody2D.position + vector * speed;
            rigidbody2D.MovePosition(nextPosition);
        }
    }
}