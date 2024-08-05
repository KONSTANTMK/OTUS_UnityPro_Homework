using ShootEmUp.GameSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp.UI
{
    public sealed class HUDObserver : MonoBehaviour

    {
        [SerializeField] private HUD hud;
        private GameManager gameManager;
        
        [Inject]
        public void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        private void Awake()
        {
            hud.PauseButton.GetComponent<Button>().onClick.AddListener(gameManager.PauseGame);
        }
    }
}