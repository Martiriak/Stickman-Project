using System; // For C# Actions.
using UnityEngine;
using Stickman.Levels.EndTrigger;
using Stickman.Levels.Context;

namespace Stickman.Levels
{
    public class Level : MonoBehaviour
    {
        private static readonly Vector3 LevelDirection = -Vector3.right;

        [SerializeField] private LevelEndTrigger mEndLevelTrigger;

        private LevelContext mContext;

        public Action EnteringScreenFinished;
        //public Action ExitingScreenFinished;

        public void AssignContext(LevelContext context) => mContext = context;

        private void Awake()
        {
            mEndLevelTrigger.EnteringScreen += () =>
            {
                EnteringScreenFinished?.Invoke();
            };

            mEndLevelTrigger.ExitingScreen += () =>
            {
                //ExitingScreenFinished?.Invoke();
                Destroy(gameObject);
            };
        }

        private void Update()
        {
            Vector3 movement = LevelDirection * mContext.CurrentVelocity * Time.deltaTime;
            transform.Translate(movement);
        }
    }
}
