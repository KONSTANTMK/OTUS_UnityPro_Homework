using System.Collections.Generic;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Common
{
    public class Pool : GameListener, IGameFixedUpdateListener,IInitializable
    {
        public HashSet<GameObject> ActiveEntities { get; } = new();
        public List<GameObject> CacheEntities { get; } = new();
        
        private int startInstantiateCount;
        
        private GameObject prefab;
        
        private Transform poolContainer;

        private readonly Queue<GameObject> entityPool = new();

        private DiContainer container;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            this.container = container;
        }
        
        public void Initialize()
        {
            Fill();
        }
        
        public void OnFixedUpdate(float deltaTime)
        {
            CacheEntities.Clear();
            CacheEntities.AddRange(ActiveEntities);
        }
        
        private void Fill()
        {
            for (var i = 0; i < startInstantiateCount; i++)
            {
                var entity = container.InstantiatePrefab(prefab, poolContainer);
                entityPool.Enqueue(entity);
            }
        }

        public bool TryDequeue(out GameObject result)
        {
            return entityPool.TryDequeue(out result);
        }

        public void ReturnToPull(GameObject entity)
        {
            entity.transform.SetParent(poolContainer);
            entityPool.Enqueue(entity);
        }

        public void SetPrefab(GameObject prefab)
        {
            this.prefab = prefab;
        }
        
        public void SetStartInstantiateCount(int startInstantiateCount)
        {
            this.startInstantiateCount = startInstantiateCount;
        }
        
        public void SetPoolContainer(Transform poolContainer)
        {
            this.poolContainer = poolContainer;
        }
    }
}