using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Stickman
{
    public class SkatePlayer : MonoBehaviour
    {
        private Rigidbody2D m_rig;
        [SerializeField]
        private float m_maxJumpForce = 10f;
        [SerializeField]
        private float m_accumulatedJumpForce = 0f;
        [SerializeField]
        private float m_doubleJumpForce = 5f;
        private bool m_landed = false;
        private bool m_hasDoubleJumped = false;
        private bool m_canGrind = false;

        public Action GrindOn, GrindOff;
        void Awake()
        {
            m_rig = gameObject.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if(!m_canGrind){
                if(m_landed)
                    SkateJump();
                else
                    DoubleJump();
            }
        }

        private void SkateJump(){
            if(Input.GetButton("Fire1")){
                m_accumulatedJumpForce += Time.deltaTime*10;
                Debug.Log("JUMP!");
            }
            else{
                if(m_accumulatedJumpForce >= m_maxJumpForce)
                    m_rig.AddForce( Vector2.up * m_maxJumpForce , ForceMode2D.Impulse);
                else
                    m_rig.AddForce( Vector2.up * m_accumulatedJumpForce , ForceMode2D.Impulse);
                m_accumulatedJumpForce = 0f;
            }
        }

        private void DoubleJump(){
            if(Input.GetButton("Fire1"))
                if(!m_hasDoubleJumped){
                    m_rig.AddForce( Vector2.up * m_doubleJumpForce , ForceMode2D.Impulse);
                    m_hasDoubleJumped = true;
                    Debug.Log("DOUBLEJUMP!");
                }
        }
        private void Landing(){
            m_landed = true;
            m_hasDoubleJumped = false;
            m_canGrind = false;
        }


        private void OnCollisionEnter2D(Collision2D other){
            if(other.gameObject.CompareTag("Floor")||other.gameObject.CompareTag("Grind"))
                Landing();
        }

        private void OnCollisionExit2D(Collision2D other){
            if(other.gameObject.CompareTag("Floor"))
                m_landed = false;
            if(other.gameObject.CompareTag("Grind")){
                GrindOff?.Invoke();
                m_landed = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other){
            if(other.CompareTag("GrindTrigger"))
                m_canGrind = true;
        } 

        private void OnTriggerStay2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("GrindTrigger")){
                if(Input.GetButton("Fire1"))
                   GrindOn?.Invoke();
            }
        }

    }
}
