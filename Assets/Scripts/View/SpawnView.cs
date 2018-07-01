using UnityEngine;

namespace TowerDefenceLike
{
	public class SpawnView : MonoBehaviour, IView
	{
		[SerializeField] private WaveConfig waveConfig;
		
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			entity.isSpawnPoint = true;
			entity.AddPosition(() => transform.position);
			entity.AddRotation(() => transform.rotation);
			entity.AddWave(waveConfig);
			entity.AddChainId(0);
			entity.AddEnemyId(0);
			entity.AddDelay(0);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
		}
	}
}