using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stickman
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame(){
            int rand = Random.Range(2, 7);
            SceneManager.LoadScene(rand);
        }

        public void QuitGame(){
            Application.Quit();
        }
    }
}
