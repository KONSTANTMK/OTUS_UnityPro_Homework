using System.Linq;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp.Bullets
{
    public class BulletSpawnObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private BulletSpawner bulletSpawner;
        [SerializeField] private BulletDestroyer bulletDestroyer;
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
            bullet.GetComponent<Bullet>().OnCollisionEntered += bulletDestroyer.DestroyBullet;
            gameManager.AddListeners(bullet.GetComponents<IGameListener>().ToList());
        }

        private void OnDestroyed(GameObject bullet)
        {
            bullet.GetComponent<Bullet>().OnCollisionEntered -= bulletDestroyer.DestroyBullet;
            gameManager.RemoveListeners(bullet.GetComponents<IGameListener>().ToList());
        }
    }
}