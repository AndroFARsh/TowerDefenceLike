using System;
using Entitas;

namespace TowerDefenceLike
{
    [Game]
    public struct OnSelectComponent: IComponent
    {
        public Action value;
    }
}