using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pigeonShooter
{
    /// <summary>
    /// Script attached to Pigeon enemy.
    /// Pigeon moves towards player. 
    /// When hit by bullet dies, spawning PigeonFall & particle effect.
    /// </summary>
    public class Pigeon : MonoBehaviour
    {
        private int life = 1;
        //private int attack = 1;

        [SerializeField]
        private GameObject pigeonFall;
        [SerializeField]
        private GameObject pigeonFeathers;

        private Transform targetPlayer;
        private float speed = 5f;

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

        void TakeDamage()
        {
            life -= 1;
            if (life <= 0)
            {
                //Instantiate(pigeonFall, this.transform.position, Quaternion.identity);
                ObjectPooler.Instance.SpawnFromPool("PigeonFall", this.transform.position, Quaternion.identity);
                Instantiate(pigeonFeathers, this.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                //Destroy(other); //destroy bullet?
                TakeDamage();
            }
        }
    }
}

