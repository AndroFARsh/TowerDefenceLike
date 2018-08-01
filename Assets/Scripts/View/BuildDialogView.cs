using Entitas.Unity;
using Smooth.Algebraics;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceLike
{
    [RequireComponent(typeof(Animator))]
    public class BuildDialogView : MonoBehaviour, IView
    {
        private static readonly int Hide = Animator.StringToHash("Hide");

        [SerializeField] private GameObject m_buttonPrefab;
        [SerializeField] private Button m_close;
        [SerializeField] private TowerInfo[] m_towers;
        
        private Option<Animator> m_animator;

        public void InitializeView(GameEntity entity, Contexts contexts)
        {
            m_animator = GetComponent<Animator>().ToOption();
            m_animator.ForEach(animator => animator.SetBool(Hide, false));

            entity.isBuildDialog = true;

            m_close.onClick.AddListener(CloseDialog);
            for (var i = 0; i < m_towers.Length; ++i)
            {
                var theta = 2 * Mathf.PI * i / m_towers.Length;
                var x = Mathf.Sin(theta);
                var y = Mathf.Cos(theta);

                var newButton = Instantiate(m_buttonPrefab, transform);
                newButton.transform.localPosition = new Vector3(x, y, 0) * 100;
                newButton.GetComponent<BuildButtonView>()
                    .InitializeView(m_towers[i], entity.initializePoint.value, contexts, CloseDialog);
            }
        }

        public void DestroyView(GameEntity entity, Contexts contexts)
        {
        }

        private void CloseDialog()
        {
            gameObject.GetEntityLink()
                .ToOption()
                .Select(link => link.entity as GameEntity)
                .ForEach(entity => entity.isRelease = true);
        }
    }
    
    [System.Serializable]
    public class TowerInfo
    {
        public string assetName;
        public string name;
     
        public Sprite sptite;
        public int    price;
    }   
}