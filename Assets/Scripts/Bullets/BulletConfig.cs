using ShootEmUp.GameSystem.Data;
using UnityEngine;

namespace ShootEmUp.Bullets
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    {
        [SerializeField]
        public PhysicsLayer physicsLayer;
        [SerializeField]
        public Color color;
        [SerializeField]
        public int damage;
        [SerializeField]
        public float speed;
    }
}