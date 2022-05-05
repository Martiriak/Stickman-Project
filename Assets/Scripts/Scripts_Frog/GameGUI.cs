using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Stickman
{
    public class GameGUI : MonoBehaviour
    {
        public TextMeshProUGUI timeTxt;
        public TextMeshProUGUI lifesTxt;
        [SerializeField]
        private int lifes = 6;
        private float time;
        // Start is called before the first frame update
        void Start()
        {
            lifesTxt.text = lifes.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            if (lifes > 0)
            {
                time += Time.deltaTime;
                timeTxt.text = time.ToString("F2");
            }
        }

        public void HitDamage()
        {
            if (lifes > 0)
                --lifes;
            lifesTxt.text = lifes.ToString();
        }

        public int getLifes(){
            return lifes;
        }

    }
}
