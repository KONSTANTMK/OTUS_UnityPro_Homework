using UnityEngine;
using ShootEmUp.Components;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;
using Zenject;

namespace ShootEmUp.Character
{
    internal class PlayerDeathObserver:MonoBehaviour,IGameStartListener, IGameFinishListener
    {
        private GameManager gameManager;
        [SerializeField] private HitPointsComponent hitPointsComponent;
        
        [Inject]
        public void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }
        
        public void OnStartGame() => StartSubscribe();
        public void OnFinishGame() => StopSubscribe();
        
        private void StartSubscribe()
        {
            hitPointsComponent.HpEmpty += gameManager.FinishGame;
        }

        private void StopSubscribe()
        {
            hitPointsComponent.HpEmpty -= gameManager.FinishGame;
        }
    }
}
