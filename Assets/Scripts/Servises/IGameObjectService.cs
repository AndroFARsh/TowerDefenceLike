
using UnityEngine;

namespace TowerDefenceLike
{
    public interface IGameObjectService
    {
        GameObject Borrow(string assetName);
        
        void Release(string assetName, GameObject gameObject);
    }
}