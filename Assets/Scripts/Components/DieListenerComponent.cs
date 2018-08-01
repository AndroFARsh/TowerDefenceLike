using System;
using Entitas;

namespace TowerDefenceLike
{
    [Game]
    public class DieListenerComponent : IComponent
    {
        public Action<GameEntity> value;
    }
}