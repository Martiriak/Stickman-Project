using UnityEngine;
using Stickman.Managers.Speed;
using Stickman.Managers.Time;
using Stickman.Managers.Sound;

namespace Stickman.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager m_instance;
        public static GameManager Instance => m_instance;

        [SerializeField] private SpeedManager m_speedManager;
        [SerializeField] private LivesManager m_livesManager;
        [SerializeField] private TimeTracker m_timeTracker;
        [SerializeField] private SoundManager m_soundManager;
        public SpeedManager SpeedManager => m_speedManager;
        public LivesManager LivesManager => m_livesManager;
        public TimeTracker TimeTracker => m_timeTracker;
        public SoundManager SoundManager => m_soundManager;

        // Here we can include stuff like score, player lives, etc...

        public int CurrentLoadedScene { get; set; } = 0;

        // I don't need lazy initialization, since this class is available from the start of the game.
        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }
    }
}
