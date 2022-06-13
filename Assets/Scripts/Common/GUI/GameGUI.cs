using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Stickman.Managers;

namespace Stickman
{
    public class GameGUI : MonoBehaviour
    {
        public TextMeshProUGUI timeTxt;
        public TextMeshProUGUI lifesTxt;
        public TextMeshProUGUI scoreTxt;
        public TextMeshProUGUI bestScoreTxt;
        [SerializeField] private float time;

        void Start()
        {
            // In LivesManager e' presente un evento/canale/azione chiamato OnLifeChange.
            // Funzioni si possono collegare a questo evento, rimanendo in ascolto per eventuali cambiamenti alla vita del player.
            // La riga sotto indica che UpdateLivesUI (che aggiorna UI vita) si collega a OnLifeChange (aka chiamata quando OnLifeChange viene chiamata).
            GameManager.Instance.LivesManager.OnLifeChange += UpdateLivesUI;
            Debug.Log("START LIVES UI CHIAMATA");
            lifesTxt.text = GameManager.Instance.LivesManager.GetLivesLeft().ToString();

            if (PlayerPrefs.GetFloat("BestScore") == 0f)
            {
                PlayerPrefs.SetFloat("BestScore", 0f);
            }
        }

        private void OnDestroy()
        {
            // Funzione UpdateLivesUI si scollega da OnLifeChange
            GameManager.Instance.LivesManager.OnLifeChange -= UpdateLivesUI;
        }

        private void UpdateLivesUI(int lives)
        {
            Debug.Log("UPDATE LIVES UI CHIAMATA");
            lifesTxt.text = lives.ToString();
        }

        void Update()
        {
            /// Ok for now ... but "DistanceTravelledManager" would be better
            //if (GameManager.Instance.LivesManager.GetLivesLeft() > 0)
            //{
            //    time += Time.deltaTime;
            //    timeTxt.text = time.ToString("F2");
            //}
             if (GameManager.Instance.LivesManager.GetLivesLeft() > 0)
            {
                timeTxt.text = $"{GameManager.Instance.TimeTracker.TotalStopWatch.ToString("F2")}";
            }

            if(GameManager.Instance.LivesManager.GetLivesLeft() == 0){
                Score();
            }
        }

        public void Score(){
            PlayerPrefs.SetFloat("HighScore", GameManager.Instance.TimeTracker.TotalStopWatch);
            if(PlayerPrefs.GetFloat("HighScore")>PlayerPrefs.GetFloat("BestScore")){
                 PlayerPrefs.SetFloat("BestScore",PlayerPrefs.GetFloat("HighScore"));
            }
            scoreTxt.text = PlayerPrefs.GetFloat("HighScore").ToString("F2");
            bestScoreTxt.text = PlayerPrefs.GetFloat("BestScore").ToString("F2");
        }

        

    }
}
