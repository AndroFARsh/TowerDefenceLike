using System.Collections.Generic;
using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class ShootSystem : ReactiveSystem<GameEntity>, ICleanupSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public ShootSystem(Contexts contexts) : base(contexts.game)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.Shoot);
        }


        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Shoot);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasFollowTo
                   && entity.hasPosition
                   && entity.hasHit;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            entities.Slinq()
                .ForEach(shotableEntity => m_context.GetEntityWithId(shotableEntity.followTo.value).ToOption()
                    .Where(e => e.hasHalth)
                    .ForEach(e => e.ReplaceHalth(e.halth.value - shotableEntity.hit.value, e.halth.value)));
        }

        public void Cleanup()
        {
            m_group.GetEntities().Slinq().ForEach(e => e.isShoot = false);
        }
    }
}