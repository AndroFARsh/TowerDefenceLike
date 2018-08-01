using System;
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
		private MetaContext m_metaСontext;
		private TowerInfo m_info;
		private Func<Vector3> m_position;
		private Action m_close;

		public void InitializeView(TowerInfo info, Func<Vector3> position, Contexts contexts, Action close)
		{
			m_gameСontext = contexts.game;
			m_metaСontext = contexts.meta;
			
			m_button = GetComponent<Button>();
			m_button.onClick.AddListener(OnButtonClicked);

			m_close = close;
			m_info = info;
			m_position = position;
			m_name.text = info.name;
			m_icon.sprite = info.sptite;
			m_price.text = info.price.ToString();
		}

		private void OnButtonClicked()
		{
			//var viewService = m_metaСontext.viewService.value;
                        
			var newEntity = m_gameСontext.CreateEntity();
			newEntity.AddInitializePoint(m_position);
			newEntity.AddBuild(m_info.assetName, m_position);
			//entity.isSelecteble = false;

			m_close();
		}
	}
}
