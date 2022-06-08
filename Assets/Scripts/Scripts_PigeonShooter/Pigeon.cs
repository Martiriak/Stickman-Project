using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stickman.pigeonShooter
{
    /// <summary>
    /// Script attached to Pigeon enemy.
    /// Pigeon moves towards player. 
    /// When hit by bullet dies, spawning PigeonFall & particle effect.
    /// </summary>
    public class Pigeon : MonoBehaviour
    {
        private int life = 1;

        [SerializeField] private GameObject pigeonFall;
        [SerializeField] private GameObject pigeonFeathers;
        [SerializeField] private GameObject boxPowerUp;

        [SerializeField] private  bool isPowerUp;

        private Transform targetPlayer;
        private float speed = 4f;
        private float dashSpeed = 12f;
        private Vector3 dashVector;
        private bool isDashing;
        private bool isSurvived;
        private bool isAttacking;
        

        private void Awake()
        {
            targetPlayer = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        public void Start()
        {
            Destroy(this.gameObject, 8); // bird dies after 8 sec
            isDashing = false; isSurvived = false; isAttacking = false;
            // ANIMATION: idle
        }

        void FixedUpdate()
        {
            
            if ((this.transform.position - targetPlayer.position).magnitude > 2.9f && isDashing == false)
            {
                MoveTowardsPlayer();
            }
            else
            {
                DashTroughPlayer();
            }
        }

        void MoveTowardsPlayer()
        {
            Vector3 direction = (targetPlayer.position - this.transform.position).normalized;
            transform.Translate(direction * Time.fixedDeltaTime * speed);
        }

        void DashTroughPlayer()
        {
            if (!isDashing)
            {
                // ANIMATION: dash
                dashVector = (targetPlayer.position - this.transform.position).normalized;
                isDashing = true;
            }

            if (this.transform.position.x > targetPlayer.position.x)
            {
                transform.Translate(dashVector * Time.fixedDeltaTime * dashSpeed);
            }
            else if (!isAttacking)
            {
                isAttacking = true;
                // DEAL DAMAGE TO PLAYER
            }
            else if ((this.transform.position - targetPlayer.position).magnitude < 1.3f)
            {
                transform.Translate(dashVector * Time.fixedDeltaTime * dashSpeed);
            }
            else
            {
                transform.Translate((Vector3.left + Vector3.up).normalized * Time.fixedDeltaTime * speed);
                if (!isSurvived)
                {
                    isSurvived = true;
                    // ANIMATION: idle
                }
            }


        }

        void TakeDamage()
        {
            life -= 1;
            if (life <= 0 && !isDashing)
            {
                //Instantiate(pigeonFall, this.transform.position, Quaternion.identity);
                ObjectPooler.Instance.SpawnFromPool("PigeonFall", this.transform.position, Quaternion.identity);
                Instantiate(pigeonFeathers, this.transform.position, Quaternion.identity);
                if (isPowerUp) Instantiate(boxPowerUp, this.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                TakeDamage();
            }
        }
    }
}

