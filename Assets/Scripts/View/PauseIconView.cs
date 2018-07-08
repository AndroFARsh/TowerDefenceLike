using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(Image))]
	public class PauseIconView : MonoBehaviour, IView
	{
		[SerializeField] private Sprite m_play;
		[SerializeField] private Sprite m_pause;
		
		private Image image;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			image = GetComponent<Image>();
			entity.AddPauseListener(OnPauseListener);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}

		private void OnPauseListener(bool paused)
		{
			image.sprite = paused ? m_play : m_pause;
		}
	}
}
