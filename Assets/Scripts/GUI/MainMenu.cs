using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stickman
{
    public class MainMenu : MonoBehaviour
    {
               public void PlayGame(){
            SceneManager.LoadScene("GravityFrog");
        }

        public void QuitGame(){
            Application.Quit();
        }
    }
}
