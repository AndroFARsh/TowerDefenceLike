using System;
using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class SetParrentComponent: IComponent
    {
        public Action<Transform, bool> value;
    }
}