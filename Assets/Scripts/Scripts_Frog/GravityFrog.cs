using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //Respawn deathTrigger;
        GameGUI life;
        [SerializeField]
        private GameObject canvas;
        void Start()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            gravity = false;
            //deathTrigger = GameObject.FindGameObjectWithTag("Death").GetComponent<Respawn>();
            life = canvas.GetComponent<GameGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
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
            DestroyPlayer();
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
                life.HitDamage();
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                anim.SetBool("Hit", false);
                Debug.Log("Obstacle Out");
            }
        }

        public void OnDeathTrigger()
        {
            anim.SetBool("Hit", true);
            life.HitDamage();
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
            if(life.getLifes() == 0){
                Destroy(gameObject);
            }
        }

    }
}
