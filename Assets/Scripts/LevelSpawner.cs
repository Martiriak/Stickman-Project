using UnityEngine;

namespace Stickman.Levels.Spawner
{
    [RequireComponent(typeof(LevelSpawnerContext))]
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] mLevelsPool;
        private LevelSpawnerContext mContextForLevels;

        private void Awake()
        {
#if UNITY_EDITOR
            if (mLevelsPool.Length == 0)
                Debug.LogError("Aoo! Che ostacoli dovrei spawnare scusa?");
#endif
            mContextForLevels = GetComponent<LevelSpawnerContext>();

            SpawnNewLevel();
        }

        private void SpawnNewLevel()
        {
            var level = Instantiate(mLevelsPool[0], transform.position, Quaternion.identity).GetComponent<Level>();
            mContextForLevels.ProvideContext(level);
            level.EnteringScreenFinished += SpawnNewLevel;
        }
    }
}
