using System;
using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class RotationComponent : IComponent
    {
        public Func<Quaternion> value;
    }
}