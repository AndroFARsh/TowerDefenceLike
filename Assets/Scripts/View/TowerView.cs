using UnityEngine;

namespace TowerDefenceLike
{
	public class TowerView : MonoBehaviour, IView
	{
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			entity.isTower = true;
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
