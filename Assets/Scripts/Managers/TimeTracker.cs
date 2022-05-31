using System; // C# Actions.
//using System.Collections;
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
        public event Action<float /*Total*/, float /*LapFinalTime*/, int /*LapsNumber*/> OnLap;
        public event Action<float /*FinalTotal*/, int /*LapsNumber*/> OnStopped;

        public void StartStopWatch()
        {
            if (IsPlaying) return;
            IsPlaying = true;

            TotalStopWatch = 0f;
            LapStopWatch = 0f;
            NumberOfLaps = 0;

            if (OnStarted != null) OnStarted();

            IsPaused = false;
        }

        public void Lap()
        {
            if (!IsPlaying) return;

            ++NumberOfLaps;

            if (OnLap != null) OnLap(TotalStopWatch, LapStopWatch, NumberOfLaps);

            LapStopWatch = 0f;
        }

        public void Pause()
        {
            if (!IsPlaying && IsPaused) return;

            IsPaused = true;
        }

        public void Resume()
        {
            if (!IsPlaying && !IsPaused) return;

            IsPaused = false;
        }

        public void Stop()
        {
            if (!IsPlaying) return;

            Lap();

            IsPlaying = false;
            IsPaused = true;

            if (OnStopped != null) OnStopped(TotalStopWatch, NumberOfLaps);
        }


        /* LateUpdate to be more reliable, however it comes with a price:
         * each frame, we will have the time update to the previous frame. */
        private void LateUpdate()
        {
            if (!IsPlaying && IsPaused) return;

            LapStopWatch += UnityEngine.Time.deltaTime;
            TotalStopWatch += LapStopWatch;
        }
    }
}
