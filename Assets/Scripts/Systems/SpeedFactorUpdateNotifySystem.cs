using System.Collections.Generic;
using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class SpeedFactorUpdateNotifySystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> m_group;

        public SpeedFactorUpdateNotifySystem(Contexts contexts) : base(contexts.game)
        {
            m_group = contexts.game.GetGroup(GameMatcher.SpeedFactorListener);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.SpeedFactor);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            entities.Slinq()
                .Select(e => e.speedFactor.value)
                .SelectMany(s =>
                    m_group.GetEntities().Slinq().Select(e => e.speedFactorListener.value).Select(Tuple.Create, s))
                .ForEach(t => t.Item1(t.Item2));
        }
    }
}