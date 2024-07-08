using System.Collections.Generic;
using System.Linq;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;

namespace ShootEmUp.GameSystem
{
    public sealed class GameManagerInstaller : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        private void Awake()
        {
            gameManager.AddListeners(GetComponentsInChildren<IGameListener>().ToList()); ;
        }
    }
}