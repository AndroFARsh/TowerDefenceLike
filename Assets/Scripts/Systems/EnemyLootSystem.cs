using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class EnemyLootSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_context;

        public EnemyLootSystem(Contexts contexts) : base(contexts.game)
        {
            m_context = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Halth);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isEnemy &&
                   entity.hasCoins &&
                   entity.halth.value <= 0;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var target = m_context.targetPointEntity;
            target.ReplaceCoins(entities.Slinq()
                .Aggregate(target.coins.value, (coins, e) => coins + e.coins.value));
        }
    }
}