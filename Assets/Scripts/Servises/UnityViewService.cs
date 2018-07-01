using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Smooth.Algebraics;
using Smooth.Pools;
using Smooth.Slinq;
using UnityEditor;
using UnityEngine;

namespace TowerDefenceLike
{
    public class UnityViewService : IViewService
    {
        private readonly Contexts mContexts;
        private readonly IGameObjectService mGameObjectService;

        public UnityViewService(Contexts contexts, IGameObjectService gameObjectService)
        {
            mContexts = contexts;
            mGameObjectService = gameObjectService;
        }

        public void Borrow(GameEntity entity, string assetName)
        {
            mGameObjectService.Borrow(assetName)
                .ToOption()
                .ForEach(go =>
                {
                    entity.AddGameObject(assetName, go);
                    go.Link(entity, mContexts.game);
                    go.GetComponentsInChildren<IView>().Slinq()
                        .ForEach(view => view.InitializeView(entity, mContexts));
                });
        }

        public void Release(GameEntity entity)
        {
            if (!entity.hasGameObject) return;
            
            var gameObjectComponent = entity.gameObject;
            gameObjectComponent.go.GetComponentsInChildren<IView>().Slinq()
                .ForEach(view => view.DestroyView(entity, mContexts));
            gameObjectComponent.go.Unlink();
            
            mGameObjectService.Release(gameObjectComponent.assetName, gameObjectComponent.go);
            entity.Destroy();
        }
    }
}