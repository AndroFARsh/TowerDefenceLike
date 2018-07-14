using Entitas;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class CountdownTimerSystem : IExecuteSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public CountdownTimerSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.CountdownTimer);
        }

        public void Execute()
        {
            if (m_context.isPaused) return;

            m_group.GetEntities().Slinq()
                .Where(entity => entity.hasTimer && !entity.isRelease)
                .ForEach(entity =>
                {
                    var timer = entity.timer;
                    var delay = Mathf.Max(timer.value - Time.deltaTime, 0.0f);
                    
                    entity.ReplaceTimer(delay, timer.initValue);
                    if (delay == 0) entity.isRelease = true;
                });
        }
    }
}