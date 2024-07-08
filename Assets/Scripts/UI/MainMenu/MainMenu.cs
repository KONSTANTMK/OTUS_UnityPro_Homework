using ShootEmUp.GameSystem.Listeners;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.UI.MainMenu 
{
    public class MainMenu : MonoBehaviour, IGameStartListener,IGamePauseListener,IGameResumeListener,IGameFinishListener
    {
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject resumeButton;
        void IGameStartListener.OnStartGame()
        {
            gameObject.SetActive(false);
        }

        void IGamePauseListener.OnPauseGame()
        {
            startButton.SetActive(false);
            resumeButton.SetActive(true);
            gameObject.SetActive(true);
        }

        void IGameResumeListener.OnResumeGame()
        {
            gameObject.SetActive(false);
        }
        
        void IGameFinishListener.OnFinishGame()
        {
            startButton.SetActive(true);
            resumeButton.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}