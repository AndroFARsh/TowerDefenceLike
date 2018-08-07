using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class OptionDialogShowSystem:  ReactiveSystem<GameEntity> 
    {
        private readonly MetaContext m_metaContext;

        public OptionDialogShowSystem(Contexts contexts) : base(contexts.game)
        {
            m_metaContext = contexts.meta;
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
                .ForEach(entity => viewService.Borrow(entity, "OptionDialogHUD"));
        }
    }
}