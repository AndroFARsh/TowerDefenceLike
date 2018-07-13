using Entitas;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class ExplosionLifeTimeSystem : IExecuteSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public ExplosionLifeTimeSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.Explosion);
        }

        public void Execute()
        {
            if (m_context.isPaused) return;

            m_group.GetEntities().Slinq()
                .Where(e => e.hasDelay && !e.isRelease)
                .ForEach(e =>
                {
                    var delay = Mathf.Max(e.delay.value - Time.deltaTime, 0.0f);
                    
                    e.ReplaceDelay(delay);
                    if (delay == 0) e.isRelease = true;
                });
        }
    }
}