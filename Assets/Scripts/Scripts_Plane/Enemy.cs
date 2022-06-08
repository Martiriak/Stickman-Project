using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stickman.Managers;

namespace Stickman
{
    public class Enemy : MonoBehaviour
    {
        private float m_enemySpeed = 0f;
        public float EnemySpeed{
            get => m_enemySpeed;
            set => m_enemySpeed = value;
        }

        private void OnEnable()
        {
            GameManager.Instance.SpeedManager.OnSpeedChange += MoveEnemyWithSpeed;
        }

        private void OnDisable()
        {
            GameManager.Instance.SpeedManager.OnSpeedChange -= MoveEnemyWithSpeed;
        }


        private void MoveEnemyWithSpeed(float speed)
        {
            Vector3 movement = -Vector2.right * speed * Time.deltaTime;
            transform.Translate(movement);
        }
    }
}
