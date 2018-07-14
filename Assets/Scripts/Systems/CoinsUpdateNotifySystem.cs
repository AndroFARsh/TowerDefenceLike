using System.Collections.Generic;
using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class CoinsUpdateNotifySystem : ReactiveSystem<GameEntity>
    {
        public CoinsUpdateNotifySystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Coins);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasCoinsListener;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            entities.Slinq()
                .ForEach(entity => entity.coinsListener.value(entity.coins.value));
        }
    }
}