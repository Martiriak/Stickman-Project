using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Stickman.Managers;
using Stickman.Managers.Sound;

namespace Stickman
{
    public class TutorialUI : MonoBehaviour
    {
        public TextMeshProUGUI tutorialTxt;
        public GameObject Panel;
        [TextArea] public string inputField;
        Scene scene;

        public event Action OnTutorialClose;
        // Start is called before the first frame update
        void Start()
        {
            scene = SceneManager.GetActiveScene();
            //StartCoroutine(StartTutorial());
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SetTutorial(){
            bool isTutorialLoaded = false;
            //PlayerPrefs.DeleteAll();
            switch (scene.name)
            {
                case "GravityFrog":
                    if( PlayerPrefs.GetInt("Frog") == 0){
                        isTutorialLoaded = true;
                    }
                    break;
                case "PigeonShooter":
                    if( PlayerPrefs.GetInt("Pigeon") == 0)
                        isTutorialLoaded = true;
                    break;
                case "Plane_Scene":
                    if( PlayerPrefs.GetInt("Plane") == 0)
                        isTutorialLoaded = true;
                    break;
                case "Skate_Scene":
                    if( PlayerPrefs.GetInt("Skate") == 0)
                        isTutorialLoaded = true;
                    break;
                case "Swordsman Scene":
                    if( PlayerPrefs.GetInt("Sword") == 0)
                        isTutorialLoaded = true;
                    break;  
            }
            if(isTutorialLoaded){
                ShowTutorial();
            }
            else{
                if(OnTutorialClose!=null){
                OnTutorialClose();
                }
            }
        }

        public void ShowTutorial()
        {
            tutorialTxt.text = inputField;
            Panel.SetActive(true);
            Time.timeScale = 0f;
        }

        public void CloseTutorial()
        {
            switch (scene.name)
            {
                case "GravityFrog":
                    PlayerPrefs.SetInt("Frog",1);
                    break;
                case "PigeonShooter":
                    PlayerPrefs.SetInt("Pigeon",1);
                    break;
                case "Plane_Scene":
                    PlayerPrefs.SetInt("Plane",1);
                    break;
                case "Skate_Scene":
                    PlayerPrefs.SetInt("Skate",1);
                    break;
                case "Swordsman Scene":
                    PlayerPrefs.SetInt("Sword",1);
                    break;  
            }
            Panel.SetActive(false);
            Time.timeScale = 1f;

            GameManager.Instance.SoundManager.PlayUIClick(SoundLabels.BAD);

            if (OnTutorialClose != null)
            {
                OnTutorialClose();
            }
        }

        /*protected IEnumerator StartTutorial(){
                PlayerPrefs.DeleteAll();
             yield return new WaitForSeconds(0.5f);
             SetTutorial();
        }*/
    }
}
