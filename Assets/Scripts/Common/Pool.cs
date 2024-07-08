using System.Collections.Generic;
using ShootEmUp.GameSystem.Listeners;
using UnityEngine;

namespace ShootEmUp.Common
{
    public class Pool : MonoBehaviour, IGameFixedUpdateListener
    {
        public HashSet<GameObject> ActiveEntityes { get; } = new();
        public List<GameObject> CacheEntities { get; } = new();
        
        [SerializeField] private GameObject prefab;
        [SerializeField] private int startInstantiateCount;
        [SerializeField] private Transform poolContainer;

        private readonly Queue<GameObject> entityPool = new();

        private void Awake()
        {
            Initialize();
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float deltaTime)
        {
            CacheEntities.Clear();
            CacheEntities.AddRange(ActiveEntityes);
        }

        private void Initialize()
        {
            for (var i = 0; i < startInstantiateCount; i++)
            {
                var entity = Instantiate(prefab, poolContainer);
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
    }
}