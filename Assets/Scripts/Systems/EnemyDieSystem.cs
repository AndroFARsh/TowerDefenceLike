using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;

namespace TowerDefenceLike
{
    public class EnemyDieSystem : ReactiveSystem<GameEntity>
    {
        public EnemyDieSystem(Contexts contexts) : base(contexts.game) {}

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Halth);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isEnemy && 
                   entity.halth.value <= 0 && 
                   entity.hasDieListener;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            entities.Slinq().ForEach(e =>
            {
                e.isDieing = true;
                e.isCountdownTimer = true;
                e.AddTimer(2, 2);
                e.dieListener.value(e);
            });
        }
    }
}