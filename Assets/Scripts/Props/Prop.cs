using System.Collections;
using UnityEngine;
using Stickman.Props.Builder;

namespace Stickman.Props
{
    public enum PropTypes
    {
        Health,
        Invulnerability
    }

    [RequireComponent(typeof(Collider2D))]
    public class Prop : MonoBehaviour
    {
        [SerializeField] private PropTypes propType;

        public PropCommand Consume()
        {
            // Callbacks vari qui...
            StartCoroutine(Die());

            return PropBehaviourBuilder.AssembleCommand(propType);
        }

        private IEnumerator Die()
        {
            yield return null;
            Destroy(gameObject);
        }


#if UNITY_EDITOR
        private void Awake()
        {
            Collider2D col = GetComponent<Collider2D>();
            if (!col.isTrigger)
                Debug.LogWarning("METTI IL COLLIDER DEI PROPS COME TRIGGER!");
            col.isTrigger = true;
        }
#endif
    }
}
