using System;
using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class SpawnBulletPointComponent : IComponent
    {
        public Func<Vector3> value;
    }
}