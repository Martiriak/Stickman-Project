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
        private SoundManager m_soundManager;
        [SerializeField]
        private GameObject m_scorePanel;
        [SerializeField]
        private GameObject m_optionsPanel;


        void Start(){
            m_soundManager = GameManager.Instance.SoundManager;
            if(PlayerPrefs.GetFloat("HighScore") == null){
                recentScoreTxt.text = "0";
                bestScoreTxt.text  = "0";
            }
            else{
                recentScoreTxt.text = PlayerPrefs.GetFloat("HighScore").ToString("F2");
                bestScoreTxt.text = PlayerPrefs.GetFloat("BestScore").ToString("F2");
            }
        }

        public void PlayGame()
        {
            m_soundManager.PlayUIClick(SoundLabels.GOOD);
            int rand = Random.Range(2, 7);
            SceneManager.LoadScene(rand);
        }

        public void QuitGame()
        {
            m_soundManager.PlayUIClick(SoundLabels.BAD);
            Application.Quit();
        }

        public void ScoreMenuOn(){
            m_soundManager.PlayUIClick(SoundLabels.GOOD);
            m_scorePanel.SetActive(true);
            m_optionsPanel.SetActive(false);
            gameObject.SetActive(false);
        }
        public void OptionsMenuOn(){
            m_soundManager.PlayUIClick(SoundLabels.GOOD);
            m_scorePanel.SetActive(false);
            m_optionsPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        public void BackToMenu(){
            m_soundManager.PlayUIClick(SoundLabels.BAD);
            m_scorePanel.SetActive(false);
            m_optionsPanel.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}
