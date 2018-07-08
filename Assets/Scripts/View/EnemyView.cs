using Entitas;
using Smooth.Algebraics;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class EnemyView : MonoBehaviour, IView
	{
		[SerializeField] private int m_halth = 3;
		[SerializeField] private int m_hit = 1;
		
		private NavMeshAgent m_navMeshAgent;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			gameObject.SetActiveRecursively(true);
			m_navMeshAgent = GetComponent<NavMeshAgent>();
		
			entity.isEnemy = true;
			entity.AddHalth(m_halth, m_halth);
			entity.AddHit(m_hit);
			entity.AddPosition(() => transform.position);
			entity.AddRotation(() => transform.rotation);
			entity.AddPauseListener(PauseListener);
			entity.AddUpdatePosition(position =>  transform.position = position);
			entity.AddUpdateDestinaltionPosition(position =>
			{
				m_navMeshAgent.enabled = true;
				m_navMeshAgent.destination =  position;
			});
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
			m_navMeshAgent.ResetPath();
			m_navMeshAgent.enabled = false;
			gameObject.SetActiveRecursively(false);
		}

		private void PauseListener(bool paused)
		{
			if (isActiveAndEnabled) m_navMeshAgent.isStopped = paused;
		}
	}
}
