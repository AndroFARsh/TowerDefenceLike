using System;
using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class PositionComponent : IComponent
    {
        public Func<Vector3> value;
    }
}