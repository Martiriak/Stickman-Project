using UnityEngine;
using UnityEngine.SceneManagement;
using Stickman.Managers;
using Stickman.Managers.Sound;

namespace Stickman
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject deadMenuUI;
        private SoundManager soundManager;
        public void Resume()
        {
            soundManager.PlayUIClick(SoundLabels.GOOD);
            GameManager.Instance.TimeTracker.Resume();
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }

        public void Pause()
        {
            Debug.Log("Pausa");
            GameManager.Instance.TimeTracker.Pause();
            soundManager.PlayUIClick(SoundLabels.GOOD);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public void LoadMenu()
        {
            GameManager.Instance.TimeTracker.Stop();
            soundManager.PlayUIClick(SoundLabels.GOOD);
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }

        public void QuitGame()
        {
            soundManager.PlayUIClick(SoundLabels.BAD);
            Application.Quit();
        }

        public void Restart(){
            soundManager.PlayUIClick(SoundLabels.GOOD);
            Time.timeScale = 1f;
            int rand = Random.Range(2, 7);
            GameManager.Instance.LivesManager.ResetLife();
            GameManager.Instance.TimeTracker.Stop();
            GameManager.Instance.SpeedManager.ResetSpeed();
            SceneManager.LoadScene(rand);
            
        }

        void Die(){
            soundManager.PlayGameOverSound();
            deadMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }

        private void Start() {
            GameManager.Instance.LivesManager.OnDeath += Die;
            soundManager = GameManager.Instance.SoundManager; 
        }
        private void OnDestroy() { GameManager.Instance.LivesManager.OnDeath -= Die; }

    }
}
