using System;
using Entitas;

namespace TowerDefenceLike
{
    [Game]
    public class SpeedFactorListenerComponent : IComponent
    {
        public Action<SpeedFactor> value;
    }

}