using UnityEngine;

namespace TowerDefenceLike
{
	public class SpawnBulletView : MonoBehaviour, IView
	{
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			entity.AddSpawnBulletPoint(() => transform.position);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}
	}
}