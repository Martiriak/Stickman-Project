using UnityEngine;

namespace Stickman.Managers.Speed
{
    public class SpeedManager : MonoBehaviour
    {
        [SerializeField] private float m_maxSpeedTimestamp = 10f;
        [SerializeField] private float m_minSpeed = 1f;
        [SerializeField] private float m_maxSpeed = 10f;
        [SerializeField] private AnimationCurve m_speedCurve;

        private float c_speedLimitsIntervalLenght;
        private void Awake() => c_speedLimitsIntervalLenght = m_maxSpeed - m_minSpeed;

        public float EvaluateSpeed()
        {
            float curveProgress = Mathf.Clamp01(m_timer / m_maxSpeedTimestamp);
            float normalizedVelocity = m_speedCurve.Evaluate(curveProgress);

            // Maps the value obtained from the curve from 0..1 to min..max and returns it.
            return normalizedVelocity * c_speedLimitsIntervalLenght + m_minSpeed;
        }


        private void OnValidate()
        {
            if (m_maxSpeedTimestamp < 1f) m_maxSpeedTimestamp = 1f;
            if (m_minSpeed < 1f) m_minSpeed = 1f;
            if (m_maxSpeed < m_minSpeed) m_maxSpeed = m_minSpeed;

            c_speedLimitsIntervalLenght = m_maxSpeed - m_minSpeed;
        }


        // TEMPORANEO! Serve un time tracker separato, perché sarà utile
        // per robe tipo il punteggio etc.
        private float m_timer = 0f;
        private void Update()
        {
            if (m_timer < m_maxSpeedTimestamp)
                m_timer += Time.deltaTime;
        }
    }
}
