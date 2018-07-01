namespace TowerDefenceLike
{
	public class MainHalthView : HalthView
	{
		public void InitializeView(GameEntity entity, Contexts contexts)
		{
			base.InitializeView(entity, contexts);
			entity.AddName("");
		}
	}
}
