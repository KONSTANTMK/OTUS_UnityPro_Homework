using System;
using UnityEngine;

namespace ShootEmUp.Managers
{
    public sealed class InputManager : MonoBehaviour
    {
        public float HorizontalDirection { get; private set; }

        public event Action ShootKeyDown;
        
        private void Update()
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