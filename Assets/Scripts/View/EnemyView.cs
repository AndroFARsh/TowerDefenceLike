using UnityEngine;
using UnityEngine.AI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class EnemyView : MonoBehaviour, IView
	{
		[SerializeField] private int halth = 3;
		[SerializeField] private int hit = 1;
		
		private NavMeshAgent navMeshAgent;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			gameObject.SetActiveRecursively(true);
			navMeshAgent = GetComponent<NavMeshAgent>();
		
			entity.isEnemy = true;
			entity.AddHalth(halth, halth);
			entity.AddHit(hit);
			entity.AddPauseListener(PauseListener);

			if (entity.hasName) gameObject.name = entity.name.value;	
			
			if (entity.hasStartDestinationPoint) {
				transform.position = entity.startDestinationPoint.startPosition;
				navMeshAgent.enabled = true;
				navMeshAgent.destination =  entity.startDestinationPoint.destinationPosition;
			}
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
			navMeshAgent.ResetPath();
			navMeshAgent.enabled = false;
			gameObject.SetActiveRecursively(false);
		}

		private void PauseListener(bool paused)
		{
			navMeshAgent.isStopped = paused;
		}
	}
}
