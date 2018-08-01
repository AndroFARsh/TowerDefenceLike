using Entitas;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class SwimSystem : IExecuteSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public SwimSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.Swim);
        }

        public void Execute()
        {
            m_group.GetEntities().Slinq()
                .Where(enity => enity.hasUpdatePosition && 
                                enity.hasUpdateRotation)
                .ForEach(Anim);
        }

        private static void Anim(GameEntity entity)
        {
            entity.updatePosition.value(new Vector3
            {
                x = NewValue(entity.swim.initPosition.x, entity.swim.x),
                y = NewValue(entity.swim.initPosition.y, entity.swim.y),
                z = NewValue(entity.swim.initPosition.z, entity.swim.z)
            });

            entity.updateRotation.value(Quaternion.Euler(
                NewValue(entity.swim.initAngles.x, entity.swim.roll),
                NewValue(entity.swim.initAngles.y, entity.swim.pitch),
                NewValue(entity.swim.initAngles.z, entity.swim.yaw)));
        }

        private static float NewValue(float value, Prop prop)
        {
            return value + Mathf.Sin(Time.time * Time.timeScale * prop.Frequnce) * prop.Amplitude;
        }
    }
}