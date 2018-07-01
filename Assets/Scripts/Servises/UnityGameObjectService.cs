using System.Collections.Generic;
using Entitas;
using Smooth.Pools;
using UnityEditor;
using UnityEngine;

namespace TowerDefenceLike
{
    public class UnityGameObjectService : IGameObjectService
    {
        private const string Prefabs = "Prefabs/";

        private readonly Dictionary<string, Stack<GameObject>> pools = new Dictionary<string, Stack<GameObject>>();
            
        public GameObject Borrow(string assetName) {
            lock (pools) {
                if (!pools.ContainsKey(assetName))
                    pools[assetName] = new Stack<GameObject>();
                
                var pool = pools[assetName];    
                return pool.Count > 0 ? pool.Pop() : Create(assetName);
            }
        }

        public void Release(string assetName, GameObject gameObject)
        {
            lock (pools)
            {
                if (!pools.ContainsKey(assetName))
                    pools[assetName] = new Stack<GameObject>();

                var pool = pools[assetName];    
                pool.Push(gameObject);
            }
        }

        private static GameObject Create(string assetName)
        { 
            return GameObject.Instantiate(Resources.Load<GameObject>(Prefabs + assetName));
        }
    }
}