
namespace TowerDefenceLike
{
    public interface IViewService
    {
        void Borrow(GameEntity entity, string assetName);
        
        void Release(GameEntity entity);
    }
}