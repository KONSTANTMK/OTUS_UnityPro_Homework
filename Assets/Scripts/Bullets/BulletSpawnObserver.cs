using System.Linq;
using ShootEmUp.Common;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Bullets
{
    public class BulletSpawnObserver : GameListener , IGameStartListener, IGameFinishListener
    {
        private BulletSpawner bulletSpawner;
        private BulletDestroyer bulletDestroyer;
        
        [Inject]
        public void Construct(BulletSpawner bulletSpawner, BulletDestroyer bulletDestroyer)
        {
            this.bulletSpawner = bulletSpawner;
            this.bulletDestroyer = bulletDestroyer;
        }

        public void OnStartGame() => StartSubscribe();
        public void OnFinishGame() => StopSubscribe();
        
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
            bullet.GetComponent<Bullet>().OnCollisionEntered -= bulletDestroyer.DestroyBullet;
            bullet.GetComponent<Bullet>().OnNeedDestroy -= bulletDestroyer.DestroyBullet;
        }
    }
}