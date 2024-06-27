using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShootEmUp
{
    internal class BulletCollisionEnteredObserver:MonoBehaviour
    {
        [SerializeField] public BulletSystem bulletSystem { get; set; }

        private void OnEnable()
        {
            this.GetComponent<Bullet>().OnCollisionEntered += BulletDamageController.DealDamage;
            
        }

        private void OnDisable()
        {
            this.GetComponent<Bullet>().OnCollisionEntered -= BulletDamageController.DealDamage;
        }
    }
}