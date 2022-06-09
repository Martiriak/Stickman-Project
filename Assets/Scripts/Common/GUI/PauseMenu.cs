using UnityEngine;
using UnityEngine.SceneManagement;
using Stickman.Managers;

namespace Stickman
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject deadMenuUI;
        public void Resume()
        {
            GameManager.Instance.TimeTracker.Resume();

            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }

        public void Pause()
        {
            Debug.Log("Pausa");
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

        public void Restart(){
            Time.timeScale = 1f;
            int rand = Random.Range(2, 7);
            GameManager.Instance.LivesManager.ResetLife();
            SceneManager.LoadScene(rand);
            
        }

        void Die(){
            deadMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }

        private void Start() { GameManager.Instance.LivesManager.OnDeath += Die; }
        private void OnDestroy() { GameManager.Instance.LivesManager.OnDeath -= Die; }

    }
}
