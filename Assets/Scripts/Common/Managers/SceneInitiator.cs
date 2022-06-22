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
        [SerializeField] private Animation m_fadeAnimation;
        [SerializeField] private TutorialUI m_tutorialPanel;

        private IEnumerator Start()
        {
            GameManager.Instance.CurrentLoadedScene = SceneManager.GetActiveScene().buildIndex;
            m_tutorialPanel.OnTutorialClose += ()=>{GameManager.Instance.TimeTracker.Resume();};

            if (m_isPlayableScene)
            {
                if (GameManager.Instance.TimeTracker.IsPlaying)
                {
                    m_fadeAnimation.Play();
                    GameManager.Instance.TimeTracker.Lap();
                    GameManager.Instance.TimeTracker.Resume();
                }
                else
                {
                    GameManager.Instance.TimeTracker.StartStopWatch(false);

                    while (!m_levelSpawner.HasFinishedSpawning)
                        yield return null;

                    m_fadeAnimation.Play();
                    yield return null;

                    while (m_fadeAnimation.isPlaying)
                        yield return null;

                    m_tutorialPanel.SetTutorial();
                }
            }
            else
            {
                 m_fadeAnimation.Play();
                // Not necessary, but safe to do.
                GameManager.Instance.TimeTracker.Pause();
            }
        }
    }
}
