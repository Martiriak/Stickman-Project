using UnityEngine;
//using Stickman.PlayArea;

namespace Stickman.Levels.Spawner
{
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] mLevelsPool;
        //[SerializeField] private 

        private void Awake()
        {
#if UNITY_EDITOR
            if (mLevelsPool.Length == 0)
                Debug.LogError("Aoo! Che ostacoli dovrei spawnare scusa?");
#endif
            SpawnNewLevel();
        }

        private void SpawnNewLevel()
        {
            var level = Instantiate(mLevelsPool[0], transform.position, Quaternion.identity).GetComponent<Level>();
            level.EnteringScreenFinished += SpawnNewLevel;
        }
    }
}
