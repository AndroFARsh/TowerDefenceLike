﻿using Entitas.Unity;
using Smooth.Algebraics;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(Button))]
	public class SpeedButtonView : MonoBehaviour, IView
	{
		private Button m_button;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			m_button = GetComponent<Button>();
			m_button.onClick.AddListener(OnButtonClicked);
			entity.AddSpeedFactor(SpeedFactor.x1);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}

		private void OnButtonClicked()
		{
			gameObject.GetEntityLink()
				.ToOption()
				.Select(link => link.entity as GameEntity)
				.ForEach(e => e.ReplaceSpeedFactor(e.speedFactor.value.Next()));
		}
	}
}
