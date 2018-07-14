using UnityEngine;

namespace TowerDefenceLike
{
	public class TowerView : MonoBehaviour, IView
	{
		[SerializeField] private int m_coins = 10;
		
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			entity.isTower = true;
			entity.AddCoins(m_coins);
			entity.AddRotation(() => transform.rotation);
			entity.AddPosition(() => transform.position);
			entity.AddSetParrent(transform.SetParent);

			if (entity.hasInitializePoint) transform.position = entity.initializePoint.value();
			
			gameObject.SetActiveRecursively(true);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
			gameObject.SetActiveRecursively(false);
		}
	}
}
