using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class NotifyHalthUpdateSystem : ReactiveSystem<GameEntity>
    {
        private IGroup<GameEntity> group;

        public NotifyHalthUpdateSystem(Contexts contexts) : base(contexts.game)
        {
            group = contexts.game.GetGroup(GameMatcher.HalthListener);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Halth);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            entities.Slinq()
                .Select(e => e.halth)
                .ForEach(halth => group.GetEntities().Slinq()
                    .Select(e => e.halthListener.value)
                    .ForEach(halthListener => halthListener(halth.max, halth.value)));
        }
    }
}