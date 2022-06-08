using System; // C# Actions.
using UnityEngine;

namespace Stickman.Managers.Time
{
    public class TimeTracker : MonoBehaviour
    {
        public float TotalStopWatch { get; private set; } = -1f;
        public float LapStopWatch { get; private set; } = -1f;

        public int NumberOfLaps { get; private set; } = -1;

        public bool IsPlaying { get; private set; } = false;
        public bool IsPaused { get; private set; } = true;

        public event Action OnStarted;
        public event Action<int /*LapsNumber*/> OnLap;
        public event Action OnPaused;
        public event Action OnResumed;
        public event Action OnStopped;
        public event Action<float /*Total*/, float /*LapFinalTime*/, int /*LapsNumber*/> OnLapWithDetails;
        public event Action<float /*CurrentTotal*/, float /*LapCurrentTime*/, int /*LapsNumber*/> OnPausedWithDetails;
        public event Action<float /*CurrentTotal*/, float /*LapCurrentTime*/, int /*LapsNumber*/> OnResumedWithDetails;
        public event Action<float /*FinalTotal*/, int /*LapsNumber*/> OnStoppedWithDetails;

        public void StartStopWatch(bool startImmediately = true)
        {
            if (IsPlaying) return;
            IsPlaying = true;

            TotalStopWatch = 0f;
            LapStopWatch = 0f;
            NumberOfLaps = 1;

            if (OnStarted != null) OnStarted();

            if (startImmediately) IsPaused = false;
            else IsPaused = true; // Unnecessary, but safe to do.
        }

        public void Lap()
        {
            if (!IsPlaying) return;

            ++NumberOfLaps;

            if (OnLap != null) OnLap(NumberOfLaps);
            if (OnLapWithDetails != null) OnLapWithDetails(TotalStopWatch, LapStopWatch, NumberOfLaps);

            //Debug.Log($"LAP! Attualmente: {NumberOfLaps}");

            LapStopWatch = 0f;
        }

        public void Pause()
        {
            if (!IsPlaying) return;
            if (IsPaused) return;

            IsPaused = true;

            if (OnPaused != null) OnPaused();
            if (OnPausedWithDetails != null) OnPausedWithDetails(TotalStopWatch, LapStopWatch, NumberOfLaps);
        }

        public void Resume()
        {
            if (!IsPlaying) return;
            if (!IsPaused) return;

            IsPaused = false;

            if (OnResumed != null) OnResumed();
            if (OnResumedWithDetails != null) OnResumedWithDetails(TotalStopWatch, LapStopWatch, NumberOfLaps);
        }

        public void Stop()
        {
            if (!IsPlaying) return;

            Lap();

            IsPlaying = false;
            IsPaused = true;

            if (OnStopped != null) OnStopped();
            if (OnStoppedWithDetails != null) OnStoppedWithDetails(TotalStopWatch, NumberOfLaps);
        }


        /* LateUpdate to be more reliable, however it comes with a price:
         * each frame, we will have the time update to the previous frame. */
        private void LateUpdate()
        {
            if (!IsPlaying) return;
            if (IsPaused) return;

            LapStopWatch += UnityEngine.Time.deltaTime;
            TotalStopWatch += UnityEngine.Time.deltaTime;

            //Debug.Log($"Tempo Del Lap: {LapStopWatch}; Tempo Totale: {TotalStopWatch}");
        }
    }
}
