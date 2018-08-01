using UnityEngine;

namespace TowerDefenceLike
{
    [CreateAssetMenu(fileName = "Towers", menuName = "TowerDefence/Towers Config", order = 1)]
    public class TowersConfig : ScriptableObject
    {
        [SerializeField] private TowerInfo[] m_towers;
        
        public int Length => m_towers.Length;
        public TowerInfo this[int index] => m_towers[index];
        
        [System.Serializable]
        public class TowerInfo
        {
            public string assetName;
            public string name;
     
            public Sprite sptite;
            public int    price;
        }   
    }
}