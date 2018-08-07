using System;
using Entitas;

namespace TowerDefenceLike
{
    [Game]
    public class MuteUpdateComponent : IComponent
    {
        public Action<bool> value;
    }
}