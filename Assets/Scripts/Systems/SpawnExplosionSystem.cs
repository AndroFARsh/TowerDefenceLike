using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class SpawnExplosionSystem : ReactiveSystem<GameEntity>
    {
        private readonly MetaContext m_metaContext;
        private readonly GameContext m_gameContext;

        public SpawnExplosionSystem(Contexts contexts) : base(contexts.game)
        {
            m_metaContext = contexts.meta;
            m_gameContext = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Release);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isProjectile && entity.hasPosition;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var viewService = m_metaContext.viewService.value;
            entities.Slinq()
                .ForEach(entity =>
                {
                    var newEntity = m_gameContext.CreateEntity();
                    newEntity.AddInitializePoint(entity.position.value);
                    viewService.Borrow(newEntity, "Explosion");
                });
        }
    }
}