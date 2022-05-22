using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stickman
{
    /// <summary>
    /// It keeps track of player health and displays it accordingly.
    /// Methods can be called by other scripts: LivesManager.Instance.SomeMethod();
    /// </summary>
    public class LivesManager : MonoBehaviour
    {
        public static LivesManager Instance { get; private set; }
        private int livesLeft;

        public RawImage life1;
        public RawImage life2;
        public RawImage life3;

        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            ResetLife();
        }

        public void ResetLife()
        {
            livesLeft = 3;
            DisplayLife();
        }

        public void RemoveLife()
        {
            livesLeft -= 1;
            if (livesLeft <= 0)
            {
                // call something like ... GameManager.Instance.GameOver();
            }
            DisplayLife();
        }

        public void AddLife()
        {
            if (livesLeft < 3)
            {
                livesLeft += 1;
            }
            DisplayLife();
        }

        public void DisplayLife()
        {
            if (livesLeft == 3)
            {
                life1.color = new Vector4(255, 255, 255, 255);
                life2.color = new Vector4(255, 255, 255, 255);
                life3.color = new Vector4(255, 255, 255, 255);
            }
            if (livesLeft == 2)
            {
                life1.color = new Vector4(255, 255, 255, 255);
                life2.color = new Vector4(255, 255, 255, 255);
                life3.color = new Vector4(0, 0, 0, 200);
            }
            if (livesLeft == 1)
            {
                life1.color = new Vector4(255, 255, 255, 255);
                life2.color = new Vector4(0, 0, 0, 200);
                life3.color = new Vector4(0, 0, 0, 200);

            }
            if (livesLeft == 0)
            {
                life1.color = new Vector4(0, 0, 0, 200);
                life2.color = new Vector4(0, 0, 0, 200);
                life3.color = new Vector4(0, 0, 0, 200);
            }
        }

    }

}
