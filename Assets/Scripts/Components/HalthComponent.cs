using Entitas;

namespace TowerDefenceLike
{
    [Game]
    public class HalthComponent : IComponent
    {
        public int max;
        public int value;
    }
}