using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Stickman.Managers;
using Stickman.Levels.Spawner;


namespace Stickman.Scenes
{
    public class SceneInitiator : MonoBehaviour
    {
        [SerializeField] private bool m_isPlayableScene = true;
        [SerializeField] private LevelSpawner m_levelSpawner = null;

        private IEnumerator Start()
        {
            GameManager.Instance.CurrentLoadedScene = SceneManager.GetActiveScene().buildIndex;

            if (m_isPlayableScene)
            {
                if (GameManager.Instance.TimeTracker.IsPlaying)
                {
                    GameManager.Instance.TimeTracker.Lap();
                    GameManager.Instance.TimeTracker.Resume();
                }
                else
                {
                    GameManager.Instance.TimeTracker.StartStopWatch(false);

                    while (!m_levelSpawner.HasFinishedSpawning)
                        yield return null;

                    GameManager.Instance.TimeTracker.Resume();
                }
            }
            else
            {
                // Not necessary, but safe to do.
                GameManager.Instance.TimeTracker.Pause();
            }
        }
    }
}
