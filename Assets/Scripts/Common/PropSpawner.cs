using UnityEngine;
using Stickman.WeightWrapper;

namespace Stickman
{
    public class PropSpawner : MonoBehaviour
    {
        [SerializeField] private WeightedObject<GameObject>[] m_propsPool;
        [SerializeField] private Transform[] m_propsSpawnPoints;

        private float c_propsWeightTotal;

        private void Awake()
        {
            c_propsWeightTotal = 0f;
            for (int i = 0; i < m_propsPool.Length; ++i)
                c_propsWeightTotal += m_propsPool[i].SpawnProbWeight;
        }

        private void Start()
        {
            if (m_propsPool.Length == 0) return;

            foreach (Transform spawnPoint in m_propsSpawnPoints)
            {
                int propToSpawnIndex = PickRandomProp();

                GameObject prefabPropGameObj = m_propsPool[propToSpawnIndex].Object.gameObject;

                if (prefabPropGameObj != null)
                    Instantiate(prefabPropGameObj, spawnPoint);
            }
        }


        /// <returns>The index of the randomly chosen prop.</returns>
        private int PickRandomProp()
        {
            if (m_propsPool.Length == 1) return 0;

            // The weights are normalized from 0 to 1, which means we can
            // treat them as direct probabilities values.

            float randomThreshold = Random.value;
            float weightCheck = 0f;

            int currentProp;
            for (currentProp = 0; currentProp < m_propsPool.Length - 1; ++currentProp)
            {
                weightCheck += m_propsPool[currentProp].SpawnProbWeight / c_propsWeightTotal;
                if (weightCheck > randomThreshold)
                    return currentProp;
            }

            return currentProp;
        }
    }
}
