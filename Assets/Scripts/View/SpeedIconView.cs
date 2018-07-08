using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(Image))]
	public class SpeedIconView : MonoBehaviour, IView
	{
		[SerializeField] private Sprite m_x1;
		[SerializeField] private Sprite m_x2;
		[SerializeField] private Sprite m_x4;
		
		private Image image;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			image = GetComponent<Image>();
			entity.AddSpeedListener(OnSpeedListener);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}

		private void OnSpeedListener(SpeedFactor speedFactor)
		{
			switch (speedFactor)
			{
				case SpeedFactor.x4:
					image.sprite = m_x4;
					break;
				case SpeedFactor.x2:
					image.sprite = m_x2;
					break;
				case SpeedFactor.x1:
				default:
					image.sprite = m_x1;
					break;
			}
		}
	}
}
