using UnityEngine;

namespace ShootEmUp.Bullets
{
    public class BulletInBoundsObserver : MonoBehaviour
    {
        [SerializeField] private BulletDestroyer bulletDestroyer;
        [SerializeField] private BulletInBoundsCheacker bulletInBoundsCheacker;

        private void OnEnable()
        {
            bulletInBoundsCheacker.OnBulletOutBounds += bulletDestroyer.DestroyBullet;
        }

        private void OnDisable()
        {
            bulletInBoundsCheacker.OnBulletOutBounds -= bulletDestroyer.DestroyBullet;
        }
    }
}