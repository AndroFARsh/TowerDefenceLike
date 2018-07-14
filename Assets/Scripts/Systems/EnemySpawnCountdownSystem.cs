using Entitas;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class EnemySpawnCountdownSystem : IExecuteSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public EnemySpawnCountdownSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.SpawnEnemyInfo);
        }

        public void Execute()
        {
            if (m_context.isPaused) return;

            m_group.GetEntities().Slinq()
                .ForEach(entity =>
                {
                    var info = entity.spawnEnemyInfo.value;
                    
                    info.delay -= Time.deltaTime;
                    if (info.delay <= 0)
                    {
                        var newEntity = m_context.CreateEntity();
                        newEntity.AddEnemyAssetName(info.assetName);
                        newEntity.AddInitializePoint(entity.initializePoint.value);
                        entity.Destroy();
                    }
                    else
                        entity.ReplaceSpawnEnemyInfo(info);
                });
        }
    }
}