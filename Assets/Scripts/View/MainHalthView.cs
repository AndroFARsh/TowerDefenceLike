namespace TowerDefenceLike
{
	public class MainHalthView : HalthView, IView
	{
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			base.InitializeView(contexts.game.targetPointEntity, contexts);
		}
	}
}
