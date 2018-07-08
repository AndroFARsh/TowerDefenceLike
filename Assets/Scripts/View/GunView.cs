using Entitas;
using Entitas.Unity;
using Smooth.Algebraics;
using UnityEngine;

namespace TowerDefenceLike
{
    [RequireComponent(typeof(Collider))]
    public class GunView : MonoBehaviour, IView
    {
        [SerializeField] private int m_hit = 1;
        [SerializeField] private float m_cooldown = 2;
        
        private Collider m_collider;

        public void DestroyView(GameEntity entity, Contexts contexts)
        {
        }

        public void InitializeView(GameEntity entity, Contexts contexts)
        {
            m_collider = GetComponent<Collider>();

            entity.isGun = true;
            entity.isShotable = true;
            entity.AddHit(m_hit);
            entity.AddCooldown(m_cooldown);
            entity.AddRotation(() => transform.rotation);
            entity.AddPosition(() => transform.position);

            entity.AddSetParrent((parent, worldPositionStays) =>
            {
                m_collider.enabled = false;
                transform.SetParent(parent, worldPositionStays);
                m_collider.enabled = true;
            });
            entity.AddUpdateRotation(quternion => transform.rotation = quternion);

            if (entity.hasInitializePoint) transform.position += entity.initializePoint.value;
        }

        private void OnTriggerStay(Collider other)
        {
            other.gameObject.GetEntityLink().ToOption().Select(link => link.entity as GameEntity)
                .Where(e => e.isEnemy && e.hasId)
                .SelectMany(otherEntity => gameObject.GetEntityLink().ToOption()
                    .Select(link => link.entity as GameEntity)
                    .Where(e => !e.hasFollowTo)
                    .Select(Tuple.Create, otherEntity))
                .ForEach(t => t.Item1.AddFollowTo(t.Item2.id.value));
        }

        private void OnTriggerExit(Collider other)
        {
            other.gameObject.GetEntityLink().ToOption().Select(link => link.entity as GameEntity)
                .Where(e => e.isEnemy && e.hasId)
                .SelectMany(otherEntity => gameObject.GetEntityLink().ToOption()
                    .Select(link => link.entity as GameEntity)
                    .Where(e => e.hasFollowTo && e.followTo.value == otherEntity.id.value)
                    .Select(Tuple.Create, otherEntity))
                .ForEach(t => t.Item1.RemoveFollowTo());
        }
    }
}