using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stickman
{
    public class PushTrap : MonoBehaviour
    {
        private enum PushTrapType { CLOUD , FAN }; 
        [SerializeField] private float fanPushingForce = 10f;
        [SerializeField] private PushTrapType type;
        void OnTriggerEnter2D(Collider2D collider){
            if(collider.CompareTag("Player")){
                Debug.Log("FAN");
                Rigidbody2D playerRig = collider.GetComponent<Rigidbody2D>();
                playerRig.velocity = Vector3.zero;
                playerRig.AddForce( Vector2.up * fanPushingForce , ForceMode2D.Impulse);
            }
        }

        void Push(Rigidbody2D playerRig){
            playerRig.velocity = Vector3.zero;
            if(type== PushTrapType.FAN)
                playerRig.AddForce( Vector2.up * fanPushingForce , ForceMode2D.Impulse);
            else
                playerRig.AddForce( -Vector2.up * fanPushingForce , ForceMode2D.Impulse);
        }

    }
}
