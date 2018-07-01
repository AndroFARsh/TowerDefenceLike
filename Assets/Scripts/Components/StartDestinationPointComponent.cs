using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class StartDestinationPointComponent : IComponent
    {
        public Vector3 startPosition;
        public Vector3 destinationPosition;
    }
}