using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class SpawnEnemySystem : ReactiveSystem<GameEntity>
    {
        private readonly MetaContext m_metaContext;
        private readonly GameContext m_gameContext;

        public SpawnEnemySystem(Contexts contexts) : base(contexts.game)
        {
            m_metaContext = contexts.meta;
            m_gameContext = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.EnemyAssetName);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var target = m_gameContext.targetPointEntity.position.value;
            var viewService = m_metaContext.viewService.value;

            entities.Slinq()
                .ForEach(entity =>
                {
                    var newEnemyEntty = m_gameContext.CreateEntity();
                    viewService.Borrow(newEnemyEntty, entity.enemyAssetName.value);

                    if (newEnemyEntty.hasUpdatePosition)
                        newEnemyEntty.updatePosition.value(entity.initializePoint.value());
                    
                    if (newEnemyEntty.hasUpdateDestinaltionPosition)
                        newEnemyEntty.updateDestinaltionPosition.value(target());
                });
        }
    }
}