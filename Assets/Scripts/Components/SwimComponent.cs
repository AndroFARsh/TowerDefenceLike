using Entitas;
using UnityEngine;

namespace TowerDefenceLike
{
    [Game]
    public class SwimComponent : IComponent
    {
        public Prop x;
        public Prop y;
        public Prop z;

        public Prop roll; // x
        public Prop pitch; // y
        public Prop yaw; // z

        public Vector3 initPosition;
        public Vector3 initAngles;
    }
}