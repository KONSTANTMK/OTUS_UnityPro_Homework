using System.Linq;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;
using Zenject;

namespace ShootEmUp.GameSystem
{
    public sealed class GameManagerInstaller : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameObject[] installObjects;
        private void Awake()
        {
            foreach (GameObject installObject in installObjects)
            {
                gameManager.AddListeners(installObject.GetComponents<IGameListener>().ToList());
                gameManager.AddListeners(installObject.GetComponentsInChildren<IGameListener>().ToList());
            }
        }
    }
}