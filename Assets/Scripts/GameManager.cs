using Entitas;
using TowerDefenceLike;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        private Systems systems;
        
        private void Start()
        {
            InitializeMeta();
            systems = CreateSystems();
            
            systems.Initialize();
        }

        private void Update()
        {
            systems.Execute();
            systems.Cleanup();
        }

        private void OnDestroy()
        {
            systems.TearDown();
        }

        private static void InitializeMeta()
        {
            var contexts = Contexts.sharedInstance;
            var gameObjectService = new UnityGameObjectService();
            var viewService = new UnityViewService(contexts, gameObjectService);
            
            var entity = contexts.meta.CreateEntity();
            entity.AddViewService(viewService);
        }

        private static Systems CreateSystems()
        {
            var contexts = Contexts.sharedInstance;
            return new Feature("GameManager")
                .Add(new InitializeViewsOnSceneSystem(contexts))
                .Add(new InitializeLevelSystem(contexts))
                
                .Add(new NotifyPauseSystem(contexts))
                .Add(new NotifyHalthUpdateSystem(contexts))
                
                .Add(new SpawnEnemySystem(contexts))
                .Add(new ReleaseSystem(contexts));
        }
    }
}