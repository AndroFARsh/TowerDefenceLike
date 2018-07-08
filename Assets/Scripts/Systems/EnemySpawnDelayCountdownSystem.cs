using Entitas;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class EnemySpawnDelayCountdownSystem : IExecuteSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public EnemySpawnDelayCountdownSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.SpawnEnemyInfo);
        }

        public void Execute()
        {
            if (m_context.isPaused) return;

            m_group.GetEntities().Slinq()
                .ForEach(e =>
                {
                    var info = e.spawnEnemyInfo.value;
                    info.delay -= Time.deltaTime;

                    if (info.delay <= 0)
                    {
                        var newEntity = m_context.CreateEntity();
                        newEntity.AddEnemyAssetName(info.assetName);
                        newEntity.AddInitializePoint(e.initializePoint.value);
                        e.Destroy();
                    }
                    else
                        e.ReplaceSpawnEnemyInfo(info);
                });
        }
    }
}