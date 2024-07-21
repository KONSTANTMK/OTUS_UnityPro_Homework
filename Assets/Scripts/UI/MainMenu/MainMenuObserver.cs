using ShootEmUp.GameSystem;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.UI
{
    public sealed class MainMenuObserver : MonoBehaviour

    {
        [SerializeField] private MainMenu mainMenu;
        [SerializeField] private GameManager gameManager;

        private void Awake()
        {
            mainMenu.StartButton.GetComponent<Button>().onClick.AddListener(mainMenu.ReadyStartGame);
            mainMenu.ResumeButton.GetComponent<Button>().onClick.AddListener(gameManager.ResumeGame);
        }
    }
}