using Smooth.Algebraics;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefenceLike
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class EnemyView : MonoBehaviour, IView
	{
		private static readonly int Die = Animator.StringToHash("Die");
		private static readonly int Type = Animator.StringToHash("Type");
		private static readonly int SpeedMultiplyer = Animator.StringToHash("SpeedMultiplyer");  
		
		[SerializeField] private int m_coins = 1;
		[SerializeField] private int m_halth = 3;
		[SerializeField] private int m_hit = 1;
		[SerializeField] private int m_deathAnimAmount = 4;
		
		private Option<Collider> m_collider;
		private Option<Animator> m_animator;
		private Option<NavMeshAgent> m_navMeshAgent;

		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			m_navMeshAgent = GetComponent<NavMeshAgent>().ToOption();
			
			m_animator = GetComponentInChildren<Animator>().ToOption();
			m_animator.ForEach(animator => animator.SetBool(Die, false));
			
			m_collider = GetComponent<Collider>().ToOption();
			m_collider.ForEach(collider => collider.enabled = true);
			
			entity.isEnemy = true;
			entity.AddHalth(m_halth, m_halth);
			entity.AddHit(m_hit);
			entity.AddCoins(m_coins);
			entity.AddPosition(() => transform.position);
			entity.AddRotation(() => transform.rotation);
			entity.AddPauseListener(PauseListener);
			entity.AddUpdatePosition(position =>  transform.position = position);
			entity.AddUpdateDestinaltionPosition(position => m_navMeshAgent
				.Do4Each(navMeshAgent => navMeshAgent.enabled = true)
				.Do4Each(navMeshAgent => navMeshAgent.destination =  position));
			entity.AddDieListener(enemyEntity =>
			{
				m_collider.ForEach(collider => collider.enabled = false);
				m_navMeshAgent.ForEach(navMeshAgent => navMeshAgent.enabled = false);
				m_animator
					.Do4Each(animator => animator.SetInteger(Type, Random.Range(0, m_deathAnimAmount)))
					.Do4Each(animator => animator.SetBool(Die, true));
			});
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
			m_navMeshAgent
				.Where(navMeshAgent => navMeshAgent.enabled)
				.Do4Each(navMeshAgent => navMeshAgent.ResetPath())
				.Do4Each(navMeshAgent => navMeshAgent.enabled = false);
		}

		private void PauseListener(bool paused)
		{
			m_animator.ForEach(animator => animator.SetFloat(SpeedMultiplyer, paused ? 0 : 1));
				
			m_navMeshAgent
				.Where(navMeshAgent => isActiveAndEnabled && navMeshAgent.enabled)	
				.ForEach(navMeshAgent => navMeshAgent.isStopped = paused);
		}
	}
}
