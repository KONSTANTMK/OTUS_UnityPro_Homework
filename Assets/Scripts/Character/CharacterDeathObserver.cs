using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShootEmUp
{
    internal class CharacterDeathObserver:MonoBehaviour
    {
        [SerializeField] private GameObject character;
        [SerializeField] private GameManager gameManager;

        private void OnEnable()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty += this.gameManager.FinishGame;
        }

        private void OnDisable()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty -= this.gameManager.FinishGame;
        }
    }
}
