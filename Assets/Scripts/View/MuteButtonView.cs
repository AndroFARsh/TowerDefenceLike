using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(Button))]
	public class MuteButtonView : MonoBehaviour, IView
	{
		private GameContext m_context;
		private Button m_button;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			m_context = contexts.game;
			
			m_button = GetComponent<Button>();
			m_button.onClick.AddListener(OnButtonClicked);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
			m_context = null;
		}

		private void OnButtonClicked()
		{
			
		}
	}
}
