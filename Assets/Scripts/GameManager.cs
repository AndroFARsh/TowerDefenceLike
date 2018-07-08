using Entitas;
using TowerDefenceLike;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        private const string Name = "GameManager";
        
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
            return new Feature(Name)
                .Add(new InitializeViewsOnSceneSystem(contexts))
                .Add(new InitializeWaveSystem(contexts))
                
                .Add(new NotifyPauseSystem(contexts))
                .Add(new NotifyHalthUpdateSystem(contexts))
                .Add(new NotifySpeedUpdateSystem(contexts))
            
                .Add(new SpeedSystem(contexts))
                
                .Add(new GunRotationSystem(contexts))
                .Add(new SelectObjectSystem(contexts))
                .Add(new EnemySpawnDelayCountdownSystem(contexts))
                .Add(new SpawnEnemySystem(contexts))
                .Add(new SpawnTowerSystem(contexts))
                
                .Add(new ShootCoolDownSystem(contexts))
                .Add(new ShootSystem(contexts))
                
                .Add(new DeathSystem(contexts))
                .Add(new ReleaseSystem(contexts))
                .Add(new CleanupFollowToSystem(contexts));
        }
    }
}