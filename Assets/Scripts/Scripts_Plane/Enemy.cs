using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stickman.Managers;

namespace Stickman
{
    public class Enemy : MonoBehaviour
    {
         private void OnEnable()
        {
            GameManager.Instance.SpeedManager.OnSpeedChange += MoveLevelWithSpeed;
        }

        private void OnDisable()
        {
            GameManager.Instance.SpeedManager.OnSpeedChange -= MoveLevelWithSpeed;
        }
       
        private void MoveLevelWithSpeed(float speed)
        {
            Vector3 movement = -Vector2.right * speed * Time.deltaTime;
            transform.Translate(movement);
        }


    }
}
