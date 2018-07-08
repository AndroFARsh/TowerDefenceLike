using System;
using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class UpdatePositionComponent : IComponent
    {
        public Action<Vector3> value;
    }
}