using UnityEngine;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine.Serialization;

namespace ShootEmUp.Character
{
    internal class CharacterInputObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private Character character;
        [SerializeField] private InputManager inputManager;
        
        void IGameStartListener.OnStartGame() => StartSubscribe();
        void IGameFinishListener.OnFinishGame() => StopSubscribe();
        
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