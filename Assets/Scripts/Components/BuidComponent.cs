using System;
using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class BuildComponent : IComponent
    {
        public string assetName;
        public Func<Vector3> position;
    }
}