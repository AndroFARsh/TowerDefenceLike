using Entitas;
using Entitas.Unity;
using Smooth.Algebraics;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class UnityViewService : IViewService
    {
        private readonly Contexts m_contexts;
        private readonly IGameObjectService m_gameObjectService;

        public UnityViewService(Contexts contexts, IGameObjectService gameObjectService)
        {
            m_contexts = contexts;
            m_gameObjectService = gameObjectService;
        }

        public void Borrow(GameEntity entity, string assetName)
        {
            m_gameObjectService.Borrow(assetName)
                .ToOption()
                .ForEach(go =>
                {
                    var list = go.GetComponentsInChildren<IView>();
                    list.Slinq()
                        .ForEach(view =>
                        {
                            view.Link(entity, m_contexts.game);
                            view.InitializeView(entity, m_contexts);
                        });
                    
                    if (!entity.hasId) entity.AddId(go.GetInstanceID());   
                    if (!entity.hasAssetName) entity.AddAssetName(assetName);
                    if (!entity.hasGameObject) entity.AddGameObject(go);
                });
        }

        public void Release(GameEntity entity)
        {
            if (!entity.hasGameObject || !entity.hasAssetName) return;
            
            entity.gameObject.value.GetComponentsInChildren<IView>().Slinq()
                .ForEach(view =>
                {
                    view.DestroyView(entity, m_contexts);
                    view.Unlink();
                });
            
            m_gameObjectService.Release(entity.assetName.value, entity.gameObject.value);
            entity.Destroy();
        }
    }
}