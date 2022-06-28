using System; // C# Actions
using System.Collections;
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


        public float CurrentSpeed { get; private set; } = 0f;

        private float m_curveProgress = 0f;
        private float m_currentIncrement = 0f;

        private float m_currentFrameLapStopWatch = 0f;
        private float m_previousFrameLapStopWatch = 0f;



        // Caches maxSpeed - minSpeed.
        private float c_speedLimitsIntervalLenght;
        // Caches m_maxPercentIncrement / 100f.
        private float c_maxPercentIncrementFloat;
        // Caches the update coroutine in order to stop it when we want to stop it.
        private IEnumerator c_updateCoroutine = null;


        public event Action<float /*CurrentEvaluatedSpeed*/> OnSpeedChange;


        private void Awake()
        {
            c_speedLimitsIntervalLenght = m_maxSpeed - m_minSpeed;
            c_maxPercentIncrementFloat = m_maxPercentIncrement / 100f;
        }

        private void Start()
        {
            GameManager.Instance.TimeTracker.OnStarted += (bool IsPausedWhenStarted) =>
            {
                m_currentFrameLapStopWatch = 0f;
                m_previousFrameLapStopWatch = 0f;
                m_curveProgress = 0f;
                m_currentIncrement = 0f;

                if (!IsPausedWhenStarted)
                {
                    c_updateCoroutine = UpdateCurveProgress();
                    StartCoroutine(c_updateCoroutine);
                }
            };

            GameManager.Instance.TimeTracker.OnStopped += () =>
            {
                ResetSpeed();

                // Check if null, because it surely will be if the stopwatch is stopped when paused.
                if (c_updateCoroutine != null)
                {
                    StopCoroutine(c_updateCoroutine);
                    c_updateCoroutine = null;
                }
            };

            GameManager.Instance.TimeTracker.OnPaused += () =>
            {
                ResetSpeed();
                StopCoroutine(c_updateCoroutine);
                c_updateCoroutine = null;
            };

            GameManager.Instance.TimeTracker.OnResumed += () =>
            {
                c_updateCoroutine = UpdateCurveProgress();
                StartCoroutine(c_updateCoroutine);
            };

            GameManager.Instance.TimeTracker.OnLap += (int CurrentLap) =>
            {
                m_currentFrameLapStopWatch = 0f;
                m_previousFrameLapStopWatch = 0f;
                ResetSpeed();

                m_currentIncrement = (m_percentIncrement * (CurrentLap - 1)) / 100f;
                if (m_currentIncrement > c_maxPercentIncrementFloat)
                    m_currentIncrement = c_maxPercentIncrementFloat;

                m_curveProgress = m_currentIncrement;
            };
        }


        public void SlowDown(int slowDownPercentage)
        {
            float slowDownPercentageFloat = Mathf.Clamp01(slowDownPercentage / 100f);

            m_curveProgress -= slowDownPercentageFloat;
            m_curveProgress = Mathf.Clamp01(m_curveProgress);

            EvaluateSpeed();
        }


        private void EvaluateSpeed()
        {
            float normalizedVelocity = m_speedCurve.Evaluate(m_curveProgress);

            // Maps the value obtained from the curve from 0..1 to minSpeed..maxSpeed.
            CurrentSpeed = normalizedVelocity * c_speedLimitsIntervalLenght + m_minSpeed;

            if (OnSpeedChange != null) OnSpeedChange(CurrentSpeed);
        }

        public void ResetSpeed()
        {
            CurrentSpeed = 0f;

            if (OnSpeedChange != null) OnSpeedChange(CurrentSpeed);
        }

        private IEnumerator UpdateCurveProgress()
        {
            while (true)
            {
                m_currentFrameLapStopWatch = GameManager.Instance.TimeTracker.LapStopWatch / m_maxSpeedTimestamp;

                m_curveProgress += m_currentFrameLapStopWatch - m_previousFrameLapStopWatch;
                m_curveProgress = Mathf.Clamp01(m_curveProgress);

                m_previousFrameLapStopWatch = m_currentFrameLapStopWatch;

                EvaluateSpeed();
                yield return null;
            }
        }


        private void OnValidate()
        {
            if (m_maxSpeedTimestamp < 1f) m_maxSpeedTimestamp = 1f;
            if (m_minSpeed < 1f) m_minSpeed = 1f;
            if (m_maxSpeed < m_minSpeed) m_maxSpeed = m_minSpeed;

            m_percentIncrement = Mathf.Clamp(m_percentIncrement, 0, 100);
            m_maxPercentIncrement = Mathf.Clamp(m_maxPercentIncrement, m_percentIncrement, 100);

            c_speedLimitsIntervalLenght = m_maxSpeed - m_minSpeed;
            c_maxPercentIncrementFloat = m_maxPercentIncrement / 100f;
        }
    }
}
