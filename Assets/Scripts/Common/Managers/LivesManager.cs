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
        public Action<int> OnLifeChange;
        private bool isInvulnerable;
        public bool IsInvulnerable => isInvulnerable;
        private float invulnerabilityTime = 1.0f;

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
            StartCoroutine(Invulnerability());
            if (livesLeft <= 0)
            {
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

        private IEnumerator Invulnerability()
        {
            isInvulnerable = true;
            Debug.Log("Life Invulnerability Coroutine Called");
            yield return new WaitForSeconds(invulnerabilityTime);
            isInvulnerable = false;

        }

    }

}
