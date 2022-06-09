using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Stickman
{
    /// <summary>
    /// It keeps track of player health and displays it accordingly.
    /// Methods can be called by other scripts: GameManager.Instance.LivesManager.SomeMethod();
    /// </summary>
    public class LivesManager : MonoBehaviour
    {
        private int livesLeft;
        public event Action<int> OnLifeChange;
        public event Action OnDamageTaken;
        public event Action OnDeath;
        private bool isInvulnerable;
        //private float invulnerabilityTime = 1.0f;

        private void Start()
        {
            ResetLife();
        }
        public int GetLivesLeft()
        {
            return livesLeft;
        }

        public void ResetLife()
        {
            livesLeft = 5;
            OnLifeChange?.Invoke(livesLeft); // same as: if (OnLifeChange != null) OnLifeChange(lifeleft);
        }

        public void RemoveLife()
        {
            if (isInvulnerable) return;
            Debug.Log("REMOVE LIFE CHIAMATA *****************************************************************");
            livesLeft -= 1;
            OnLifeChange?.Invoke(livesLeft);
            OnDamageTaken?.Invoke();
            StartCoroutine(Invulnerability(1f));
            if (livesLeft <= 0)
            {
                OnDeath?.Invoke();

                // GAME OVER
                // call something like ... GameManager.Instance.GameOver();
                return;
            }
        }

        public void AddLife()
        {
            livesLeft += 1;
            OnLifeChange?.Invoke(livesLeft);
        }

        private IEnumerator Invulnerability(float invulnerabilityTime)
        {
            isInvulnerable = true;
            Debug.Log("Life Invulnerability Coroutine Called : " + invulnerabilityTime);
            yield return new WaitForSeconds(invulnerabilityTime);
            isInvulnerable = false;

        }

        public void CallInInvulnerability(float invulnerabilityTime){
            StartCoroutine(Invulnerability(invulnerabilityTime));
        }

        public int Die(){
            if(livesLeft <= 0)
                return 0;
            return livesLeft;
        }

    }

}
