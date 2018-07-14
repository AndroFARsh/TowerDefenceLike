using Entitas;

namespace TowerDefenceLike
{
    public class LevelInitializeSystem : IInitializeSystem
    {
        private readonly GameContext m_gameContext;
        private readonly MetaContext m_metaContext;

        public LevelInitializeSystem(Contexts contexts)
        {
            m_metaContext = contexts.meta;
            m_gameContext = contexts.game;
        }

        public void Initialize()
        {
            var viewService = m_metaContext.viewService.value;
            viewService.Borrow(m_gameContext.CreateEntity(), Constants.Spawn);
            viewService.Borrow(m_gameContext.CreateEntity(), Constants.Target);
        }
    }
}