using UnityEngine;
using ShootEmUp.Managers;

namespace ShootEmUp.Character
{
    internal class CharacterInputObserver : MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] private InputManager inputManager;
        private void OnEnable()
        {
            inputManager.shootKeyDown += character.Shot;
        }

        private void OnDisable()
        {
            inputManager.shootKeyDown -= character.Shot;
        }
    }
}