using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stickman.Managers;
using Stickman.Player;

namespace Stickman
{
    public class GravityFrog : PlayerBase
    {
        [SerializeField]
        private AnimationClip[] animationClips;

        public Animator anim;
        private bool gravity = true;
        [SerializeField]
        private float forceGravity = 9.8f;
        private Rigidbody2D rb;

        //Respawn deathTrigger;

        //public Action<int> OnLifeChange;

        void Awake()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            //gravity = true;
            //deathTrigger = GameObject.FindGameObjectWithTag("Death").GetComponent<Respawn>();

            // To be removed
           //GameManager.Instance.LivesManager.ResetLife();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (gravity)
               {
                    if (anim.GetBool("Gravity"))
                    {
                        Physics2D.gravity = new Vector2(0, -forceGravity);
                        anim.SetBool("Gravity", false);
                    }
                    else
                    {
                        Physics2D.gravity = new Vector2(0, forceGravity);
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

        protected override void OnLanding()
        {
            gravity = true;
        }


        //void OnCollisionEnter2D(Collision2D collision)
        //{
        //    if (collision.gameObject.CompareTag("Platform"))
        //    {
        //        gravity = true;
        //    }
        //}

        /*void OnTriggerEnter2D(Collider2D collision)
        {
           if (collision.gameObject.CompareTag("Gravity"))
           {
            gravity = false;
           }
        }*/

        //void OnTriggerExit2D(Collider2D collision)
        //{
        //    Debug.Log("COLLISIONE CON ...");
        //    if (collision.gameObject.CompareTag("Obstacle"))
        //    {
        //        Debug.Log("COLLISIONE CON OBSTACLE");
        //        anim.SetBool("Hit", false);
        //        GameManager.Instance.LivesManager.RemoveLife();
        //        Debug.Log("Obstacle Out");
        //    }
        //}

        /*public void OnDeathTrigger()
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
        }*/

    }
}
