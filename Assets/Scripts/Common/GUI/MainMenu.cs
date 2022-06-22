using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Stickman.Managers;
using Stickman.Managers.Sound;


namespace Stickman
{
    public class MainMenu : MonoBehaviour
    {
        public TextMeshProUGUI recentScoreTxt;
        public TextMeshProUGUI bestScoreTxt;
        private SoundManager soundmanager;
        [SerializeField]
        private GameObject scorePanel;
        [SerializeField]
        private GameObject optionsPanel;
        [SerializeField]
        private GameObject creditsPanel;          


        void Start()
        {
            soundmanager = GameManager.Instance.SoundManager; 
            if (PlayerPrefs.GetFloat("HighScore") == 0)
            {
                recentScoreTxt.text = "0";
                bestScoreTxt.text  = "0";
            }
            else
            {
                recentScoreTxt.text = PlayerPrefs.GetFloat("HighScore").ToString("F2");
                bestScoreTxt.text = PlayerPrefs.GetFloat("BestScore").ToString("F2");
            }
        }

        public void PlayGame()
        {
            soundmanager.PlayUIClick(SoundLabels.GOOD);
            int rand = Random.Range(2, 7);
            SceneManager.LoadScene(rand);
        }

        public void QuitGame()
        {
            soundmanager.PlayUIClick(SoundLabels.BAD);
            Application.Quit();
        }

        public void ShowScorePanel(){
            soundmanager.PlayUIClick(SoundLabels.GOOD);
            scorePanel.SetActive(true);
            optionsPanel.SetActive(false);
            gameObject.SetActive(false);
            creditsPanel.SetActive(false);
        }
        public void ShowOptionsPanel(){
            soundmanager.PlayUIClick(SoundLabels.GOOD);
            scorePanel.SetActive(false);
            optionsPanel.SetActive(true);
            gameObject.SetActive(false);
            creditsPanel.SetActive(false);
        }

        public void ShowCreditsPanel(){
            soundmanager.PlayUIClick(SoundLabels.GOOD);
            scorePanel.SetActive(false);
            optionsPanel.SetActive(false);
            gameObject.SetActive(false);
            creditsPanel.SetActive(true);
        }

        public void BackToMenu(){
            soundmanager.PlayUIClick(SoundLabels.BAD);
            scorePanel.SetActive(false);
            optionsPanel.SetActive(false);
            gameObject.SetActive(true);
            creditsPanel.SetActive(false);
        }
    }
}
