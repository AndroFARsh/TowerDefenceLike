using System.Collections.Generic;
using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;
using UnityEngine;


namespace TowerDefenceLike
{
    public class HitTrackSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_context;

        public HitTrackSystem(Contexts contexts) : base(contexts.game)
        {
            m_context = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Release);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasFollowTo
                   && entity.hasHit
                   && entity.isProjectile;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            entities.Slinq()
                .ForEach(shotableEntity => m_context.GetEntityWithId(shotableEntity.followTo.value)
                    .ToOption().Where(e => e.hasHalth)
                    .ForEach(e => e.ReplaceHalth(e.halth.max, e.halth.value - shotableEntity.hit.value)));
        }
    }
}