using Entitas.Unity;
using Smooth.Algebraics;
using UnityEngine;

namespace TowerDefenceLike
{
	public class TargetView : MonoBehaviour, IView
	{
		[SerializeField] private int halth = 25;
		
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			entity.isTargetPoint = true;
			entity.AddPosition(() => transform.position);
			entity.AddHalth(halth, halth);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}
		
		private void OnTriggerEnter(Collider other)
		{
			other.gameObject.GetEntityLink()
				.ToOption()
				.Select(link => link.entity as GameEntity)
				.ForEach(entity => entity.isRelease = true);
		}
	}
}
