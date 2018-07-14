using Entitas.Unity;
using Smooth.Algebraics;
using UnityEngine;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(Collider))]
	public class BulletView : MonoBehaviour, IView
	{
		[SerializeField] private float m_speed = 10f;
		[SerializeField] private float m_lifeTime = 2.0f;
		
		private Collider m_collider;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			m_collider = GetComponent<Collider>();

			entity.isBullet = true;
			entity.isCountdownTimer = true;
			entity.AddTimer(m_lifeTime, m_lifeTime);
			entity.AddSpeed(m_speed);
			entity.AddPosition(() => transform.position);
			entity.AddUpdatePosition(position => transform.position = position);
			entity.AddSetParrent((parent, worldPositionStays) =>
			{
				m_collider.enabled = false;
				transform.SetParent(parent, worldPositionStays);
				m_collider.enabled = true;
			});
			
			gameObject.SetActiveRecursively(true);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
			gameObject.SetActiveRecursively(false);
		}
		
		private void OnTriggerEnter(Collider other)
		{
			gameObject.GetEntityLink()
				.ToOption()
				.Select(link => link.entity as GameEntity)
				.Where(entity => entity.isBullet)
				.ForEach(entity => entity.isRelease = true);
		}
	}
}