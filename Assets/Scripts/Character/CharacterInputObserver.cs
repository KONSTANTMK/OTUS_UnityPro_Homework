using UnityEngine;
using ShootEmUp.GameSystem;
using UnityEngine.Serialization;

namespace ShootEmUp.Character
{
    internal class CharacterInputObserver : MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] private InputManager inputManager;
        private void OnEnable()
        {
            inputManager.ShootKeyDown += character.Shot;
        }

        private void OnDisable()
        {
            inputManager.ShootKeyDown -= character.Shot;
        }
    }
}