using UnityEngine;
using Stickman.Props;

namespace Stickman.Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class SwordsmanPropHandler : MonoBehaviour, IPropHandler
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Prop"))
            {
                HandlePropFromObject(collision.gameObject);
            }
        }

        public void HandlePropFromObject(GameObject PropGameObject)
        {
            PropCommand command = PropGameObject.GetComponent<Prop>()?.Consume();
            command?.Execute();
        }
    }
}
