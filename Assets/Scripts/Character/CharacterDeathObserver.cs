using UnityEngine;
using ShootEmUp.Components;
using ShootEmUp.GameSystem;

namespace ShootEmUp.Character
{
    internal class CharacterDeathObserver:MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private HitPointsComponent hitPointsComponent;

        private void OnEnable()
        {
            GetComponent<HitPointsComponent>().HpEmpty += gameManager.FinishGame;
        }

        private void OnDisable()
        {
            GetComponent<HitPointsComponent>().HpEmpty -= gameManager.FinishGame;
        }
    }
}
