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
            Debug.Log("REMOVE LIFE CHIAMATA");
            livesLeft -= 1;
            if (livesLeft <= 0)
            {
                // GAME OVER
                // call something like ... GameManager.Instance.GameOver();
            }
            OnLifeChange?.Invoke(livesLeft);
        }

        public void AddLife()
        {
            livesLeft += 1;
            OnLifeChange?.Invoke(livesLeft);
        }

        public void DisplayLife()
        {

        }

    }

}
