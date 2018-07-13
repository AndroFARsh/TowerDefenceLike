using Entitas;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class MoveBulletSystem : IExecuteSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public MoveBulletSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.Bullet);
        }

        public void Execute()
        {
            if (m_context.isPaused) return;

            m_group.GetEntities().Slinq()
                .ForEach(e => e.updatePosition.value(
                    e.position.value() + (e.destinationPoint.value() - e.initializePoint.value()).normalized *
                    Time.deltaTime * e.speed.value));
        }
    }
}