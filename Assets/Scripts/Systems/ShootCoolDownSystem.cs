using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;
using UnityEngine;
using Tuple = System.Tuple;

namespace TowerDefenceLike
{
    public class ShootCoolDownSystem : IExecuteSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public ShootCoolDownSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.Shotable);
        }

        public void Execute()
        {
            if (m_context.isPaused) return;

            m_group.GetEntities().Slinq()
                .Where(entity => entity.hasCooldown
                                 && entity.hasDelay)
                .ForEach(entity =>
                {
                    var delay = Mathf.Max(entity.delay.value - Time.deltaTime, 0.0f);
                    if (delay <= 0 && entity.hasFollowTo && entity.isAimed)
                    {
                        entity.isShoot = true;
                        delay = entity.cooldown.value;
                    }
                    entity.ReplaceDelay(delay);
                });
        }
    }
}