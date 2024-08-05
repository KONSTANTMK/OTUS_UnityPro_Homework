using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;
using Zenject;

namespace ShootEmUp.Common
{
    public abstract class ListenableObject : IGameListener
    {
        private GameManager gameManager;
        
        [Inject]
        private void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.gameManager.AddListener(this);
        }
    }
}