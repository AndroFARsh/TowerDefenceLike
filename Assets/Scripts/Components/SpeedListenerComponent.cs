using System;
using Entitas;

namespace TowerDefenceLike
{
    [Game]
    public class SpeedListenerComponent : IComponent
    {
        public Action<SpeedFactor> value;
    }

}