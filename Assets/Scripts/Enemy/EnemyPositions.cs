using UnityEngine;

namespace ShootEmUp.Enemy
{
    public sealed class EnemyPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPositions;
        [SerializeField] private Transform[] attackPositions;
        public Transform RandomSpawnPosition() => RandomTransform(spawnPositions);
        public Transform RandomAttackPosition() => RandomTransform(attackPositions);

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}