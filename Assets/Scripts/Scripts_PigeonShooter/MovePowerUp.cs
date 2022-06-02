using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stickman
{
    public class MovePowerUp : MonoBehaviour
    {
        private Transform targetPlayer;
        private float speed = 10f;

        private void Awake()
        {
            targetPlayer = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        void FixedUpdate()
        {
                MoveTowardsPlayer();
        }

        void MoveTowardsPlayer()
        {
            Vector3 direction = (targetPlayer.position - this.transform.position).normalized;
            transform.Translate(direction * Time.fixedDeltaTime * speed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                ActivatePowerUp();
            }
        }

        private void ActivatePowerUp()
        {
            Debug.Log("Activate Power Up !!!");
            // ...
            Destroy(gameObject);
        }
    }
}
