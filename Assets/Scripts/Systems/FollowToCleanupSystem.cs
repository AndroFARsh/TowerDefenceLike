using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class FollowToCleanupSystem : ICleanupSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public FollowToCleanupSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = contexts.game.GetGroup(GameMatcher.FollowTo);
        }

        public void Cleanup()
        {
            m_group.GetEntities().Slinq()
                .Where(e =>
                {
                    var followTo = m_context.GetEntityWithId(e.followTo.value);
                    return followTo == null || followTo.isDieing;
                })
                .ForEach(e => e.RemoveFollowTo());
        }
    }
}