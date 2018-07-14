using System;
using Entitas;

namespace TowerDefenceLike
{
    [Game]
    public class CoinsListenerComponent : IComponent
    {
        public Action<int> value;
    }

}