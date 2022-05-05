using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Stickman
{
    public class LevelLoader : MonoBehaviour
    {
        public Animator transition;
        public float transitionTime = 1f;
        public void LoadNextLevel()
        {
            StartCoroutine(LoadLevel());
        }

        IEnumerator LoadLevel()
        {
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene("GravityFrog");
            this.gameObject.SetActive(true);
        }

    }
}
