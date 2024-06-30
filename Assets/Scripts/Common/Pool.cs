using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.Common
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;

        [SerializeField] private int startInstantiateCount;
        private readonly Queue<GameObject> entityPool = new();

        [SerializeField] private Transform poolContainer;
        public HashSet<GameObject> activeEntityes { get; } = new();
        public List<GameObject> cacheEntities { get; } = new();
        
        private void Awake()
        {
            FillPoolOnstart();
        }

        private void FixedUpdate()
        {
            this.cacheEntities.Clear();
            this.cacheEntities.AddRange(this.activeEntityes);
        }

        public void FillPoolOnstart()
        {
            for (var i = 0; i < startInstantiateCount; i++)
            {
                var entity = Instantiate(this.prefab, this.poolContainer);
                this.entityPool.Enqueue(entity);
            }
        }

        public Queue<GameObject> GetPool()
        {
            return entityPool;
        }

        public void ReturnToPull(GameObject entity)
        {
            entity.transform.SetParent(this.poolContainer);
            entityPool.Enqueue(entity);
        }
    }
}