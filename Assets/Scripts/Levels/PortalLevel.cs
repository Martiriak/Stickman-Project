using UnityEngine;
using UnityEngine.SceneManagement;
using Stickman.Managers;

namespace Stickman.Levels.Portal
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class PortalLevel : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameManager.Instance.TimeTracker.Pause();
            
            SceneManager.LoadScene(1); // To the Loading Level!
        }


#if UNITY_EDITOR
        private void Awake()
        {
            if (gameObject.layer != LayerMask.NameToLayer("Portal"))
                Debug.LogWarning("METTIMI IL LAYER \"Portal\" IN BUILD FINALE PLEASE!");
            gameObject.layer = LayerMask.NameToLayer("Portal");

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (!rb.isKinematic)
                Debug.LogWarning("METTIMI KINEMATIC IN BUILD FINALE PLEASE!");
            rb.isKinematic = true;

            Collider2D col = GetComponent<Collider2D>();
            if (!col.isTrigger)
                Debug.LogWarning("METTIMI TRIGGER IN BUILD FINALE PLEASE!");
            col.isTrigger = true;
        }
#endif
    }
}
