using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace TowerDefenceLike
{
    [Game]
    public struct IdComponent : IComponent
    {
        [PrimaryEntityIndex]
        public int value;
    }
}