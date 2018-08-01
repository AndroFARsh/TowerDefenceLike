using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class SpawnTowerSystem : ReactiveSystem<GameEntity>
    {
        private readonly MetaContext m_metaContext;
        private readonly GameContext m_gameContext;

        public SpawnTowerSystem(Contexts contexts) : base(contexts.game)
        {
            m_metaContext = contexts.meta;
            m_gameContext = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Build);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var viewService = m_metaContext.viewService.value;
            entities.Slinq()
                .ForEach(entity =>
                {
                    var newTowerEntity = m_gameContext.CreateEntity();
                    newTowerEntity.AddInitializePoint(entity.build.position);
                    viewService.Borrow(newTowerEntity, entity.build.assetName);
                    
                    entity.Destroy();
                });
        }
    }
}