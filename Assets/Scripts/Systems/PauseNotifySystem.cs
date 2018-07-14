using System.Collections.Generic;
using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class PauseNotifySystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> m_group;

        public PauseNotifySystem(Contexts contexts) : base(contexts.game)
        {
            m_group = contexts.game.GetGroup(GameMatcher.PauseListener);
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
                .SelectMany(paused => m_group.GetEntities().Slinq().Select(Tuple.Create, paused))
                .ForEach(t => t.Item1.pauseListener.value(t.Item2));
        }
    }
}