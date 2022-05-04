using UnityEngine;
using Stickman.WeightWrapper;
using Random = UnityEngine.Random;

namespace Stickman.Levels.Spawner
{
    /// <summary>
    /// Chooses a level from a pool and instantiates it, providing it with
    /// context.
    /// </summary>
    [RequireComponent(typeof(LevelSpawnerContext))]
    public class LevelSpawner : MonoBehaviour
    {
        [Tooltip("To increase efficency, the levels with the highest weights should be placed in the first positions.")]
        [SerializeField] private WeightedObject<Level>[] m_levelsPool;
        [Tooltip("Minimum number of level slots it will generate.")]
        [SerializeField] private int m_minSlots = 20;
        [Tooltip("Maximum number of level slots it will generate.")]
        [SerializeField] private int m_maxSlots = 50;
        [Tooltip("The final level.")]
        [SerializeField] private GameObject m_finalLevelPrefab;

        private LevelSpawnerContext m_contextForLevels;
        private Vector3 m_nextLevelPosition;

        // The cached sum of all the probabilities weights.
        private float c_levelsWeightTotal;

        private void Awake()
        {
#if UNITY_EDITOR
            if (m_levelsPool.Length == 0)
                Debug.LogError("Aoo! Che ostacoli dovrei spawnare scusa?");
#endif
            // Compute the cache.
            c_levelsWeightTotal = 0f;
            for (int i = 0; i < m_levelsPool.Length; ++i)
                c_levelsWeightTotal += m_levelsPool[i].SpawnProbWeight;

            m_contextForLevels = GetComponent<LevelSpawnerContext>();
        }

        private void Start()
        {
            m_nextLevelPosition = transform.position;
            SpawnLevels();
        }

        private void SpawnLevels()
        {
            int numberOfSlots = Random.Range(m_minSlots, m_maxSlots);

            while (numberOfSlots > 0)
                numberOfSlots -= SpawnNewLevel();

            Level level = Instantiate(m_finalLevelPrefab, m_nextLevelPosition, transform.rotation).GetComponent<Level>();
            m_contextForLevels.ProvideContext(level);
        }

        private int SpawnNewLevel()
        {
            int chosenLevelIndex = PickRandomLevel();

            GameObject prefabLevelGameObj = m_levelsPool[chosenLevelIndex].Object.gameObject;

            Level level = Instantiate(prefabLevelGameObj, m_nextLevelPosition, transform.rotation).GetComponent<Level>();
            m_contextForLevels.ProvideContext(level);
            //level.EnteringScreenFinished += SpawnNewLevel;
            m_nextLevelPosition = level.GetEndLevelPosition();

            return m_levelsPool[chosenLevelIndex].LevelSlotWeight;
        }

        /// <returns>The index of the randomly chosen level.</returns>
        private int PickRandomLevel()
        {
            if (m_levelsPool.Length == 1) return 0;

            // The weights are normalized from 0 to 1, which means we can
            // treat them as direct probabilities values.

            float randomThreshold = Random.value;
            float weightCheck = 0f;

            int currentLevel;
            for (currentLevel = 0; currentLevel < m_levelsPool.Length - 1; ++currentLevel)
            {
                weightCheck += m_levelsPool[currentLevel].SpawnProbWeight / c_levelsWeightTotal;
                if (weightCheck > randomThreshold)
                    return currentLevel;
            }

            return currentLevel;
        }
    }
}
