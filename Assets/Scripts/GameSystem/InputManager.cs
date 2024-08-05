using System;
using ShootEmUp.Common;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;

namespace ShootEmUp.GameSystem
{
    public sealed class InputManager : GameListener, IGameUpdateListener 
    {
        public float HorizontalDirection { get; private set; }

        public event Action ShootKeyDown;
        
        public void OnUpdate(float deltaTime) => HandleInput();

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