using Cysharp.Threading.Tasks;
using ShootEmUp.GameSystem;
using ShootEmUp.GameSystem.Listeners;
using TMPro;
using UnityEngine;

namespace ShootEmUp.UI
{
    public class MainMenu : MonoBehaviour, IGameStartListener,IGamePauseListener,IGameResumeListener,IGameFinishListener
    {
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject resumeButton;
        [SerializeField] private GameObject timer;
        [SerializeField] private GameManager gameManager;
        
        public GameObject StartButton => startButton;
        public GameObject ResumeButton => resumeButton;
        public void OnStartGame()
        {
            gameObject.SetActive(false);
        }

        public void OnPauseGame()
        {
            startButton.SetActive(false);
            resumeButton.SetActive(true);
            gameObject.SetActive(true);
        }

        public void OnResumeGame()
        {
            gameObject.SetActive(false);
        }
        
        public void OnFinishGame()
        {
            startButton.SetActive(true);
            resumeButton.SetActive(false);
            gameObject.SetActive(true);
        }

        public async void ReadyStartGame()
        {
            startButton.SetActive(false);
            timer.SetActive(true);
            for (int i = 3; i > 0; i--)
            {
                timer.GetComponent<TextMeshProUGUI>().text = i.ToString();
                await UniTask.WaitForSeconds(1);
            }
            timer.SetActive(false);
            gameManager.StartGame();
        }
    }
}