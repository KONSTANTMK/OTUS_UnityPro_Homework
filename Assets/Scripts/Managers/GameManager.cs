using UnityEngine;

namespace ShootEmUp.Managers
{
    public sealed class GameManager : MonoBehaviour
    {
        public void FinishGame(GameObject player)
        {
            Destroy(player);
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}