using UnityEngine;

namespace TowerDefenceLike
{
	public class PlatformView : MonoBehaviour, IView
	{
		private const string Spawn = "Spawn";
		
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			gameObject.SetActiveRecursively(true);
			var buildTransform =  transform.Find(Spawn) ?? transform;
			
			entity.isPlatform = true;
			entity.isSelecteble = true;
			entity.AddId(gameObject.GetInstanceID());
			entity.AddPosition(() => buildTransform.position);
		}

		public void DestroyView(GameEntity entity, Contexts contexts)
		{
			gameObject.SetActiveRecursively(false);
		}
	}
}
