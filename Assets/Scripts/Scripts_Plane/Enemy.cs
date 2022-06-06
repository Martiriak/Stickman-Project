using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stickman.Managers;

namespace Stickman
{
    public class Enemy : MonoBehaviour
    {
        private void Update()
        {
            Vector3 movement = -Vector2.right * GameManager.Instance.SpeedManager.EvaluateSpeed() * Time.deltaTime;
            transform.Translate(movement);
        }


    }
}
