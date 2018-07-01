using Entitas;

namespace TowerDefenceLike
{
    public class InitializeLevelSystem : IInitializeSystem
    {
        private readonly GameContext gameContext;
        private readonly MetaContext metaContext;

        public InitializeLevelSystem(Contexts contexts)
        {
            metaContext = contexts.meta;
            gameContext = contexts.game;
        }

        public void Initialize()
        {
            var viewService = metaContext.viewService.value;
            viewService.Borrow(gameContext.CreateEntity(), Constants.Spawn);
            viewService.Borrow(gameContext.CreateEntity(), Constants.Target);
        }
    }
}