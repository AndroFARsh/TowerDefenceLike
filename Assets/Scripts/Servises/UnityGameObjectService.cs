using System.Collections.Generic;
using UnityEngine;

namespace TowerDefenceLike
{
    public class UnityGameObjectService : IGameObjectService
    {
        private const string Prefabs = "Prefabs/";

        private readonly Dictionary<string, Stack<GameObject>> m_pools = new Dictionary<string, Stack<GameObject>>();
            
        public GameObject Borrow(string assetName) {
            lock (m_pools)
            {
                var pool = !m_pools.ContainsKey(assetName)
                    ? m_pools[assetName] = new Stack<GameObject>()
                    : m_pools[assetName];    
                var gameObject = pool.Count > 0 ? 
                       pool.Pop() : 
                       Create(assetName);
                gameObject.SetActiveRecursively(true);
                return gameObject;
            }
        }

        public void Release(string assetName, GameObject gameObject)
        {
            lock (m_pools)
            {
                gameObject.SetActiveRecursively(false);
                (!m_pools.ContainsKey(assetName) 
                    ? m_pools[assetName] = new Stack<GameObject>() 
                    : m_pools[assetName]).Push(gameObject);
            }
        }

        private static GameObject Create(string assetName)
        { 
            return Object.Instantiate(Resources.Load<GameObject>(Prefabs + assetName));
        }
    }
}