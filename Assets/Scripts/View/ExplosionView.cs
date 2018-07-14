using UnityEngine;

namespace TowerDefenceLike
{
	public class ExplosionView : MonoBehaviour, IView
	{
		[SerializeField] private float m_lifeTime = 2.0f;
		
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			entity.isExplosion = true;
			entity.isCountdownTimer = true;
			entity.AddTimer(m_lifeTime, m_lifeTime);
			
			if (entity.hasInitializePoint) transform.position = entity.initializePoint.value();
			
			gameObject.SetActiveRecursively(true);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
			gameObject.SetActiveRecursively(false);
		}
		
	}
}