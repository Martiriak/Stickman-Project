using UnityEngine;
using UnityEngine.SceneManagement;
using Stickman.Managers;
using Stickman.Managers.Time;

namespace Stickman
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuUI;

        public void Resume()
        {
            GameManager.Instance.TimeTracker.Resume();

            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }

        public void Pause()
        {
            GameManager.Instance.TimeTracker.Pause();

            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public void LoadMenu()
        {
            GameManager.Instance.TimeTracker.Stop();

            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }
}
