using System;
using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class InitializePointComponent : IComponent
    {
        public Func<Vector3> value;
    }
}