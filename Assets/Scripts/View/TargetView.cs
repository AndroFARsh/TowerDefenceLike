using Entitas.Unity;
using Smooth.Algebraics;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
	public class TargetView : MonoBehaviour, IView
	{
		[SerializeField] private int m_halth = 25;
		[SerializeField] private int m_coins = 20;
		[SerializeField] private GameObject m_hudPref;
		
		private GameObject m_hud;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			entity.isTargetPoint = true;
			entity.AddCoins(m_coins);
			entity.AddHalth(m_halth, m_halth);
			entity.AddPosition(() => transform.position);

			m_hud = Instantiate(m_hudPref, transform);
			m_hud.GetComponentsInChildren<IView>()
				.Slinq()
				.ForEach(view => view.InitializeView(entity, contexts));
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
			m_hud.GetComponentsInChildren<IView>()
				.Slinq()
				.ForEach(view => view.DestroyView(entity, contexts));
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
