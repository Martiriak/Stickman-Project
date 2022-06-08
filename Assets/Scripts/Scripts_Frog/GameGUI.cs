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
        [SerializeField] private float time;

        void Start()
        {
            // In LivesManager e' presente un evento/canale/azione chiamato OnLifeChange.
            // Funzioni si possono collegare a questo evento, rimanendo in ascolto per eventuali cambiamenti alla vita del player.
            // La riga sotto indica che UpdateLivesUI (che aggiorna UI vita) si collega a OnLifeChange (aka chiamata quando OnLifeChange viene chiamata).
            GameManager.Instance.LivesManager.OnLifeChange += UpdateLivesUI;
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
        }

    }
}
