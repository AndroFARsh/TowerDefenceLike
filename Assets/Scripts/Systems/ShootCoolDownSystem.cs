using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;
using UnityEngine;
using Tuple = System.Tuple;

namespace TowerDefenceLike
{
    public class ShootCoolDownSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public ShootCoolDownSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.Shotable);
        }

        public void Initialize()
        {
            m_group.GetEntities().Slinq()
                .ForEach(entity => entity.AddDelay(0));
        }

        public void Execute()
        {
            if (m_context.isPaused) return;

            m_group.GetEntities().Slinq()
                .Where(entity => entity.hasFollowTo
                                 && entity.hasCooldown
                                 && entity.hasDelay)
                .ForEach(entity =>
                {
                    var delay = entity.delay.value - Time.deltaTime;
                    if (delay <= 0)
                    {
                        entity.isShoot = true;
                        delay = entity.cooldown.value;
                    }
                    entity.ReplaceDelay(delay);
                });
        }
    }
}