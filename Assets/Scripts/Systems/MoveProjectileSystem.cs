using Entitas;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class MoveProjectileSystem : IExecuteSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public MoveProjectileSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.Projectile);
        }

        public void Execute()
        {
            if (m_context.isPaused) return;

            m_group.GetEntities().Slinq()
                .Where(e => e.hasDestinationPoint && 
                            e.hasInitializePoint && 
                            e.hasUpdatePosition && 
                            e.hasUpdateRotation)
                .ForEach(e =>
                {
                    var direction = (e.destinationPoint.value() - e.initializePoint.value()).normalized;
                    e.updateRotation.value(Quaternion.LookRotation(direction));
                        
                    e.updatePosition.value(e.position.value() + direction *Time.deltaTime * e.speed.value);
                });
        }
    }
}