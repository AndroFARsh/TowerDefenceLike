using System;
using Entitas;

namespace TowerDefenceLike
{
    [Game]
    public class HalthListenerComponent : IComponent
    {
        public Action<int, int> value;
    }
}