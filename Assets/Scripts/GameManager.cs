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
                
                .Add(new PauseNotifySystem(contexts))
                .Add(new HalthUpdateNotifySystem(contexts))
                .Add(new SpeedFactorUpdateNotifySystem(contexts))
                .Add(new CoinsUpdateNotifySystem(contexts))
            
                .Add(new SpeedFactorSystem(contexts))
                
                .Add(new GunAimSystem(contexts))
                .Add(new SelectObjectSystem(contexts))
                .Add(new EnemySpawnCountdownSystem(contexts))
                .Add(new SpawnEnemySystem(contexts))
                .Add(new SpawnTowerSystem(contexts))
                
                .Add(new ShootCooldownSystem(contexts))
                .Add(new SpawnBulletSystem(contexts))
                .Add(new MoveBulletSystem(contexts))
                .Add(new HitTrackSystem(contexts))
                
                .Add(new SpawnExplosionSystem(contexts))
                .Add(new CountdownTimerSystem(contexts))
                
                .Add(new ShootCleanupSystem(contexts))
                
                .Add(new EnemyDieSystem(contexts))
                .Add(new EnemyLootSystem(contexts))
                .Add(new ReleaseSystem(contexts))
                .Add(new FollowToCleanupSystem(contexts));
        }
    }
}