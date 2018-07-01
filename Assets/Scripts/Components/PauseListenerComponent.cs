using System;
using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class PauseListenerComponent : IComponent
    {
        public Action<bool> value;
    }
}