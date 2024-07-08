using System;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;

namespace ShootEmUp.GameSystem
{
    public sealed class InputManager : MonoBehaviour, IGameUpdateListener
    {
        public float HorizontalDirection { get; private set; }

        public event Action ShootKeyDown;
        
        void IGameUpdateListener.OnUpdate(float deltaTime) => HandleInput();

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootKeyDown?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                HorizontalDirection = 1;
            }
            else
            {
                HorizontalDirection = 0;
            }
        }
        
    }
}