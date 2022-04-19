using System; // For C# Actions.
using UnityEngine;
using Stickman.Levels.EndTrigger;
using Stickman.Levels.Context;

namespace Stickman.Levels
{
    /// <summary>
    /// Manages the lifetime of a level object, which is a prefab representing a tiny section of
    /// obstacles or awards that the player has to face.
    /// 
    /// It also moves the level from right to left.
    /// </summary>
    public class Level : MonoBehaviour
    {
        private static readonly Vector3 LevelDirection = -Vector3.right;

        [Tooltip("Allows the Level to know when it enters and exits the play view.")]
        [SerializeField] private LevelEndTrigger mEndLevelTrigger;

        // Needed to know at which velocity it should move, and maybe other stuff in the future.
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