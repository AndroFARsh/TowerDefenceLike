using System.Collections.Generic;
using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class NotifyHalthUpdateSystem : ReactiveSystem<GameEntity>
    {
        public NotifyHalthUpdateSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Halth);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasHalthListener;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            entities.Slinq()
                .ForEach(entity => entity.halthListener.value(entity.halth.max, entity.halth.value));
        }
    }
}