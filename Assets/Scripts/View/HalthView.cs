using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(Slider))]
	public class HalthView : MonoBehaviour, IView
	{
		private Slider m_slider;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			m_slider = GetComponent<Slider>();
			entity.AddHalthListener((max, value) =>
			{
				m_slider.value = ((float) value) / max;
			});
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}
	}
}
