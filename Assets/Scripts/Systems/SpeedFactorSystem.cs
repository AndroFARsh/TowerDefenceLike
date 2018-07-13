using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class SpeedFactorSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public SpeedFactorSystem(Contexts contexts) : base(contexts.game)
        {
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
                .ForEach(scale => Time.timeScale = (float) scale);
        }
    }
}