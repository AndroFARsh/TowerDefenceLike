using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefenceLike
{
    [CreateAssetMenu(fileName = "Wave", menuName = "TowerDefence/Wave Config", order = 1)]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private string m_name;
        
        [SerializeField] private float m_delay;
        
        [SerializeField] private ChainInfo[] m_cheins;

        private int Count => m_cheins?.Length ?? 0;

        public IEnumerable<SpawnEnemyInfo> ToSpawnChainEnum()
        {
            return new EnumerableImpl(this);
        }

        public IList<SpawnEnemyInfo> ToSpawnChainList()
        {
            var delay = 0.0f;
            var result = new List<SpawnEnemyInfo>();
            foreach (var chain in m_cheins) {
                delay += m_delay;
                for (var i = 0; i < chain.count; ++i)
                {
                    result.Add(new SpawnEnemyInfo
                    {
                        assetName = chain.assetName,
                        delay = delay
                    });
                    delay += chain.delay;
                }
            }

            return result;
        }
            
        [System.Serializable]
        class ChainInfo
        {
            [SerializeField] internal string assetName;

            [SerializeField] internal float delay;
        
            [SerializeField] internal int count;
        }
        
        class EnumerableImpl : IEnumerable<SpawnEnemyInfo>, IEnumerator<SpawnEnemyInfo>
        {
            private readonly WaveConfig mWaveConfig;

            private int chain = -1;
            private int enemy = -1;
            private float delay;

            internal EnumerableImpl(WaveConfig waveConfig)
            {
                mWaveConfig = waveConfig;
            }

            public IEnumerator<SpawnEnemyInfo> GetEnumerator() => this;

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


            public bool MoveNext()
            {
                if (chain == -1)
                {
                    chain++;
                    delay += mWaveConfig.m_delay;
                }

                if (chain > mWaveConfig.Count) return false;

                if (enemy + 1 < mWaveConfig.m_cheins[chain].count)
                {
                    enemy++;
                    delay += mWaveConfig.m_cheins[chain].delay;
                    return true;
                }
                
                if (chain + 1 < mWaveConfig.Count)
                {
                    chain++;
                    enemy = -1;
                    delay += mWaveConfig.m_delay;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                chain = -1;
                enemy = -1;
                delay = 0.0f;
            }

            public SpawnEnemyInfo Current => new SpawnEnemyInfo
            {
                assetName = mWaveConfig.m_cheins[chain].assetName,
                delay = delay
            };

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                Reset();
            }
        }
    }

    public struct SpawnEnemyInfo
    {
        public string assetName;
        public float delay;
    }

    
}