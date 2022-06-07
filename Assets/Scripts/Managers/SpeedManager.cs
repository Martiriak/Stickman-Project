using UnityEngine;

namespace Stickman.Managers.Speed
{
    /// <summary>
    /// Controls the speed of the game and its evolution.
    /// </summary>
    public class SpeedManager : MonoBehaviour
    {
        [Tooltip("The time after which the speed stops changing.")]
        [SerializeField] private float m_maxSpeedTimestamp = 10f;

        [Tooltip("The starting speed of the game.")]
        [SerializeField] private float m_minSpeed = 1f;

        [Tooltip("The final speed of the game.")]
        [SerializeField] private float m_maxSpeed = 10f;

        [Tooltip("Describes how much the initial speed grows at the start of each scene.\nIn Particular, at what percentage the curve is initially evaluated, incrementing each scene.")]
        [SerializeField] private int m_percentIncrement = 10;

        [Tooltip("The upper limit to the percentage increment.")]
        [SerializeField] private int m_maxPercentIncrement = 10;

        [Tooltip("Describes the evolution of the speed throughout the game: from min to max, until max timestamp.")]
        [SerializeField] private AnimationCurve m_speedCurve;


        private float m_currentIncrement = 0f;

        // Caches maxSpeed - minSpeed.
        private float c_speedLimitsIntervalLenght;
        // Caches m_maxPercentIncrement / 100f.
        private float c_maxPercentIncrementFloat;

        private void Awake()
        {
            c_speedLimitsIntervalLenght = m_maxSpeed - m_minSpeed;
            c_maxPercentIncrementFloat = m_maxPercentIncrement / 100f;
        }

        private void Start()
        {
            GameManager.Instance.TimeTracker.OnLap += (int CurrentLap) =>
            {
                Debug.Log($"Current Lap: {CurrentLap}");
                m_currentIncrement = (m_percentIncrement * (CurrentLap - 1)) / 100f;
            };
        }

        public float EvaluateSpeed()
        {
            float curveProgress = Mathf.Clamp01(m_timer / m_maxSpeedTimestamp);
            curveProgress = Mathf.Clamp(curveProgress + m_currentIncrement, 0f, c_maxPercentIncrementFloat);

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
                m_timer += UnityEngine.Time.deltaTime;
        }
    }
}
