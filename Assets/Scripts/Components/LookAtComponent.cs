using System;
using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class LookAtComponent : IComponent
    {
        public Action<Vector3> value;
    }
}