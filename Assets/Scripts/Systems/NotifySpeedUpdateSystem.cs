using System.Collections.Generic;
using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class NotifySpeedUpdateSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> m_group;

        public NotifySpeedUpdateSystem(Contexts contexts) : base(contexts.game)
        {
            m_group = contexts.game.GetGroup(GameMatcher.SpeedListener);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Speed);
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
                    m_group.GetEntities().Slinq().Select(e => e.speedListener.value).Select(Tuple.Create, s))
                .ForEach(t => t.Item1(t.Item2));
        }
    }
}