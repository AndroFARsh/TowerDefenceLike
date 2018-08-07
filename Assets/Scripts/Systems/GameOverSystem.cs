using System.Collections.Generic;
using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class GameOwerSystem : ReactiveSystem<GameEntity>
    {
        private readonly MetaContext m_metaContext;
        
        public GameOwerSystem(Contexts contexts) : base(contexts.game)
        {
            m_metaContext = contexts.meta;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Halth);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.halth.value == 0 && entity.isTargetPoint;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var viewService = m_metaContext.viewService.value;
            entities.Slinq()
                .ForEach(entity => viewService.Borrow(entity, "GameOwerDialogHUD"));
        }
    }
}