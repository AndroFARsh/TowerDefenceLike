using UnityEngine;

namespace TowerDefenceLike
{
    public class SwimView : MonoBehaviour, IView
    {
        [SerializeField] private Prop m_x;
        [SerializeField] private Prop m_y;
        [SerializeField] private Prop m_z;


        [SerializeField] private Prop m_roll; // x
        [SerializeField] private Prop m_pitch; // y
        [SerializeField] private Prop m_yaw; // z

        public void InitializeView(GameEntity entity, Contexts contexts)
        {
            entity.AddSwim(m_x, m_y, m_z, m_roll, m_pitch, m_yaw, transform.position, transform.rotation.eulerAngles);
            entity.AddUpdatePosition(position => transform.position = position);
            entity.AddUpdateRotation(rotation => transform.rotation = rotation);
        }

        public void DestroyView(GameEntity entity, Contexts contexts)
        {
        }
    }

    [System.Serializable]
    public class Prop
    {
        [SerializeField] private float m_amplitude = 0;
        [SerializeField] private float m_frequnce = 1;

        public float Amplitude => m_amplitude;
        public float Frequnce => m_frequnce;
    }
}