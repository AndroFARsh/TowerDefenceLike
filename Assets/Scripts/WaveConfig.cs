using UnityEngine;

namespace TowerDefenceLike
{
    [CreateAssetMenu(fileName = "Wawe", menuName = "TowerDefence/Wave", order = 1)]
    public class WaveConfig : ScriptableObject
    {
        public string Name;
        
        public float Delay;

        public EnemyChein[] Cheins;

        public int Count => Cheins?.Length ?? 0;
    }
    
    [System.Serializable]
    public class EnemyChein
    {
        public string AssetName;

        public float Delay;
        
        public int Number;
    }
}