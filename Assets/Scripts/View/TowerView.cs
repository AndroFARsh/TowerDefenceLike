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

			if (entity.hasInitializePoint) transform.position = entity.initializePoint.value;
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}
		
		private void OnCollisionEnter(Collision other)
		{
			Debug.Log($"OnCollisionEnter: {other.transform.name}");
		}

		private void OnTriggerEnter(Collider other)
		{
			Debug.Log($"OnTriggerEnter: {other.name}");
		}
	}
}
