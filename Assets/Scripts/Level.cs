using System; // For C# Actions.
using UnityEngine;
using Stickman.Levels.EndTrigger;

namespace Stickman.Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private LevelEndTrigger mEndLevelTrigger;

        public Action EnteringScreenFinished;
        //public Action ExitingScreenFinished;

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
    }
}
