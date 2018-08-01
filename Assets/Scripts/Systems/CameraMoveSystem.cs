using Entitas;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class CameraMoveSystem : IExecuteSystem
    {
        private const string MouseScorollWheel = "Mouse ScrollWheel";

        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        public CameraMoveSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.Camera);
        }

        public void Execute()
        {
            m_group.GetEntities().Slinq()
                .Where(entity => entity.hasPosition &&
                                 entity.hasUpdatePosition &&
                                 entity.hasCameraMoveConfig)
                .ForEach(Move);
        }

        private static void Move(GameEntity entity)
        {
            var pos = entity.position.value();
            var panBorrderThikness = entity.cameraMoveConfig.panBorrderThikness;
            var panSpeed = entity.cameraMoveConfig.panSpeed;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ) {
                if (Input.mousePosition.x <= panBorrderThikness)
                    pos.x += panSpeed * Mathf.Pow(1 - Input.mousePosition.x / panBorrderThikness, 3) * Time.deltaTime;
                else if (Input.mousePosition.x >= Screen.width - panBorrderThikness)
                    pos.x += panSpeed * Mathf.Pow((Screen.width - Input.mousePosition.x) / panBorrderThikness - 1, 3)
                                      * Time.deltaTime;
            }
            
            pos.y -= Input.GetAxis(MouseScorollWheel) * Time.deltaTime * entity.cameraMoveConfig.zoomSpeed * 100;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                if (Input.mousePosition.y <= panBorrderThikness)
                    pos.z += panSpeed * Mathf.Pow(1 - Input.mousePosition.y / panBorrderThikness, 3) * Time.deltaTime;
                else if (Input.mousePosition.y >= Screen.height - panBorrderThikness)
                    pos.z += panSpeed * Mathf.Pow((Screen.height - Input.mousePosition.y) / panBorrderThikness - 1, 3)
                                      * Time.deltaTime;
            }

            pos.x = Mathf.Clamp(pos.x, entity.cameraMoveConfig.panLimitX.x, entity.cameraMoveConfig.panLimitX.y);
            pos.y = Mathf.Clamp(pos.y, entity.cameraMoveConfig.panLimitY.x, entity.cameraMoveConfig.panLimitY.y);
            pos.z = Mathf.Clamp(pos.z, entity.cameraMoveConfig.panLimitZ.x, entity.cameraMoveConfig.panLimitZ.y);

            entity.updatePosition.value(pos);
        }
    }
}