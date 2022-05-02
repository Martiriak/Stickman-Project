using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Stickman
{
    public class SkatePlayer : MonoBehaviour
    {
        enum SkateState {LANDED , JUMPING , CANGRIND}
        private Rigidbody2D m_rig;
        [SerializeField]
        private float m_maxJumpForce = 10f;
        [SerializeField]
        private float m_accumulatedJumpForce = 0f;
        [SerializeField]
        private float m_doubleJumpForce = 5f;
        [SerializeField]
        private GameObject m_grindSparkling;
        [SerializeField]
        private GrindTrigger currentGrind = null;
        private bool m_hasDoubleJumped = false;
        private bool m_landed = false;
        private bool m_canGrind = false;

        private SkateState m_playerState = SkateState.JUMPING;

        void Awake()
        {
            m_rig = gameObject.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
           switch (m_playerState) {
               case SkateState.JUMPING:
                    gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    DoubleJump();
                    break;
                case SkateState.LANDED:
                    gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    SkateJump();
                    break;
                case SkateState.CANGRIND:
                    gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                    Grind();
                    break;
           }

           /*if(!m_canGrind){
               if(m_landed)
                    SkateJump();
                else    
                    DoubleJump();
           }
           if(m_canGrind)
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            else
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;*/
        }

        private void SkateJump(){
            if(Input.GetButton("Fire1")){
                m_accumulatedJumpForce += Time.deltaTime*10;
                //Debug.Log("JUMP!");
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
                   // Debug.Log("DOUBLEJUMP!");
                }
        }

        private void Grind(){
            if(Input.GetButton("Fire1"))
                if(currentGrind!=null)
                    currentGrind.ActivateGrinds();
        }

        private void Landing(){
            m_playerState = SkateState.LANDED;
            m_landed = true;
            m_hasDoubleJumped = false;
            m_canGrind = false;
        }


        private void OnCollisionEnter2D(Collision2D other){
            if(other.gameObject.CompareTag("Floor"))
                Landing();
            if(other.gameObject.CompareTag("Grind")){
                m_grindSparkling.SetActive(true);
                Landing();
            }
        }

        private void OnCollisionExit2D(Collision2D other){
            if(other.gameObject.CompareTag("Floor"))
                m_landed = false;
                m_playerState = SkateState.JUMPING;
            if(other.gameObject.CompareTag("Grind")){
                m_playerState = SkateState.JUMPING;
                m_landed = false;
                m_grindSparkling.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other){
            if(other.CompareTag("GrindTrigger")){
               currentGrind = other.gameObject.GetComponent<GrindTrigger>();
               m_playerState = SkateState.CANGRIND;   
               //m_canGrind = true;   
            }
        }
        private void OnTriggerExit2D(Collider2D other){
            if(other.CompareTag("GrindTrigger")){
                currentGrind = null;
                m_playerState = SkateState.JUMPING;   
               //m_canGrind = false;   
            }
        }

      /*  private void OnTriggerStay2D(Collider2D other){
            if(other.CompareTag("GrindTrigger"))
                if(Input.GetButton("Fire1"))
                    other.gameObject.GetComponent<GrindTrigger>().ActivateGrinds();  
        }*/

    }
}
