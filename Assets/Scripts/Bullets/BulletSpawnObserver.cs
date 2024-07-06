using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp.Bullets
{
    public class BulletSpawnObserver : MonoBehaviour
    {
        [SerializeField] private BulletSpawner bulletSpawner;
        [SerializeField] private BulletDestroyer bulletDestroyer;

        private void OnEnable()
        {
            bulletSpawner.OnBulletSpawned += OnSpawned;
            bulletDestroyer.OnBulletDestroyed += OnDestroyed;
        }

        private void OnDisable()
        {
            bulletSpawner.OnBulletSpawned -= OnSpawned;
            bulletDestroyer.OnBulletDestroyed -= OnDestroyed;
        }

        private void OnSpawned(GameObject bullet)
        {
            bullet.GetComponent<Bullet>().OnCollisionEntered += bulletDestroyer.DestroyBullet;
        }

        private void OnDestroyed(GameObject bullet)
        {
            bullet.GetComponent<Bullet>().OnCollisionEntered -= bulletDestroyer.DestroyBullet;
        }
    }
}