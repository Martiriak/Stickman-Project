using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stickman
{
    public class Respawn : MonoBehaviour
    {
        [SerializeField] private Transform frog;
        
        private bool fallen;
        GravityFrog player;
        void Start(){
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<GravityFrog>();
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                frog.transform.position = new Vector3(0,0,0);
                Physics.SyncTransforms();
                //fallen = true;
                //player.OnDeathTrigger();
            }
        }

        public bool GetFallen(){
            return fallen;
        }

        public void SetFallenFalse(){
            fallen = false;
        }

    }
}
