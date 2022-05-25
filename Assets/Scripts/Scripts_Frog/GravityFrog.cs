using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stickman.Managers;

namespace Stickman
{
    public class GravityFrog : MonoBehaviour
    {
        [SerializeField]
        private AnimationClip[] animationClips;

        public Animator anim;
        private bool gravity;
        [SerializeField]
        private float forceGravity = 9.8f;
        private Rigidbody2D rb;

        Respawn deathTrigger;

        //public Action<int> OnLifeChange;

        void Start()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            gravity = false;
            deathTrigger = GameObject.FindGameObjectWithTag("Death").GetComponent<Respawn>();

            // To be removed
            GameManager.Instance.LivesManager.ResetLife();
        }

        void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                if (gravity)
                {
                    if (anim.GetBool("Gravity"))
                    {
                        Physics2D.gravity = new Vector3(0, -forceGravity);
                        anim.SetBool("Gravity", false);
                    }
                    else
                    {
                        Physics2D.gravity = new Vector3(0, forceGravity);
                        anim.SetBool("Gravity", true);
                    }
                    gravity = false;
                }

            }
            //DestroyPlayer();
            /*if(deathTrigger.GetFallen()){
                OnDeathTrigger();
                deathTrigger.SetFallenFalse();
            }*/
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                gravity = true;
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                anim.SetBool("Hit", true);
                Debug.Log("Obstacle In");
                GameManager.Instance.LivesManager.RemoveLife();
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("COLLISIONE CON ...");
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("COLLISIONE CON OBSTACLE");
                anim.SetBool("Hit", false);
                GameManager.Instance.LivesManager.RemoveLife();
                Debug.Log("Obstacle Out");
            }
        }

        public void OnDeathTrigger()
        {
            Debug.Log("COLLISIONE CON FUORI");
            anim.SetBool("Hit", true);
            GameManager.Instance.LivesManager.RemoveLife();
            Debug.Log("Death In");
            StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(1f);
            anim.SetBool("Hit", false);
            Debug.Log("Death Out");
        }

        private void DestroyPlayer(){
            //if (GameManager.Instance.LivesManager.GetLivesLeft() <= 0)
            //{
            //    Destroy(gameObject);
            //}
        }

    }
}
