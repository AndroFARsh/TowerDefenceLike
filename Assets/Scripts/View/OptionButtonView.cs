using Entitas.Unity;
using Smooth.Algebraics;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(Button))]
	public class OptionButtonView : MonoBehaviour, IView
	{
		private Button m_button;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			m_button = GetComponent<Button>();
			m_button.onClick.AddListener(OnButtonClicked);
			entity.isPaused = true;
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}

		private void OnButtonClicked()
		{
			gameObject.GetEntityLink()
				.ToOption()
				.Select(link => link.entity as GameEntity)
				.ForEach(e =>
				{
					e.isPaused = !e.isPaused;
					
				});
		}
	}
}
