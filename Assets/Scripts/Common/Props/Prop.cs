using System; // C# Actions.
using System.Collections;
using UnityEngine;
using Stickman.Props.Builder;

namespace Stickman.Props
{
    public enum PropTypes
    {
        Health,
        Invulnerability,
        Clock,
        // Add here new types!
    }


    [RequireComponent(typeof(Collider2D))]
    public class Prop : MonoBehaviour
    {
        [SerializeField] private PropTypes m_propType;

        public event Action OnConsumed;
        public event Action<PropTypes /*Type*/> OnConsumedWithType;
        public event Action OnDestruction;

        public PropCommand Consume()
        {
            if (OnConsumed != null) OnConsumed();
            if (OnConsumedWithType != null) OnConsumedWithType(m_propType);

            StartCoroutine(Die());

            return PropBehaviourBuilder.AssembleCommand(m_propType);
        }

        // Die the next frame.
        private IEnumerator Die()
        {
            yield return null;

            if (OnDestruction != null) OnDestruction();
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
