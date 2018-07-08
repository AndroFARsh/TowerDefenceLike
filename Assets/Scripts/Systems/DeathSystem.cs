using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class DeathSystem : ReactiveSystem<GameEntity>
    {
        public DeathSystem(Contexts contexts) : base(contexts.game) {}

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Halth);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.halth.value <= 0;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            entities.Slinq().ForEach(e => e.isRelease = true);
        }
    }
}