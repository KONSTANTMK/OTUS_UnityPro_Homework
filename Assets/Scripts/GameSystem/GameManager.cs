using System.Collections.Generic;
using ShootEmUp.GameSystem.Data;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;

namespace ShootEmUp.GameSystem
{
    public sealed class GameManager : MonoBehaviour
    {
        public GameState State => state;
        [SerializeField]
        private GameState state;

        private readonly List<IGameListener> listeners = new();
        private readonly List<IGameUpdateListener> updateListeners = new();
        private readonly List<IGameFixedUpdateListener> fixedUpdateListeners = new();
        private readonly List<IGameLateUpdateListener> lateUpdateListeners = new();
        
        private void Update()
        {
            if (state != GameState.PLAYING)
            {
                return;
            }
            var deltaTime = Time.deltaTime;
            for (int i = 0, count = updateListeners.Count; i < count; i++)
            {
                var listener = updateListeners[i];
                listener.OnUpdate(deltaTime);
            }
        }
        
        private void FixedUpdate()
        {
            if (this.state != GameState.PLAYING)
            {
                return;
            }
            
            var deltaTime = Time.fixedDeltaTime;
            for (int i = 0, count = this.fixedUpdateListeners.Count; i < count; i++)
            {
                var listener = this.fixedUpdateListeners[i];
                listener.OnFixedUpdate(deltaTime);
            }
        }
        
        private void LateUpdate()
        {
            if (this.state != GameState.PLAYING)
            {
                return;
            }
            
            var deltaTime = Time.deltaTime;
            for (int i = 0, count = this.lateUpdateListeners.Count; i < count; i++)
            {
                var listener = this.lateUpdateListeners[i];
                listener.OnLateUpdate(deltaTime);
            }
        }
        
        public void AddListener(IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }
            
            this.listeners.Add(listener);

            if (listener is IGameUpdateListener updateListener)
            {
                this.updateListeners.Add(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                this.fixedUpdateListeners.Add(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                this.lateUpdateListeners.Add(lateUpdateListener);
            }
        }
        
        public void AddListeners(List<IGameListener> listeners)
        {
            if (listeners == null)
            {
                return;
            }

            foreach (var listener in listeners)
            {
                this.listeners.Add(listener);
                if (listener is IGameUpdateListener updateListener)
                {
                    this.updateListeners.Add(updateListener);
                }

                if (listener is IGameFixedUpdateListener fixedUpdateListener)
                {
                    this.fixedUpdateListeners.Add(fixedUpdateListener);
                }

                if (listener is IGameLateUpdateListener lateUpdateListener)
                {
                    this.lateUpdateListeners.Add(lateUpdateListener);
                }
            }
        }
        
        public void RemoveListener(IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }
            
            this.listeners.Remove(listener);

            if (listener is IGameUpdateListener updateListener)
            {
                this.updateListeners.Remove(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                this.fixedUpdateListeners.Remove(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                this.lateUpdateListeners.Remove(lateUpdateListener);
            }
        }
        
        public void StartGame()
        {
            foreach (var listener in this.listeners)
            {
                if (listener is IGameStartListener startListener)
                {
                    startListener.OnStartGame();
                }
            }

            this.state = GameState.PLAYING;
        }
        
        public void PauseGame()
        {
            foreach (var listener in this.listeners)
            {
                if (listener is IGamePauseListener pauseListener)
                {
                    pauseListener.OnPauseGame();
                }
            }
            
            this.state = GameState.PAUSED;
        }
        
        public void ResumeGame()
        {
            foreach (var listener in this.listeners)
            {
                if (listener is IGameResumeListener resumeListener)
                {
                    resumeListener.OnResumeGame();
                }
            }
            
            this.state = GameState.PLAYING;
        }
        
        public void FinishGame(GameObject _)
        {
            foreach (var listener in this.listeners)
            {
                if (listener is IGameFinishListener finishListener)
                {
                    finishListener.OnFinishGame();
                }
            }
            this.state = GameState.FINISHED;
        }
    }
}