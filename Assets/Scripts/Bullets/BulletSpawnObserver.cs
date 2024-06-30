using UnityEngine;

namespace ShootEmUp.Bullets
{
    public class BulletSpawnObserver : MonoBehaviour
    {
        [SerializeField] private BulletSpawner bulletSpawner;
        [SerializeField] private BulletUnspawner bulletUnspawner;

        private void OnEnable()
        {
            bulletSpawner.OnBulletSpawned += this.OnSpawn;
            bulletUnspawner.OnBulletUnspawned += this.OnUnspawn;
        }

        private void OnDisable()
        {
            bulletSpawner.OnBulletSpawned -= this.OnSpawn;
            bulletUnspawner.OnBulletUnspawned -= this.OnUnspawn;
        }

        private void OnSpawn(GameObject bullet)
        {
            bullet.GetComponent<Bullet>().OnCollisionEntered += bulletUnspawner.UnspawnBullet;
        }

        private void OnUnspawn(GameObject bullet)
        {
            bullet.GetComponent<Bullet>().OnCollisionEntered -= bulletUnspawner.UnspawnBullet;
        }
    }
}