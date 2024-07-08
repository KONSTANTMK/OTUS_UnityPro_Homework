using ShootEmUp.GameSystem.Listeners;
using UnityEngine;

namespace ShootEmUp.Bullets
{
    public class BulletInBoundsObserver : MonoBehaviour,IGameStartListener, IGameFinishListener
    {
        [SerializeField] private BulletDestroyer bulletDestroyer;
        [SerializeField] private BulletInBoundsCheacker bulletInBoundsCheacker;
        
        void IGameStartListener.OnStartGame() => StartSubscribe();
        void IGameFinishListener.OnFinishGame() => StopSubscribe();

        private void StartSubscribe()
        {
            bulletInBoundsCheacker.OnBulletOutBounds += bulletDestroyer.DestroyBullet;
        }

        private void StopSubscribe()
        {
            bulletInBoundsCheacker.OnBulletOutBounds -= bulletDestroyer.DestroyBullet;
        }
    }
}