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
        void IGameStartListener.OnStartGame() => StartSubscribe();
        void IGameFinishListener.OnFinishGame() => StopSubscribe();
        
        private void StartSubscribe()
        {
            GetComponent<HitPointsComponent>().HpEmpty += gameManager.FinishGame;
        }

        private void StopSubscribe()
        {
            GetComponent<HitPointsComponent>().HpEmpty -= gameManager.FinishGame;
        }
    }
}
