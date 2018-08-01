using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class OptionDialogShowSystem:  ReactiveSystem<GameEntity> 
    {
        private MetaContext m_metaContext;
        private GameContext m_gameContext;

        public OptionDialogShowSystem(Contexts contexts) : base(contexts.game)
        {
            m_metaContext = contexts.meta;
            m_gameContext = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.OptionDialogShowEvent);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var viewService = m_metaContext.viewService.value;
            entities.Slinq()
                .ForEach(entity => viewService.Borrow(entity, ""));
        }
    }
}