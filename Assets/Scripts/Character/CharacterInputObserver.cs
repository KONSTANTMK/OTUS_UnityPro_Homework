using UnityEngine;
using ShootEmUp.Managers;

namespace ShootEmUp.Character
{
    internal class CharacterInputObserver : MonoBehaviour
    {
        private void OnEnable()
        {
            this.GetComponent<InputManager>().shootKeyDown += this.GetComponent<Character>().Shot;
        }

        private void OnDisable()
        {
            this.GetComponent<InputManager>().shootKeyDown -= this.GetComponent<Character>().Shot;
        }
    }
}