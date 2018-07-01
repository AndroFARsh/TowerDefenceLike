using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class ReleaseSystem : ReactiveSystem<GameEntity>
    {
        private readonly MetaContext metaContext;

        public ReleaseSystem(Contexts contexts) : base(contexts.game)
        {
            metaContext = contexts.meta;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Release);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var viewService = metaContext.viewService.value;
            entities.Slinq().ForEach(e => viewService.Release(e));
        }
    }
}