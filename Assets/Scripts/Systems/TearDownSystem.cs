using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class TearDownSystem : ITearDownSystem
    {
        private readonly Contexts m_contexts;

        public TearDownSystem(Contexts contexts)
        {
            m_contexts = contexts;
        }

        public void TearDown()
        {
            m_contexts.meta.GetEntities().Slinq().ForEach(entity => entity.Destroy());
            m_contexts.game.GetEntities().Slinq().ForEach(entity => entity.Destroy());
        }
    }
}