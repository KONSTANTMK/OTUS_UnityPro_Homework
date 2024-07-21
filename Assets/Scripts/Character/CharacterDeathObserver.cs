using UnityEngine;
using ShootEmUp.Components;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;

namespace ShootEmUp.Character
{
    internal class CharacterDeathObserver:MonoBehaviour,IGameStartListener, IGameFinishListener
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private HitPointsComponent hitPointsComponent;
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
