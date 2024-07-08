using ShootEmUp.GameSystem.Listeners;
using UnityEngine;

namespace ShootEmUp.UI.MainMenu 
{
    public class HUD : MonoBehaviour, IGameStartListener,IGamePauseListener,IGameResumeListener,IGameFinishListener
    {
        void IGameStartListener.OnStartGame()
        {
            gameObject.SetActive(true);
        }

        void IGamePauseListener.OnPauseGame()
        {
            gameObject.SetActive(false);
        }

        void IGameResumeListener.OnResumeGame()
        {
            gameObject.SetActive(false);
        }
        
        void IGameFinishListener.OnFinishGame()
        {
            gameObject.SetActive(false);
        }
    }
}