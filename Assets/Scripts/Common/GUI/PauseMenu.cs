using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stickman
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenuUI;


        // Update is called once per frame

        public void Resume(){
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
        }

        public void Pause(){
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public void LoadMenu(){
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }

        public void QuitGame(){
            Application.Quit();
        }

    }
}
