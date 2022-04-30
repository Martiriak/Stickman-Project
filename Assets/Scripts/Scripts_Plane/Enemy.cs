using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stickman
{
    public class Enemy : MonoBehaviour
    {
        private float m_enemySpeed = 0f;
        public float EnemySpeed{
            get => m_enemySpeed;
            set => m_enemySpeed = value;
        }
        private void Update()
        {
            Vector3 movement = -Vector2.right * m_enemySpeed * Time.deltaTime;
            transform.Translate(movement);
        }


    }
}
