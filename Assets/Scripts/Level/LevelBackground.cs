using System;
using UnityEngine;

namespace ShootEmUp.Level
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField] private float startPositionY;
        [SerializeField] private float endPositionY;
        [SerializeField] private float movingSpeedY;

        private float positionX;

        private float positionZ;

        private Transform selfTransform;

        private void Awake()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Initialize()
        {
            selfTransform = transform;
            var position = selfTransform.position;
            positionX = position.x;
            positionZ = position.z;
        }

        private void Move()
        {
            Vector3 position;
            if (selfTransform.position.y <= endPositionY)
            {
                position = new Vector3(positionX, startPositionY, positionZ);
                selfTransform.position = position;
            }

            position = new Vector3(positionX, movingSpeedY * Time.fixedDeltaTime, positionZ);
            selfTransform.position -= position;
        }
    }
}