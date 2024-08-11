using System;
using UnityEngine;

namespace ShootEmUp.Enemy
{
    public sealed class WorldTransform : MonoBehaviour
    {
        private Transform worldTransform;
        public Transform GetTransfrom => worldTransform;

        private void Awake() => worldTransform = gameObject.transform;
    }
}