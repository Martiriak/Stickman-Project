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
        //[SerializeField] private AnimationClip[] animationClips;
        //protected Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            //anim = GetComponent<Animator>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("ON_COLLISION_ENTER");
            if (collision.gameObject.CompareTag("Platform"))
            {
                OnLanding();
            }
        }

        protected virtual void OnLanding() { }

        void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("ON_TRIGGER_ENTER");
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                //anim.SetBool("Hit", true);
                GameManager.Instance.LivesManager.RemoveLife();
                Debug.Log("FORSE HO RIDOTTO VITA");
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("ON_TRIGGER_EXIT");
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("COLLISIONE CON OBSTACLE");
                //anim.SetBool("Hit", false);
                GameManager.Instance.LivesManager.RemoveLife();
                Debug.Log("Obstacle Out");
            }
        }
    }
}
