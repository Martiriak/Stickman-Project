using UnityEngine;
using Stickman.Levels.Context;
using Stickman.Managers;

namespace Stickman.Levels.Spawner
{
    /// <summary>
    /// Provides all the necessary information and classes to the newly
    /// instantiated levels. It is separate from the LevelSpawner class
    /// to not pollute that class with unnecessary using directives.
    /// </summary>
    [RequireComponent(typeof(LevelSpawner))]
    public class LevelSpawnerContext : MonoBehaviour
    {
        private LevelContext m_contextToAssign;

        private void Awake()
        {
            m_contextToAssign = new LevelContext(GameManager.Instance.SpeedManager);
        }

        public void ProvideContext(Level level)
        {
            level.AssignContext(m_contextToAssign);
        }
    }
}
