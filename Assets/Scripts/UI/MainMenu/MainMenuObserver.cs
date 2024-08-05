using ShootEmUp.GameSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp.UI
{
    public sealed class MainMenuObserver : MonoBehaviour

    {
        [SerializeField] private MainMenu mainMenu;
        private GameManager gameManager;
        
        [Inject]
        public void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        private void Awake()
        {
            mainMenu.StartButton.GetComponent<Button>().onClick.AddListener(mainMenu.ReadyStartGame);
            mainMenu.ResumeButton.GetComponent<Button>().onClick.AddListener(gameManager.ResumeGame);
        }
    }
}