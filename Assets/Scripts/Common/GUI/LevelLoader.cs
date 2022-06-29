using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Stickman.Managers;

namespace Stickman
{
    public class LevelLoader : MonoBehaviour
    {
        enum Characters { FROG , GUNGUY , PIG , SKATEGUY , SWORDGUY }
        [SerializeField]
        public Animator transition;
        [SerializeField]
        public Sprite[] images;
        GameObject randomCharacterPanel, selectedCharacterPanel;
        int selectedCharacterIndex;
        int previousScene;
        Dictionary<Characters , int> charactersMapScenes = new Dictionary<Characters, int>();
        
        private void Awake()
        {
            previousScene =  GameManager.Instance.CurrentLoadedScene;
            randomCharacterPanel = GameObject.Find("RandomChar");
            selectedCharacterPanel = GameObject.Find("SelectedChar");
        }
        private void Start() {
            FillCharacterMapScene();
            EnableCharacterPanel(false);
            InizializeCharacterPanel();
            StartCoroutine(LoadNewLevel());
        }
        IEnumerator LoadNewLevel()
        {
            yield return new WaitForSeconds(5f);
            EnableCharacterPanel(true);
            yield return new WaitForSeconds(2f);
            transition.SetBool("End" , true);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(PickCharacterScene(selectedCharacterIndex));
        }


        private int PickCharacterScene(int index){
            switch(index){
                case 0:
                    return charactersMapScenes[Characters.FROG];
                case 1:
                    return charactersMapScenes[Characters.GUNGUY];
                case 2:
                    return charactersMapScenes[Characters.PIG];
                case 3:
                    return charactersMapScenes[Characters.SKATEGUY];
                case 4:
                    return charactersMapScenes[Characters.SWORDGUY];
                default:
                    return 0;
            }
        }
        
        
        private void FillCharacterMapScene(){
            charactersMapScenes.Add(Characters.FROG, 2);
            charactersMapScenes.Add(Characters.GUNGUY, 3);
            charactersMapScenes.Add(Characters.PIG, 4);
            charactersMapScenes.Add(Characters.SKATEGUY, 5);
            charactersMapScenes.Add(Characters.SWORDGUY, 6);
        }

        private void EnableCharacterPanel(bool setting){
            randomCharacterPanel.SetActive(!setting);
            selectedCharacterPanel.SetActive(setting);
        }        
        private void PickRandomCharacter()
        {
            selectedCharacterIndex = Random.Range(0,5);    
        }
        private void InizializeCharacterPanel(){  
            do
                PickRandomCharacter();
            while( previousScene == PickCharacterScene(selectedCharacterIndex));
            selectedCharacterPanel.GetComponent<SpriteRenderer>().sprite = images[selectedCharacterIndex];
        }

        

    }
}
