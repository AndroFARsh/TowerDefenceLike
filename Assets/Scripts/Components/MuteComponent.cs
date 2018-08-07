using System;
using Entitas;

namespace TowerDefenceLike
{
    [Game]
    public class MuteComponent : IComponent
    {
        public Func<bool> value;
    }
}
