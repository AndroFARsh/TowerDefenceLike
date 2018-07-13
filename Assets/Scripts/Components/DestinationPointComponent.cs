using System;
using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class DestinationPointComponent : IComponent
    {
        public Func<Vector3> value;
    }
}