using ShootEmUp.GameSystem;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.UI
{
    public sealed class HUDObserver : MonoBehaviour

    {
        [SerializeField] private HUD hud;
        [SerializeField] private GameManager gameManager;

        private void Awake()
        {
            hud.PauseButton.GetComponent<Button>().onClick.AddListener(gameManager.PauseGame);
        }
    }
}