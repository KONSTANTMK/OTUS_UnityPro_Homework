using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Common
{
    public class Pool : MonoBehaviour
    {
        public HashSet<GameObject> activeEntityes { get; } = new();
        public List<GameObject> cacheEntities { get; } = new();
        
        [SerializeField] private GameObject prefab;
        [SerializeField] private int startInstantiateCount;
        [SerializeField] private Transform poolContainer;

        private readonly Queue<GameObject> entityPool = new();

        private void Awake()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            this.cacheEntities.Clear();
            this.cacheEntities.AddRange(this.activeEntityes);
        }

        private void Initialize()
        {
            for (var i = 0; i < startInstantiateCount; i++)
            {
                var entity = Instantiate(this.prefab, this.poolContainer);
                this.entityPool.Enqueue(entity);
            }
        }

        public bool TryDequeue(out GameObject result)
        {
            return entityPool.TryDequeue(out result);
        }

        public void ReturnToPull(GameObject entity)
        {
            entity.transform.SetParent(this.poolContainer);
            entityPool.Enqueue(entity);
        }
    }
}