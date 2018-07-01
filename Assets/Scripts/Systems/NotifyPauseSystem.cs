using System.Collections.Generic;
using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class NotifyPauseSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> @group;

        public NotifyPauseSystem(Contexts contexts) : base(contexts.game)
        {
            group = contexts.game.GetGroup(GameMatcher.PauseListener);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Paused, GroupEvent.AddedOrRemoved));
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            entities.Slinq()
                .Select(e => e.isPaused)
                .SelectMany(paused => @group.GetEntities().Slinq().Select(Tuple.Create, paused))
                .ForEach(t => t.Item1.pauseListener.value(t.Item2));
        }
    }
}