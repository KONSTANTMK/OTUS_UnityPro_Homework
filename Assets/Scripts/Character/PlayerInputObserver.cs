using UnityEngine;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine.Serialization;
using Zenject;

namespace ShootEmUp.Character
{
    internal class PlayerInputObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        private Player player;
        private InputManager inputManager;
        
        [Inject]
        public void Construct(Player player,InputManager inputManager)
        {
            this.player = player;
            this.inputManager = inputManager;
        }
        
        public void OnStartGame() => StartSubscribe();
        public void OnFinishGame() => StopSubscribe();
        
        private void StartSubscribe()
        {
            inputManager.ShootKeyDown += player.Shot;
        }

        private void StopSubscribe()
        {
            inputManager.ShootKeyDown -= player.Shot;
        }
    }
}