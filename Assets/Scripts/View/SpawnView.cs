using UnityEngine;

namespace TowerDefenceLike
{
	public class SpawnView : MonoBehaviour, IView
	{
		[SerializeField] private WaveConfig m_waveConfig;
		
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			entity.isSpawnPoint = true;
			entity.AddPosition(() => transform.position);
			entity.AddRotation(() => transform.rotation);
			entity.AddWave(m_waveConfig);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}
	}
}