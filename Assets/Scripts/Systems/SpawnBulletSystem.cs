﻿using System.Collections.Generic;
using Entitas;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class SpawnBulletSystem : ReactiveSystem<GameEntity>
    {
        private readonly MetaContext m_metaContext;
        private readonly GameContext m_gameContext;

        public SpawnBulletSystem(Contexts contexts) : base(contexts.game)
        {
            m_metaContext = contexts.meta;
            m_gameContext = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Shoot);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasFollowTo
                && entity.hasSpawnBulletPoint;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var viewService = m_metaContext.viewService.value;
            entities.Slinq()
                .Where(entity => entity.hasFollowTo && entity.hasSpawnBulletPoint)
                .ForEach(entity =>
                {
                    var target = m_gameContext.GetEntityWithId(entity.followTo.value).position.value;
                    var source = entity.spawnBulletPoint.value;
                    
                    var newEntity = m_gameContext.CreateEntity();
                    newEntity.AddInitializePoint(source);
                    newEntity.AddDestinationPoint(target);
                    newEntity.AddFollowTo(entity.followTo.value);
                    newEntity.AddHit(entity.hit.value);
                    
                    viewService.Borrow(newEntity, "Towers/Bullet");

                    newEntity.updatePosition.value(source());
                });
        }
    }
}