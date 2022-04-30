using System.Collections;
using System.Collections.Generic;
using Stickman.Managers.Speed;
using Stickman.Levels.EndTrigger;
using UnityEngine;

namespace Stickman
{
    public class StarterLevel : MonoBehaviour
    {
        private static readonly Vector3 LevelDirection = -Vector3.right;
        [SerializeField] private SpeedManager mSpeedManager;
        private float currentSpeed;

        [SerializeField] private LevelEndTrigger mEndLevelTrigger;

        void Awake()
        {
            currentSpeed = mSpeedManager.EvaluateSpeed();
            mEndLevelTrigger.ExitingScreen += () =>
            {
                //ExitingScreenFinished?.Invoke();
                Destroy(gameObject);
            };
        }

        // Update is called once per frame
        private void Update()
        {
            Vector3 movement = LevelDirection * currentSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
    }
}
