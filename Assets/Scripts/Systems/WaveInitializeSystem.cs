using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class InitializeWaveSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_context;

        public InitializeWaveSystem(Contexts contexts) : base(contexts.game)
        {
            m_context = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Wave);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasPosition;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            entities.Slinq()
                .ForEach(e => e.wave.value.ToSpawnChainList().Slinq()
                    .ForEach(info =>
                    {
                        var entity = m_context.CreateEntity();
                        entity.AddSpawnEnemyInfo(info);
                        entity.AddInitializePoint(e.position.value);
                    }));
        }
    }
}