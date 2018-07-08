using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class BuildComponent : IComponent
    {
        public string assetName;
        public Vector3 position;
    }
}