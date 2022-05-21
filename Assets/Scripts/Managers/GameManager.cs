using UnityEngine;
using Stickman.Managers.Speed;

namespace Stickman.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager m_instance;
        public static GameManager Instance => m_instance;

        [SerializeField] private SpeedManager m_speedManager;
        public SpeedManager SpeedManager => m_speedManager;

        // Here we can include stuff like score, player lives, etc...

        public int CurrentLoadedScene { get; set; } = 0;

        // I don't need lazy initialization, since this class is avaiable from the start of the game.
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
