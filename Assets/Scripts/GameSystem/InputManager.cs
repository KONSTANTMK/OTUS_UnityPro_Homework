using System;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;
using Zenject;

namespace ShootEmUp.GameSystem
{
    public sealed class InputManager : IGameUpdateListener, IGameStartListener
    {
        public float HorizontalDirection { get; private set; }
        private GameManager gameManager;

        public event Action ShootKeyDown;
        
        [Inject]
        public void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.gameManager.AddListener(this);
        }
        
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

        public void OnStartGame()
        {
            Debug.Log("Работает");
        }
    }
}