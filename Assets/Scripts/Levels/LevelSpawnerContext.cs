using UnityEngine;
using Stickman.Levels.Context;
using Stickman.Managers.Speed;

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
        [SerializeField] private SpeedManager mSpeedManager;

        private LevelContext mContextToAssign;

        private void Awake()
        {
#if UNITY_EDITOR
            if (mSpeedManager == null)
                Debug.LogError("No speed manager was provided!");
#endif
            mContextToAssign = new LevelContext(mSpeedManager);
        }

        public void ProvideContext(Level level)
        {
            level.AssignContext(mContextToAssign);
        }
    }
}
