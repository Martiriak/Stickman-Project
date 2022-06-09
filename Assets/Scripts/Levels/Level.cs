using System; // For C# Actions.
using UnityEngine;
using Stickman.Levels.EndTrigger;
using Stickman.Levels.Context;
using Stickman.Managers;

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

        public Vector3 GetEndLevelPosition() => mEndLevelTrigger.transform.position;

        private void Awake()
        {
            FixEndLevelPosition();

            mEndLevelTrigger.EnteringScreen += () =>
            {
                EnteringScreenFinished?.Invoke();
            };

            mEndLevelTrigger.ExitingScreen += () =>
            {
                //ExitingScreenFinished?.Invoke();
                Destroy(gameObject);
            };


            void FixEndLevelPosition()
            {
                float oldX = mEndLevelTrigger.transform.localPosition.x;
                mEndLevelTrigger.transform.localPosition = new Vector3(oldX, 0f, 0f);
            }
        }

        private void OnEnable()
        {
            GameManager.Instance.SpeedManager.OnSpeedChange += MoveLevelWithSpeed;
        }

        private void OnDisable()
        {
            GameManager.Instance.SpeedManager.OnSpeedChange -= MoveLevelWithSpeed;
        }

        private void MoveLevelWithSpeed(float speed)
        {
            Vector3 movement = LevelDirection * speed * Time.deltaTime;
            transform.Translate(movement);
        }
    }
}
