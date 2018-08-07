using System;
using Smooth.Algebraics;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(Button))]
	public class BuildButtonView : MonoBehaviour
	{
		[SerializeField] private Text m_name;
		[SerializeField] private Image m_icon;
		[SerializeField] private Text m_price;
		
		private Button m_button;
		private GameContext m_gameСontext;
		private TowerInfo m_info;
		private Func<Vector3> m_position;
		private Action m_close;
		private int m_id;

		public void InitializeView(TowerInfo info, int id, Func<Vector3> position, Contexts contexts, Action close)
		{
			m_gameСontext = contexts.game;
			
			m_button = GetComponent<Button>();
			m_button.onClick.AddListener(OnButtonClicked);

			m_close = close;
			m_info = info;
			m_id = id;
			m_position = position;
			m_name.text = info.name;
			m_icon.sprite = info.sptite;
			m_price.text = info.price.ToString();
		}

		private void OnButtonClicked()
		{
			var tartget = m_gameСontext.targetPointEntity;
			if (tartget.coins.value - m_info.price >= 0) {
				tartget.ReplaceCoins(tartget.coins.value - m_info.price);
				
				var newEntity = m_gameСontext.CreateEntity();
				newEntity.AddInitializePoint(m_position);
				newEntity.AddBuild(m_info.assetName, m_position);
				
				m_gameСontext.GetEntityWithId(m_id).ToOption().ForEach(entity => entity.isSelecteble = false);
				
				m_close();
			}
		}
	}
}
