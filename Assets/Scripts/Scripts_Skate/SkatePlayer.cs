using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Stickman.Player;
using Stickman.Managers;
using Stickman.Managers.Sound;

namespace Stickman
{
    public class SkatePlayer : PlayerBase
    {
        enum SkateState {LANDED , JUMPING , CANGRIND}
        private Rigidbody2D m_rig;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float m_minJumpForce = 2f;
        [SerializeField]
        private float m_maxJumpForce = 10f;
        [SerializeField]
        private float m_doubleJumpForce = 5f;
        [SerializeField]
        private GameObject m_grindSparkling;
        [SerializeField]
        private GrindTrigger currentGrind = null;

        private SoundManager m_soundManager;

        //[SerializeField]
        //private Transform m_startingPos = null;
        private float m_accumulatedJumpForce;
        private bool m_hasDoubleJumped = false;

        [SerializeField] private SkateState m_playerState = SkateState.JUMPING;

        [SerializeField]
        private GameObject grindPopUp ; 

        private void Awake()
        {
            m_rig = gameObject.GetComponent<Rigidbody2D>();
            m_accumulatedJumpForce = m_minJumpForce;
            m_soundManager = GameManager.Instance.SoundManager;
            m_soundManager.SetupPlayerSoundInstance(PlayerTypes.SKATE);
        }

        private void Update()
        {
           switch (m_playerState) {
               case SkateState.JUMPING:
                    DoubleJump();
                    break;
                case SkateState.LANDED:
                    SkateJump();
                    break;
                case SkateState.CANGRIND:
                    Grind();
                    break;
           }
        }

        private void SkateJump()
        {
            if(Input.GetMouseButton(0)){
                m_accumulatedJumpForce += Time.deltaTime*10;
            }
            if (Input.GetMouseButtonUp(0)){
                m_soundManager.PlaySkateSound(PlayerActions.JUMP);
                if(m_accumulatedJumpForce >= m_maxJumpForce)
                    m_rig.AddForce( Vector2.up * m_maxJumpForce , ForceMode2D.Impulse);
                else
                    m_rig.AddForce( Vector2.up * m_accumulatedJumpForce , ForceMode2D.Impulse);
                m_accumulatedJumpForce = m_minJumpForce;
            }
        }

        private void DoubleJump()
        {
            if (Input.GetMouseButtonDown(0))
                if(!m_hasDoubleJumped){
                    m_soundManager.PlaySkateSound(PlayerActions.JUMP);
                    m_rig.velocity = Vector3.zero;
                    m_rig.AddForce( Vector2.up * m_doubleJumpForce , ForceMode2D.Impulse);
                    m_hasDoubleJumped = true;
                }
        }

        private void Grind(){
            if(Input.GetMouseButtonDown(0))
                if(currentGrind!=null)
                    currentGrind.ActivateGrinds();
        }

        private void Landing(){
            m_playerState = SkateState.LANDED;
            animator.SetBool("Jumping" , false);
            m_hasDoubleJumped = false;
        }

        private void Jumping(){
            m_soundManager.PlaySkateSound(PlayerActions.STOP);
            animator.SetBool("Jumping" , true);
            m_playerState = SkateState.JUMPING;
        }
        void SkateRespawn(){
            m_playerState = SkateState.JUMPING;
            m_hasDoubleJumped = false;
            /*gameObject.transform.position = m_startingPos.position;
            m_rig.bodyType = RigidbodyType2D.Static;
            StartCoroutine(Blink());
            yield return new WaitForSeconds(1.5f);
            m_rig.bodyType = RigidbodyType2D.Dynamic;*/
            StartCoroutine(Respawn());
        }

       /* IEnumerator Blink(){
            for(int i=0 ; i<4 ; i++){
                yield return new WaitForSeconds(0.1f);
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                yield return new WaitForSeconds(0.1f);
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }*/


        //private void OnCollisionEnter2D(Collision2D other){
        override protected void CollisionEnterBehaviuor(Collision2D other){
            if(other.gameObject.CompareTag("Floor")){
                m_soundManager.PlaySkateSound(PlayerActions.LAND);
                Landing();    
            }
            if(other.gameObject.CompareTag("Grind")){
                m_soundManager.PlaySkateSound(PlayerActions.GRIND);
                m_grindSparkling.SetActive(true);
                Landing();
            }
        }

        //private void OnCollisionExit2D(Collision2D other){
        override protected void CollisionExitBehaviuor(Collision2D other){
            if(other.gameObject.CompareTag("Floor")){
                Jumping();
            }
            if(other.gameObject.CompareTag("Grind")){
                Jumping();
                m_grindSparkling.SetActive(false);
            }
        }

        //private void OnTriggerEnter2D(Collider2D other){
        override protected void TriggerEnterBehaviuor(Collider2D other){
            if(other.CompareTag("GrindTrigger")){
                grindPopUp.SetActive(true);
                currentGrind = other.gameObject.GetComponent<GrindTrigger>();
                m_playerState = SkateState.CANGRIND;   
            }
            if(other.CompareTag("KillZone")){
                GameManager.Instance.LivesManager.RemoveLife();
                SkateRespawn();
            }
            if(other.CompareTag("Obstacle")){
                StartCoroutine(Blink());
                GameManager.Instance.LivesManager.RemoveLife();
            }
            if(other.gameObject.layer == LayerMask.NameToLayer("Prop")){
                HandlePropFromObject(other.gameObject);
            }
        }
       // private void OnTriggerExit2D(Collider2D other){
        override protected void TriggerExitBehaviuor(Collider2D other){   
            if(other.CompareTag("GrindTrigger")){
                grindPopUp.SetActive(false);
                currentGrind = null;
                m_playerState = SkateState.JUMPING;   
            }
        }

        /*  private void OnTriggerStay2D(Collider2D other){
              if(other.CompareTag("GrindTrigger"))
                  if(Input.GetButton("Fire1"))
                      other.gameObject.GetComponent<GrindTrigger>().ActivateGrinds();  
          }*/


        private void OnValidate()
        {
            if (m_minJumpForce > m_maxJumpForce)
                m_minJumpForce = m_maxJumpForce;
        }

        protected override void OnDestroy()
        {
            m_soundManager.PlaySkateSound(PlayerActions.STOP);
            m_soundManager.ReleasePlayerSoundInstance(); 
            base.OnDestroy();
        }
    }
}
