using UnityEngine;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;

namespace ShootEmUp.Character
{
    internal class CharacterInputObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private Character character;
        [SerializeField] private InputManager inputManager;
        
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