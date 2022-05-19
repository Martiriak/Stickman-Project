using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pigeonShooter
{
    //[RequireComponent(typeof(Pigeon))]
    public class PigeonAnimation : MonoBehaviour
    {
        Animator animator;
        private string currentState;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            this.GetComponent<Pigeon>();
        }

        // Update is called once per frame
        void ChangeAnimationState(string newState)
        {
            // stop the same animation from interrupting itself
            if (currentState == newState) return;

            // play the animation
            animator.Play(newState);

            // reassign the current state
            currentState = newState;

        }
    }
}
