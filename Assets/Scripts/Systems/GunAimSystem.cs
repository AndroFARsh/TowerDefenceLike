﻿using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;
using UnityEngine;
using Tuple = System.Tuple;

namespace TowerDefenceLike
{
    public class GunAimSystem : IExecuteSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public GunAimSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.FollowTo);
        }

        public void Execute()
        {
            m_group.GetEntities().Slinq()
                .Where(entity => entity.hasRotation)
                .Select(entity => Tuple.Create(entity, m_context.GetEntityWithId(entity.followTo.value).ToOption()
                    .Where(e => e.hasPosition).Select(e => e.position.value()).ValueOr(Vector3.zero)))
                .ForEach(t => Aim(t.Item1, t.Item2));
        }

        private static void Aim(GameEntity entity, Vector3 target)
        {
            var from = entity.rotation.value();
            var to = LookRotation(entity.position.value(), target);

            entity.isAimed = Quaternion.Angle(from, to) < 10;
            entity.updateRotation.value(Quaternion.Slerp(from, to,
                Time.deltaTime * entity.speed.value));
        }

        private static Quaternion LookRotation(Vector3 from, Vector3 to)
        {
            if (Vector3.zero.Equals(to)) return Quaternion.identity;
            
            var forward = to - from;
            
            // rotate gun only in ground plane
            forward.y = 0;
            return Quaternion.LookRotation(forward);
        }
    }
}