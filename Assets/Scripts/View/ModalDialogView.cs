using UnityEngine;

namespace TowerDefenceLike
{
	public class ModalDialogView : MonoBehaviour, IView
	{
		private bool m_paused;
		
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			m_paused = contexts.game.isPaused;
			contexts.game.isPaused = true;
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
			contexts.game.isPaused = m_paused;
		}
	}
}
