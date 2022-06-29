using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stickman.Player;
using Stickman.Managers;
using Stickman.Managers.Sound;


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
            //if (Input.GetButtonDown("Fire1")){
            if (Input.GetMouseButtonDown(0)){
                animator.Play("PigFlyFlap");
                GameManager.Instance.SoundManager.PlayJumpSound();
                //rig.velocity = Vector3.zero;
                PushPlane(PushDirection.UP , planeFloatingForce);
            }
        }

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
    }
}
