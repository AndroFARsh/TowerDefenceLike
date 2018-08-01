using UnityEngine;

namespace TowerDefenceLike
{
	public class ExplosionView : MonoBehaviour, IView
	{
		private static readonly int Die = Animator.StringToHash("Hide");
		
		[SerializeField] private float m_lifeTime = 2.0f;
		
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			entity.isExplosion = true;
			entity.isCountdownTimer = true;
			entity.AddTimer(m_lifeTime, m_lifeTime);
			
			if (entity.hasInitializePoint) transform.position = entity.initializePoint.value();
		
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}
		
	}
}