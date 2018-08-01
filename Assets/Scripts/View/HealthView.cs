using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(Slider))]
	public class HealthView : MonoBehaviour, IView
	{
		private Slider m_slider;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			m_slider = GetComponent<Slider>();
			entity.AddHalthListener(OnHalthListener);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}

		private void OnHalthListener(int max, int value)
		{
			m_slider.value = Mathf.Max(0, ((float) value) / max);
		}
	}
}
