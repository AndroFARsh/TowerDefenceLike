using Entitas;
using Entitas.CodeGeneration.Attributes;
using TowerDefenceLike;

namespace TowerDefenceLike
{
    [Meta, Unique]
    public class GameObjecterviceComponent : IComponent
    {
        public IGameObjectService value;
    }
}