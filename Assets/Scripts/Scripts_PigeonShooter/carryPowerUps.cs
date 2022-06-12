using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stickman
{
    public class carryPowerUps : MonoBehaviour
    {
        [SerializeField] private GameObject[] boxPowerUp;


        void Start()
        {
            int rand = Random.Range(0, boxPowerUp.Length);
            GameObject childPowerUp = Instantiate(boxPowerUp[rand], this.transform.position, Quaternion.identity);
            childPowerUp.transform.SetParent(this.transform);
        }
    }
}
