using System;
using Entitas;
using Smooth.Slinq;
using UnityEngine;

namespace TowerDefenceLike
{
    public class CameraRotationSystem : IExecuteSystem
    {
        private readonly GameContext m_context;
        private readonly IGroup<GameEntity> m_group;

        private Vector3 prevMouse;
        
        public CameraRotationSystem(Contexts contexts)
        {
            m_context = contexts.game;
            m_group = m_context.GetGroup(GameMatcher.Camera);
        }

        public void Execute()
        {
            m_group.GetEntities().Slinq()
                .Where(entity => entity.hasRotation &&
                                 entity.hasPosition &&
                                 entity.hasUpdateRotation &&
                                 entity.hasCameraConfig)
                .ForEach(Rotaate);
        }

        private void Rotaate(GameEntity entity)
        {
            if (Input.GetMouseButtonDown(1))
            {
                prevMouse = Input.mousePosition;
            }
            
            if (!Input.GetMouseButton(1)) return;

            if (Math.Abs(Input.mousePosition.x - prevMouse.x) < 0.01f) return;

            var rotationY = (Input.mousePosition.x - prevMouse.x) * 
                             Time.deltaTime * entity.cameraConfig.rotateSpeed / 10;

            var prevAngles = entity.rotation.value().eulerAngles;
            var rotation = Quaternion.Euler(prevAngles.x, prevAngles.y + rotationY, prevAngles.z);

            entity.updateRotation.value(rotation);
        }
    }
}