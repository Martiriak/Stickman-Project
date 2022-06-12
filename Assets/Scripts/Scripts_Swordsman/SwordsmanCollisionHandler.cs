using UnityEngine;
using Stickman.Managers;

namespace Stickman.Player.Swordsman
{
    public class SwordsmanCollisionHandler : PlayerBase
    {
        [SerializeField] private LayerMask m_collidableMask;

        private Vector2 c_boxCastSize;

        private void Awake()
        {
            BoxCollider2D col = (BoxCollider2D) GetComponent<Collider2D>();
            c_boxCastSize = col.size - new Vector2(0.1f, 0.4f);
        }

        protected override void TriggerEnterBehaviuor(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Death Zone"))
            {
                GameManager.Instance.LivesManager.RemoveLife();
                StartCoroutine(Respawn());
            }
        }

        protected override void CollisionEnterBehaviuor(Collision2D collision) { }

        private void FixedUpdate()
        {
            if (GameManager.Instance.LivesManager.IsInvulnerable) return;

            if (CheckIfWallHit())
                GameManager.Instance.LivesManager.RemoveLife();
        }

        private bool CheckIfWallHit()
        {
            RaycastHit2D hit
                = Physics2D.BoxCast(transform.position, c_boxCastSize, 0f, Vector2.right, 0.1f, m_collidableMask);

            if (hit.transform != null) return true;
            else return false;
        }
    }
}
