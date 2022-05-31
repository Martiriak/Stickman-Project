using UnityEngine;
using UnityEngine.SceneManagement;
using Stickman.Managers;

namespace Stickman.Scenes
{
    public class SceneInitiator : MonoBehaviour
    {
        [SerializeField] private bool m_isPlayableScene = true;

        private void Start()
        {
            GameManager.Instance.CurrentLoadedScene = SceneManager.GetActiveScene().buildIndex;

            if (m_isPlayableScene)
            {
                GameManager.Instance.TimeTracker.Lap();
                GameManager.Instance.TimeTracker.Resume();
            }
            else
            {
                // Not necessary, but safe to do.
                GameManager.Instance.TimeTracker.Pause();
            }
        }
    }
}
