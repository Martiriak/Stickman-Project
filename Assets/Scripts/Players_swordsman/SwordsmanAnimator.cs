using UnityEngine;

namespace Stickman.Players.Animators
{
    [RequireComponent(typeof(Swordsman))]
    public class SwordsmanAnimator : MonoBehaviour
    {
        [SerializeField] private Animator m_swordsmanAnimator;

        private Swordsman m_swordsman;
        private string m_currentState = "";

        private void Awake()
        {
            m_swordsman = GetComponent<Swordsman>();
        }

        private void OnEnable()
        {
            m_swordsman.OnRunning += SetRunningState;
            m_swordsman.OnSwinging += SetSwingingState;
        }

        private void OnDisable()
        {
            m_swordsman.OnRunning -= SetRunningState;
            m_swordsman.OnSwinging -= SetSwingingState;
        }

        private void SetRunningState()
        {
            if (m_currentState.Equals("Running")) return;

            m_swordsmanAnimator.Play("Running");

            m_currentState = "Running";
        }

        private void SetSwingingState()
        {
            if (m_currentState.Equals("Swinging")) return;

            m_swordsmanAnimator.Play("Swinging");

            m_currentState = "Swinging";
        }
    }
}
