using System; // For C# Actions.
using UnityEngine;

namespace Stickman.Levels.EndTrigger
{
    /// <summary>
    /// It interacts with the play view trigger, notifying when it enters
    /// and exits said view.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class LevelEndTrigger : MonoBehaviour
    {
        public Action EnteringScreen;
        public Action ExitingScreen;

        private void OnTriggerEnter(Collider _) => EnteringScreen?.Invoke();
        private void OnTriggerExit(Collider _) => ExitingScreen?.Invoke();

#if UNITY_EDITOR
        private void Awake()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (!rb.isKinematic)
                Debug.LogError("IN BUILD FINALE DEVI METTERMI IL RIGIDBODY KINEMATIC!");
            rb.isKinematic = true;
            if (rb.useGravity)
                Debug.LogError("IN BUILD FINALE DEVI TOGLIERMI LA GRAVITA'!");
            rb.useGravity = false;

            Collider col = GetComponent<Collider>();
            if (!col.isTrigger)
                Debug.LogError("IN BUILD FINALE DEVI METTERMI IL COLLIDER TRIGGER!");
            col.isTrigger = true;
        }
#endif
    }
}
