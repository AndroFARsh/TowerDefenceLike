using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
	public class EnemyHUDView : MonoBehaviour, IView
	{
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			entity.AddLookAt(worldPosition => transform.LookAt(worldPosition));
			entity.AddUpdateRotation(rotation => transform.rotation = rotation);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}
	}
}
