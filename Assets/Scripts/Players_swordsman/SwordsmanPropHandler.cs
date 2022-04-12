using UnityEngine;
using Stickman.Players.Context;
using Stickman.Props;

namespace Stickman.Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class SwordsmanPropHandler : MonoBehaviour
    {
        [SerializeField] private PlayerContext m_context;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Prop"))
            {
                PropCommand command = collision.GetComponent<Prop>().Consume();
                command.Execute(m_context);
            }
        }
    }
}
