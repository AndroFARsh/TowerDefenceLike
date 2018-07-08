using System.Collections.Generic;
using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class NotifyHalthUpdateSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_context;

        public NotifyHalthUpdateSystem(Contexts contexts) : base(contexts.game)
        {
            m_context = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Halth);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            entities.Slinq()
                .Where(entity => entity.hasId)
                .ForEach(entity => m_context.GetEntityWithId(entity.id.value)
                    .ToOption()
                    .Where(e => e.hasHalthListener)
                    .Select(e => e.halthListener.value)
                    .ForEach(halthListener => halthListener(entity.halth.max, entity.halth.value)));
        }
    }
}