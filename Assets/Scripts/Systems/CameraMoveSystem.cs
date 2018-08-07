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
                                 entity.hasRotation &&
                                 entity.hasUpdatePosition &&
                                 entity.hasCameraConfig)
                .ForEach(Move);
        }

        private static void Move(GameEntity entity)
        {
            var rotation = entity.rotation.value();
            
            var pos = entity.position.value();
            var panBorrderThikness = entity.cameraConfig.panBorrderThikness;
            var panSpeed = entity.cameraConfig.panSpeed;
            
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetMouseButton(2))
            {
                var left = rotation * Vector3.left;
                left.y = 0;
                if (Input.mousePosition.x <= panBorrderThikness)
                    pos += left * panSpeed * Mathf.Pow(1 - Input.mousePosition.x / panBorrderThikness, 3) * Time.deltaTime;
                else if (Input.mousePosition.x >= Screen.width - panBorrderThikness)
                    pos += left * panSpeed * Mathf.Pow((Screen.width - Input.mousePosition.x) / panBorrderThikness - 1, 3)
                                     * Time.deltaTime;
            }

            pos.y -= Input.GetAxis(MouseScorollWheel) * Time.deltaTime * entity.cameraConfig.zoomSpeed * 100;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetMouseButton(2))
            {
                var forward = rotation * Vector3.back;
                forward.y = 0;
                if (Input.mousePosition.y <= panBorrderThikness)
                    pos += forward * panSpeed * Mathf.Pow(1 - Input.mousePosition.y / panBorrderThikness, 3) * Time.deltaTime;
                else if (Input.mousePosition.y >= Screen.height - panBorrderThikness)
                    pos += forward * panSpeed * Mathf.Pow((Screen.height - Input.mousePosition.y) / panBorrderThikness - 1, 3)
                                      * Time.deltaTime;
            }

            pos.x = Mathf.Clamp(pos.x, entity.cameraConfig.panLimitX.x, entity.cameraConfig.panLimitX.y);
            pos.y = Mathf.Clamp(pos.y, entity.cameraConfig.panLimitY.x, entity.cameraConfig.panLimitY.y);
            pos.z = Mathf.Clamp(pos.z, entity.cameraConfig.panLimitZ.x, entity.cameraConfig.panLimitZ.y);

            entity.updatePosition.value(pos);
        }
    }
}