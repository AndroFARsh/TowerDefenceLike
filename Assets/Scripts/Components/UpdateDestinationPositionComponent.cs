using System;
using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class UpdateDestinaltionPositionComponent : IComponent
    {
        public Action<Vector3> value;
    }
}