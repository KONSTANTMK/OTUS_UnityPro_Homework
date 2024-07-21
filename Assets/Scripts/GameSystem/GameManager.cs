using System.Collections.Generic;
using ShootEmUp.GameSystem.Data;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;

namespace ShootEmUp.GameSystem
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private GameState state;

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
            if (state != GameState.PLAYING)
            {
                return;
            }
            
            var deltaTime = Time.fixedDeltaTime;
            for (int i = 0, count = fixedUpdateListeners.Count; i < count; i++)
            {
                var listener = fixedUpdateListeners[i];
                listener.OnFixedUpdate(deltaTime);
            }
        }
        
        private void LateUpdate()
        {
            if (state != GameState.PLAYING)
            {
                return;
            }
            
            var deltaTime = Time.deltaTime;
            for (int i = 0, count = lateUpdateListeners.Count; i < count; i++)
            {
                var listener = lateUpdateListeners[i];
                listener.OnLateUpdate(deltaTime);
            }
        }
        
        public void AddListener(IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }
            
            listeners.Add(listener);

            if (listener is IGameUpdateListener updateListener)
            {
                updateListeners.Add(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                fixedUpdateListeners.Add(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                lateUpdateListeners.Add(lateUpdateListener);
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
                AddListener(listener);
            }
        }
        
        public void RemoveListener(IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }
            
            listeners.Remove(listener);

            if (listener is IGameUpdateListener updateListener)
            {
                updateListeners.Remove(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                fixedUpdateListeners.Remove(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                lateUpdateListeners.Remove(lateUpdateListener);
            }
        }

        public void RemoveListeners(List<IGameListener> listeners)
        {
            if (listeners == null)
            {
                return;
            }

            foreach (var listener in listeners)
            {
                RemoveListener(listener);
            }
        }
        
        public void StartGame()
        {
            foreach (var listener in listeners)
            {
                if (listener is IGameStartListener startListener)
                {
                    startListener.OnStartGame();
                }
            }

            state = GameState.PLAYING;
        }
        
        public void PauseGame()
        {
            foreach (var listener in listeners)
            {
                if (listener is IGamePauseListener pauseListener)
                {
                    pauseListener.OnPauseGame();
                }
            }
            
            state = GameState.PAUSED;
        }
        
        public void ResumeGame()
        {
            foreach (var listener in listeners)
            {
                if (listener is IGameResumeListener resumeListener)
                {
                    resumeListener.OnResumeGame();
                }
            }
            
            state = GameState.PLAYING;
        }
        
        public void FinishGame(GameObject _)
        {
            foreach (var listener in listeners)
            {
                if (listener is IGameFinishListener finishListener)
                {
                    finishListener.OnFinishGame();
                }
            }
            state = GameState.FINISHED;
        }
    }
}