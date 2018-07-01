﻿using Entitas.Unity;
using Smooth.Algebraics;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(Button))]
	public class PauseButtonView : MonoBehaviour, IView
	{
		private Button button;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			button = GetComponent<Button>();
			button.onClick.AddListener(OnButtonClicked);
			entity.isPaused = true;
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}

		private void OnButtonClicked()
		{
			Debug.Log("Button Click");
			gameObject.GetEntityLink()
				.ToOption()
				.Select(link => link.entity as GameEntity)
				.ForEach(e => e.isPaused = !e.isPaused);
		}
	}
}
