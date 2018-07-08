using UnityEngine;

namespace TowerDefenceLike
{
	public class PlatformView : MonoBehaviour, IView
	{
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			gameObject.SetActiveRecursively(true);

			entity.isPlatform = true;
			entity.isSelecteble = true;
			entity.AddPosition(() => transform.position);
			entity.AddSetParrent(transform.SetParent);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
			gameObject.SetActiveRecursively(false);
		}
	}
}
