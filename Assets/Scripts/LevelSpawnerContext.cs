using UnityEngine;
using Stickman.Levels.Context;
using Stickman.Managers.Speed;

namespace Stickman.Levels.Spawner
{
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
