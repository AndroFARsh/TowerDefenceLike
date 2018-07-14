using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class ShootCleanupSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> m_group;

        public ShootCleanupSystem(Contexts contexts) 
        {
            m_group = contexts.game.GetGroup(GameMatcher.Shoot);
        }

        public void Cleanup()
        {
            m_group.GetEntities().Slinq().ForEach(e => e.isShoot = false);
        }
    }
}