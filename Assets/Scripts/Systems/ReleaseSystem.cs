using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class ReleaseSystem : ICleanupSystem
    {
        private readonly MetaContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public ReleaseSystem(Contexts contexts)
        {
            m_context = contexts.meta;
            m_group = contexts.game.GetGroup(GameMatcher.Release);
        }

        public void Cleanup()
        {
            var viewService = m_context.viewService.value;
            m_group.GetEntities().Slinq().ForEach(e => viewService.Release(e));
        }
    }
}