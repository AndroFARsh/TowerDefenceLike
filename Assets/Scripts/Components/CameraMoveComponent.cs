using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class CameraMoveConfigComponent : IComponent
    {
        public float panSpeed;
        public float panBorrderThikness;
        public float zoomSpeed;
        public Vector2 panLimitX;
        public Vector2 panLimitY;
        public Vector2 panLimitZ;
    }
}