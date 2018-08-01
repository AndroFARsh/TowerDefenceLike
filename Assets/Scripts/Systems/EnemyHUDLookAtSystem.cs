using Entitas;
using Entitas.Unity;
using Smooth.Algebraics;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class EnemyHUDLookAtSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> m_group;
        private readonly GameContext m_context;

        public EnemyHUDLookAtSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = contexts.game.GetGroup(GameMatcher.Enemy);
        }

        public void Execute()
        {
            var cameraRotation = m_context.cameraEntity.rotation.value();
            m_group.GetEntities().Slinq()
                .Where(entity => entity.hasLookAt)
                .ForEach(entity => entity.updateRotation.value(cameraRotation));
        }
    }
}