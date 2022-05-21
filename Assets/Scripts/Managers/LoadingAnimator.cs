using UnityEngine.SceneManagement;
using UnityEngine;

namespace Stickman
{
    public class LoadingAnimator : MonoBehaviour
    {
        public void LoadNextScene()
        {
            int nextLevelToLoad = GetNextScene();
            SceneManager.LoadScene(nextLevelToLoad);
        }


        private int GetNextScene()
        {
            // Dalla 2 alla 6 sono scene di gioco; lo 0 � il men�, lo 1 � la loading.
            return 5; //UnityEngine.Random.Range(2, 7);
        }
    }
}
