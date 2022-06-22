using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stickman.Managers.Sound
{
    /// <summary>
    /// Controls the sound of the game and its evolution.
    /// </summary>

    public enum SoundLabels{
        GOOD , BAD
    }
    public enum PlayerTypes{
        SKATE , BAD
    }

    public enum PlayerActions{
        LAND , GRIND , JUMP , STOP
    }


    public class SoundManager : MonoBehaviour
    {    // Start is called before the first frame update
    
        private FMOD.Studio.EventInstance  m_instanceGuiClick;
        private FMOD.Studio.EventInstance  m_instancePlayer;
        void Start()
        {
            m_instanceGuiClick = FMODUnity.RuntimeManager.CreateInstance("event:/UI_Click");
        }
        private void OnDestroy()
        {
            m_instanceGuiClick.release();
        }
        // Update is called once per frame
        void Update()
        {
        
        }

        public void PlayUIClick(SoundLabels type){
           switch (type) {
            case SoundLabels.GOOD:
                m_instanceGuiClick.setParameterByName("Click_Choice", 0);
                break;
            case SoundLabels.BAD:
                m_instanceGuiClick.setParameterByName("Click_Choice", 1);
                break;
            default :
                break;
           }
            m_instanceGuiClick.start();
        } 

        public void SetupPlayerSoundInstance(PlayerTypes type){
           switch (type) {
            case PlayerTypes.SKATE:
                m_instancePlayer = FMODUnity.RuntimeManager.CreateInstance("event:/Skate_Movement");
                break;
            default :
                
                break;
           }
        }
        public void ReleasePlayerSoundInstance(){
            if(m_instancePlayer.isValid())
                m_instancePlayer.release();
        }
        public void PlaySkateSound(PlayerActions type){
           switch (type) {
            case PlayerActions.LAND:
                m_instancePlayer.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                m_instancePlayer.setParameterByName("Skate_State", 0);
                m_instancePlayer.start();
                break;
            case PlayerActions.GRIND:
                m_instancePlayer.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                m_instancePlayer.setParameterByName("Skate_State", 1);
                m_instancePlayer.start();
                break;
            case PlayerActions.JUMP:
                PlayJumpSound();
                break;
            case PlayerActions.STOP:
                m_instancePlayer.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                break;
            default :
                
                break;
           }
            
        }
        public void PlayJumpSound() {
            FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerBase_Jump");
        }
        public void PlayDamageSound() {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Death_Zone");
        }
        public void PlayProptSound() {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Power_Up");
        }
        public void PlayLaserSound() {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Shooter_Laser");
        }
        public void PlayBreakBoxSound() {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Swordman_Crash");
        }
        public void PlaySwooshSound() {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Swordman_Swoosh");
        }
        public void PlayGameOverSound() {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Game_Over");
        }

    }
}
