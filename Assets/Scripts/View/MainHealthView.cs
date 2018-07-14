namespace TowerDefenceLike
{
	public class MainHealthView : HealthView, IView
	{
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			entity.isRelease = true;
			base.InitializeView(contexts.game.targetPointEntity, contexts);

//			base.InitializeView(entity, contexts);
//
//			var target = contexts.game.targetPointEntity;
//			if (entity.hasHalthListener) target.AddHalthListener(entity.halthListener.value);
		}
	}
}
