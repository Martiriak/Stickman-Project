using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stickman
{
    public class GrindTrigger : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_grindTile, m_player;
        private Collider2D m_grindCollider;
        private Collider2D m_grindTrigger;

        

        // Start is called before the first frame update
        void Awake()
        {
            m_player = GameObject.Find("Player");
            m_player.GetComponent<SkatePlayer>().GrindOn += ActivateGrinds;
            m_player.GetComponent<SkatePlayer>().GrindOff += DeactivateGrinds;
            m_grindCollider = m_grindTile.GetComponent<BoxCollider2D>();
            m_grindTrigger = gameObject.GetComponent<BoxCollider2D>();
        }


        private void ActivateGrinds(){
            if(m_grindTrigger != null && m_grindCollider!= null){
                m_grindTrigger.enabled =false; 
                m_grindCollider.enabled = true;
            }
        }

        private void DeactivateGrinds(){
            if(m_grindTrigger != null && m_grindCollider!= null){
                m_grindCollider.enabled = false;
                m_grindTrigger.enabled = true; 
            }
        }
    }
}
