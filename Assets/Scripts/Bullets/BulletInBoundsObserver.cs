using ShootEmUp.Common;
using ShootEmUp.GameSystem.Listeners;
using Zenject;

namespace ShootEmUp.Bullets
{
    public class BulletInBoundsObserver : GameListener, IGameStartListener, IGameFinishListener
    {
        private BulletDestroyer bulletDestroyer;
        private BulletInBoundsCheacker bulletInBoundsCheacker;
        
        [Inject]
        public void Construct(BulletDestroyer bulletDestroyer, BulletInBoundsCheacker bulletInBoundsCheacker)
        {
            this.bulletDestroyer = bulletDestroyer;
            this.bulletInBoundsCheacker = bulletInBoundsCheacker;
        }
        
        public void OnStartGame() => StartSubscribe();
        public void OnFinishGame() => StopSubscribe();

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