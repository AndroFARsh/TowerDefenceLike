using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(Slider))]
	public class HalthView : MonoBehaviour, IView
	{
		private Slider slider;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			slider = GetComponent<Slider>();
			entity.AddHalthListener((max, value) => slider.value = ((float)value)/max);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}
	}
}
