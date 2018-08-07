using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(Image))]
	public class MuteIconView : MonoBehaviour, IView
	{
		[SerializeField] private Sprite m_play;
		[SerializeField] private Sprite m_mute;
		
		private Image image;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			image = GetComponent<Image>();
			entity.AddMuteUpdate(OnMuteListener);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}

		private void OnMuteListener(bool mute)
		{
			image.sprite = mute ? m_mute : m_play;
		}
	}
}
