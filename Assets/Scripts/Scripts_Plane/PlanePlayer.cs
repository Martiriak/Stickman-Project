using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stickman.Player;

namespace Stickman
{
    public class PlanePlayer : PlayerBase
    {
        enum PushDirection { UP , DOWN}
        [SerializeField] private float planeFloatingForce = 10f;
        [SerializeField] private float cloudPushingForce = 10f;
        [SerializeField] private Animator animator;
        private Rigidbody2D rig;

        void Awake()
        {
            rig = gameObject.GetComponent<Rigidbody2D>();
        }


        void Update()
        {
            PlaneFlapping();
        }

        void PlaneFlapping(){
            if (Input.GetButtonDown("Fire1")){
                animator.Play("PigFlyFlap");
                //rig.velocity = Vector3.zero;
                PushPlane(PushDirection.UP , planeFloatingForce);
            }
        }

  /*      void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.CompareTag("Cloud")){
                PushPlane(PushDirection.DOWN, cloudPushingForce);
                Debug.Log("Cloud");
            }
           if(collider.CompareTag("Fan")){
                PushPlane(PushDirection.UP, fanPushingForce);
                Debug.Log("Fan");
            }
            if(collider.CompareTag("Obstacle")){
               StartCoroutine(Blink());
            }
        }
        */

        void PushPlane(PushDirection dir, float vel){
           switch (dir) {
               case PushDirection.UP:
                   rig.AddForce(Vector2.up * vel, ForceMode2D.Impulse);
                   break;
               case PushDirection.DOWN :
                   rig.AddForce(Vector2.down * vel, ForceMode2D.Impulse);
                   break;
           }
        }

       /* IEnumerator Blink(){
            for(int i=0 ; i<4 ; i++){
                yield return new WaitForSeconds(0.1f);
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                yield return new WaitForSeconds(0.1f);
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }*/
    

    }
}
