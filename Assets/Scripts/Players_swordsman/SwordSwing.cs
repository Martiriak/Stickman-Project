using UnityEngine;
using Stickman.Damageables;

namespace Stickman.Players.SwordsmanUtilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class SwordSwing : MonoBehaviour
    {

#if UNITY_EDITOR
        private void Awake()
        {
            if (gameObject.layer != LayerMask.NameToLayer("Damaging"))
                Debug.LogWarning("METTIMI IL LAYER \"Damaging\" IN BUILD FINALE PLEASE!");
            gameObject.layer = LayerMask.NameToLayer("Damaging");

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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.GetComponent<IDamageable>()?.HandleHit();
        }
    }
}
