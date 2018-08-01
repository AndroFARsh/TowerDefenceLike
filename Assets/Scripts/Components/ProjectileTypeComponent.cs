using System;
using Entitas;

namespace TowerDefenceLike
{
    public enum ProjectileType
    {
        Arrow,
        Cannonball,
        Lightning
    }

    [Game]
    public class ProjectileTypeComponent : IComponent
    {
        public ProjectileType value;

        public string name => Enum.GetName(typeof(ProjectileType), value);
    }
}