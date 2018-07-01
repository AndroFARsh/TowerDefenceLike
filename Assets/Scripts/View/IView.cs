namespace TowerDefenceLike
{
    public interface IView
    {
        void InitializeView(GameEntity entity, Contexts contexts);

        void DestroyView(GameEntity entity, Contexts contexts);
    }
}