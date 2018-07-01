using Entitas;
using Entitas.CodeGeneration.Attributes;
using TowerDefenceLike;

namespace TowerDefenceLike
{
    [Meta, Unique]
    public class ViewServiceComponent : IComponent
    {
        public IViewService value;
    }
}