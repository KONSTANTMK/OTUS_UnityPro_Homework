using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShootEmUp
{
    internal class CharacterInputObserver:MonoBehaviour
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