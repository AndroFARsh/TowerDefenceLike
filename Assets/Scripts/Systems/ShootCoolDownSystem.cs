﻿using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;
using UnityEngine;
using Tuple = System.Tuple;

namespace TowerDefenceLike
{
    public class ShootCooldownSystem : IExecuteSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public ShootCooldownSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.Shotable);
        }

        public void Execute()
        {
            if (m_context.isPaused) return;

            m_group.GetEntities().Slinq()
                .Where(entity => entity.hasTimer)
                .ForEach(entity =>
                {
                    var timer = entity.timer;
                    var delay = Mathf.Max(timer.value - Time.deltaTime, 0.0f);
                    if (delay <= 0 && entity.hasFollowTo && entity.isAimed)
                    {
                        entity.isShoot = true;
                        delay = timer.initValue;
                    }
                    entity.ReplaceTimer(delay, timer.initValue);
                });
        }
    }
}