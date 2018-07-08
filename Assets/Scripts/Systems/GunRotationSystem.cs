using Entitas;
using Smooth.Algebraics;
using Smooth.Slinq;
using UnityEngine;
using Tuple = System.Tuple;

namespace TowerDefenceLike
{
    public class GunRotationSystem : IExecuteSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public GunRotationSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.FollowTo);
        }

        public void Execute()
        {
            m_group.GetEntities().Slinq()
                .Select(entity => Tuple.Create(entity, m_context.GetEntityWithId(entity.followTo.value).ToOption()
                    .Where(e => e.hasPosition).Select(e => e.position.value()).ValueOr(Vector3.zero)))
                .ForEach(t => t.Item1.updateRotation.value(Quaternion.Slerp(t.Item1.rotation.value(),
                    LookRotation(t.Item1.position.value(), t.Item2),
                    Time.deltaTime * 10)));
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