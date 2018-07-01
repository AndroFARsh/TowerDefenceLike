using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class GameObjectComponent : IComponent
    {
        public string assetName;
        public GameObject go;
    }
}