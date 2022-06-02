using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stickman.Managers;

namespace Stickman.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerBase : MonoBehaviour
    {

        private Transform startingPos = null;

        void Start()
        {
            // In LivesManager e' presente un evento/canale/azione chiamato OnLifeChange.
            // Funzioni si possono collegare a questo evento, rimanendo in ascolto per eventuali cambiamenti alla vita del player.
            // La riga sotto indica che UpdateLivesUI (che aggiorna UI vita) si collega a OnLifeChange (aka chiamata quando OnLifeChange viene chiamata).
            GameManager.Instance.LivesManager.OnLifeChange += BlinkPlayerSprite;
        }

        private void OnDestroy()
        {
            // Funzione UpdateLivesUI si scollega da OnLifeChange
            GameManager.Instance.LivesManager.OnLifeChange -= BlinkPlayerSprite;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnterBehaviuor(collision);
        }

        virtual protected void CollisionEnterBehaviuor(Collision2D collision){
            Debug.Log("ON_COLLISION_ENTER");
            if (collision.gameObject.CompareTag("Platform"))
            {
                OnLanding();
            }
        }
        protected virtual void OnLanding() { }

        void OnCollisionExit2D(Collision2D collision)
        {
            CollisionExitBehaviuor(collision);
        }

        virtual protected void CollisionExitBehaviuor(Collision2D collision){
            Debug.Log("ON_COLLISION_EXIT");
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerEnterBehaviuor(collision);
        }

        virtual protected void TriggerEnterBehaviuor(Collider2D collision){
            Debug.Log("ON_TRIGGER_ENTER");
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                //anim.SetBool("Hit", true);
                //StartCoroutine(Blink());
                GameManager.Instance.LivesManager.RemoveLife();
                Debug.Log("FORSE HO RIDOTTO VITA");
            }
            if(collision.CompareTag("KillZone")){
                StartCoroutine(Respawn());
            }
        }

        virtual protected void OnTriggerExit2D(Collider2D collision)
        {
            TriggerExitBehaviuor(collision);
        }

        virtual protected void TriggerExitBehaviuor(Collider2D collision){
            Debug.Log("ON_TRIGGER_EXIT");
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("COLLISIONE CON OBSTACLE");
                //anim.SetBool("Hit", false);
                //GameManager.Instance.LivesManager.RemoveLife();
                Debug.Log("Obstacle Out");
            }
        }

        private void BlinkPlayerSprite(int i)
        {
            StartCoroutine(Blink());
        }

        protected IEnumerator Blink(){
            Debug.Log("Blink");
            for(int i=0 ; i<5 ; i++){
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                yield return new WaitForSeconds(0.1f);
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                yield return new WaitForSeconds(0.1f);
            }
        }

        virtual protected IEnumerator Respawn(){
            Rigidbody2D rig = gameObject.GetComponent<Rigidbody2D>();
            Transform startingPos = GameObject.Find("StartPosition").GetComponent<Transform>();
            if(startingPos!=null){
                gameObject.transform.position = startingPos.position;
                rig.bodyType = RigidbodyType2D.Static;
                StartCoroutine(Blink());
                yield return new WaitForSeconds(1.5f);
                rig.bodyType = RigidbodyType2D.Dynamic;
            }
        }
        
    }
}
