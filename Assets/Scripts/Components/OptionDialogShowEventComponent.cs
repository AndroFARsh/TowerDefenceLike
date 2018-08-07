using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace TowerDefenceLike
{
    [Unique, Event(EventTarget.Any)]
    public class OptionDialogShowEventComponent : IComponent
    {
    }
}