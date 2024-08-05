using UnityEngine;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;
using Zenject;

namespace ShootEmUp.Character
{
    internal class CharacterInputObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private Character character;
        private InputManager inputManager;
        
        [Inject]
        public void Construct(InputManager inputManager)
        {
            this.inputManager = inputManager;
        }
        
        public void OnStartGame() => StartSubscribe();
        public void OnFinishGame() => StopSubscribe();
        
        private void StartSubscribe()
        {
            inputManager.ShootKeyDown += character.Shot;
        }

        private void StopSubscribe()
        {
            inputManager.ShootKeyDown -= character.Shot;
        }
    }
}