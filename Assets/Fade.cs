using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stickman
{
    public class Fade : StateMachineBehaviour
    {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("EndFade", true);
        }
    }
}
