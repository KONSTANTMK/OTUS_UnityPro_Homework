using UnityEngine;
using ShootEmUp.Components;
using ShootEmUp.Managers;

namespace ShootEmUp.Character
{
    internal class CharacterDeathObserver:MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private HitPointsComponent hitPointsComponent;

        private void OnEnable()
        {
            this.GetComponent<HitPointsComponent>().hpEmpty += this.gameManager.FinishGame;
        }

        private void OnDisable()
        {
            this.GetComponent<HitPointsComponent>().hpEmpty -= this.gameManager.FinishGame;
        }
    }
}
