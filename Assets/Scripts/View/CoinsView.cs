using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(Text))]
	public class CoinsView : MonoBehaviour, IView
	{
		private Text m_amount;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			m_amount = GetComponent<Text>();
			entity.AddCoinsListener(amount => m_amount.text = amount.ToString());
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}
	}
}
