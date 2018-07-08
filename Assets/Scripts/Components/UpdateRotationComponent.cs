using System;
using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class UpdateRotationComponent : IComponent
    {
        public Action<Quaternion> value;
    }
}