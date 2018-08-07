using UnityEngine;

namespace TowerDefenceLike
{
    [RequireComponent(typeof(Camera))]
    public class MainCameraView : MonoBehaviour, IView
    {
        [SerializeField] private float m_rotateSpeed = 10;
        [SerializeField] private float m_panSpeed = 20;
        [SerializeField] private float m_zoomSpeed = 20;
        [SerializeField] private float m_panBorrderThikness = 300;
        [SerializeField] private Vector2 m_panLimitX;
        [SerializeField] private Vector2 m_panLimitY;
        [SerializeField] private Vector2 m_panLimitZ;

        public void InitializeView(GameEntity entity, Contexts contexts)
        {     
            gameObject.SetActiveRecursively(true);

            entity.isCamera = true;
            entity.AddCameraConfig(m_rotateSpeed, m_panSpeed, m_panBorrderThikness, m_zoomSpeed, m_panLimitX,
                m_panLimitY, m_panLimitZ);
            entity.AddPosition(() => transform.position);
            entity.AddRotation(() => transform.rotation);
            entity.AddUpdatePosition(position => transform.position = position);
            entity.AddUpdateRotation(rotation => transform.rotation = rotation);
        }

        public void DestroyView(GameEntity entity, Contexts contexts)
        {
            gameObject.SetActiveRecursively(false);
        }
    }
}