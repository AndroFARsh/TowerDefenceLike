using System;
using Entitas;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class SpawnEnemySystem : IExecuteSystem
    {
        private readonly MetaContext metaContext;
        private readonly GameContext gameContext;
        private readonly IGroup<GameEntity> group;

        public SpawnEnemySystem(Contexts contexts)
        {
            metaContext = contexts.meta;
            gameContext = contexts.game;
            group = gameContext.GetGroup(GameMatcher.Wave);
        }

        public void Execute()
        {
            if (gameContext.isPaused) return;

            var target = gameContext.targetPointEntity.position.value;
            var viewService = metaContext.viewService.value;

            group.GetEntities()
                .Slinq()
                .ForEach(e => Spawn(e, target, viewService));
        }

        private void Spawn(GameEntity entity, Func<Vector3> target, IViewService viewService)
        {
            var delay = entity.delay.value - Time.deltaTime;
            if (delay > 0)
            {
                entity.ReplaceDelay(delay);
                return;
            }

            var chainId = entity.chainId.value;
            var enemyId = entity.enemyId.value + 1;

            var wave = entity.wave.value;
            if (wave.Count <= chainId) return;

            var chain = wave.Cheins[chainId];
            if (chain.Number >= enemyId)
            {
                var newEnemyEntty = gameContext.CreateEntity();
                newEnemyEntty.AddName($"{chain.AssetName}_{chainId}_{enemyId}");
                newEnemyEntty.AddStartDestinationPoint(entity.position.value(), target());
                viewService.Borrow(newEnemyEntty, chain.AssetName);
                entity.ReplaceEnemyId(enemyId);
                entity.ReplaceDelay(chain.Delay);
            }
            else
            {
                entity.ReplaceChainId(chainId + 1);
                entity.ReplaceDelay(wave.Delay);
            }
        }
    }
}