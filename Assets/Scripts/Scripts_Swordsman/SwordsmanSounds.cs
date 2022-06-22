using UnityEngine;
using Stickman.Managers;

namespace Stickman.Players.SwordsmanUtilities
{
    public class SwordsmanSounds : MonoBehaviour
    {
        [SerializeField] private Swordsman m_swordsman;

        private void OnEnable()
        {
            if (m_swordsman != null)
            {
                m_swordsman.OnJumping += PlayJumpSound;
                m_swordsman.OnCrushingDown += PlayFallingSound;
                m_swordsman.OnSwinging += PlaySwingSound;
            }
        }

        private void OnDisable()
        {
            if (m_swordsman != null)
            {
                m_swordsman.OnJumping -= PlayJumpSound;
                m_swordsman.OnCrushingDown -= PlayFallingSound;
                m_swordsman.OnSwinging -= PlaySwingSound;
            }
        }

        
        private void PlayJumpSound() { GameManager.Instance.SoundManager.PlayJumpSound(); }
        private void PlayFallingSound() { GameManager.Instance.SoundManager.PlayJumpSound(); }
        private void PlaySwingSound() { GameManager.Instance.SoundManager.PlaySwooshSound(); }
    }
}
