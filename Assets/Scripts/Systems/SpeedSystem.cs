using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class SpeedSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public SpeedSystem(Contexts contexts) : base(contexts.game)
        {
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
                .ForEach(scale => Time.timeScale = (float) scale);
        }
    }
}