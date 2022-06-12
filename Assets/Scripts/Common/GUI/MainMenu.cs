using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace Stickman
{
    public class MainMenu : MonoBehaviour
    {
        public TextMeshProUGUI recentScoreTxt;
        public TextMeshProUGUI bestScoreTxt;

        void Start(){
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
            int rand = Random.Range(2, 7);
            SceneManager.LoadScene(rand);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
