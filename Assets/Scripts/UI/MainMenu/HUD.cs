using ShootEmUp.GameSystem.Listeners;
using UnityEngine;

namespace ShootEmUp.UI
{
    public class HUD : MonoBehaviour, IGameStartListener,IGamePauseListener,IGameResumeListener,IGameFinishListener
    {
        [SerializeField] private GameObject pauseButton;
        public GameObject PauseButton => pauseButton;
        public void OnStartGame()
        {
            pauseButton.SetActive(true);
            gameObject.SetActive(true);
        }

        public void OnPauseGame()
        {
            gameObject.SetActive(false);
        }

        public void OnResumeGame()
        {
            gameObject.SetActive(true);
        }
        
        public void OnFinishGame()
        {
            gameObject.SetActive(false);
        }
    }
}