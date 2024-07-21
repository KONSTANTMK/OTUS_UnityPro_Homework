using System;
using UnityEngine;
using ShootEmUp.GameSystem.Listeners;

namespace ShootEmUp.Enemy
{
    public sealed class Enemy : MonoBehaviour, IGameFinishListener
    {
        public event Action<GameObject> OnNeedDestroy; 
        public void OnFinishGame() => OnNeedDestroy?.Invoke(gameObject);
        
    }
}