namespace TowerDefenceLike
{
	public class MainHalthView : HalthView, IView
	{
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			base.InitializeView(entity, contexts);

			var target = contexts.game.targetPointEntity;
			if (entity.hasHalthListener) target.AddHalthListener(entity.halthListener.value);
		}
	}
}
