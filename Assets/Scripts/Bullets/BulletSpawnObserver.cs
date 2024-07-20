using System.Linq;
using ShootEmUp.Common;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;

namespace ShootEmUp.Bullets
{
    public class BulletSpawnObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private BulletSpawner bulletSpawner;
        [SerializeField] private BulletDestroyer bulletDestroyer;
        [SerializeField] private Pool bulletPool;
        [SerializeField] private GameManager gameManager;

        void IGameStartListener.OnStartGame() => StartSubscribe();
        void IGameFinishListener.OnFinishGame() => StopSubscribe();
        
        private void StartSubscribe()
        {
            bulletSpawner.OnBulletSpawned += OnSpawned;
            bulletDestroyer.OnBulletDestroyed += OnDestroyed;
        }

        private void StopSubscribe()
        {
            bulletSpawner.OnBulletSpawned -= OnSpawned;
            bulletDestroyer.OnBulletDestroyed -= OnDestroyed;
        }

        private void OnSpawned(GameObject bullet)
        {
            gameManager.AddListeners(bullet.GetComponents<IGameListener>().ToList());
            bullet.GetComponent<Bullet>().OnCollisionEntered += bulletDestroyer.DestroyBullet;
            bullet.GetComponent<Bullet>().OnNeedDestroy += bulletDestroyer.DestroyBullet;
        }

        private void OnDestroyed(GameObject bullet)
        {
            gameManager.RemoveListeners(bullet.GetComponents<IGameListener>().ToList());
            bullet.GetComponent<Bullet>().OnCollisionEntered -= bulletDestroyer.DestroyBullet;
            bullet.GetComponent<Bullet>().OnNeedDestroy -= bulletDestroyer.DestroyBullet;
        }
    }
}