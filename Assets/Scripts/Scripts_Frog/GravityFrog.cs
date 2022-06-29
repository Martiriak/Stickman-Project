using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stickman.Managers;
using Stickman.Managers.Sound;
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

        void Awake()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (gravity)
               {
                    GameManager.Instance.SoundManager.PlayJumpSound();
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
        }

        protected override void OnLanding()
        {
            gravity = true;
        }

        protected override void OnDestroy(){
            Physics2D.gravity = new Vector2(0, -forceGravity);
            base.OnDestroy();

        }
    }
}
