using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stickman
{
    public class PortalArea : MonoBehaviour
    {
        [SerializeField] private GameObject portal;
        private void OnTriggerStay2D(Collider2D other)
        {
            if(other.CompareTag("Player")){
                other.transform.Translate(( portal.transform.position - other.transform.position).normalized * 0.1f);
            }
        }
    }
}
