using Entitas.Unity;
using Smooth.Algebraics;
using UnityEngine;

namespace TowerDefenceLike
{
	public class TargetView : MonoBehaviour, IView
	{
		[SerializeField] private int m_halth = 25;
		[SerializeField] private int m_coins = 20;
		
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			entity.isTargetPoint = true;
			entity.AddCoins(m_coins);
			entity.AddHalth(m_halth, m_halth);
			entity.AddPosition(() => transform.position);
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
