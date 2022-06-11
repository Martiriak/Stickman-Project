using System; // C# Actions
using System.Collections;
using UnityEngine;

namespace Stickman.Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class SwordsmanLives : MonoBehaviour
    {
        [SerializeField] private int m_maxLives = 3;
        [SerializeField] private float m_invulnerabilityTime = 2f;

        [SerializeField] private LayerMask m_mask;

        private int m_currentLives;
        private bool m_isInvulnerable = false;

        private IEnumerator c_invulnerabilityCoroutine = null;
        private ContactPoint2D[] c_collisionsContacts = new ContactPoint2D[2];
        private Rigidbody2D c_rb;
        //private BoxCollider2D c_col;

        public Action<bool> OnInvulnerability;

        private void Awake()
        {
            c_rb = GetComponent<Rigidbody2D>();
            //c_col = GetComponent<BoxCollider2D>();
        }
        private void Start() => m_currentLives = m_maxLives;


        public void RegenLives(uint amount = 1)
        {
            Debug.Log("Healed!");
            m_currentLives += (int) amount;
            if (m_currentLives > m_maxLives)
                m_currentLives = m_maxLives;
        }

        public void SetInvulnerabilityTime(float invTime)
        {
            if (c_invulnerabilityCoroutine != null)
                StopCoroutine(c_invulnerabilityCoroutine);

            c_invulnerabilityCoroutine = InvulnerabilityTimer(invTime);
            StartCoroutine(c_invulnerabilityCoroutine);
        }




        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Death Zone"))
            {
                c_rb.velocity = Vector2.zero;
                transform.position = transform.position + new Vector3(0f, 5f, 0f);

                if (!m_isInvulnerable) HandleDamage();
                return;
            }
        }

        /*
        private bool CheckIfWallHit()
        {
            Vector2 boxCastSize = c_col.size - new Vector2(0.1f, 0.5f);

            RaycastHit2D hit
                = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.right, 0.1f, m_mask);

            if (hit.transform != null) return true;
            else return false;
        }*/

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (m_isInvulnerable) return;

            collision.GetContacts(c_collisionsContacts);

            foreach (ContactPoint2D contact in c_collisionsContacts)
            {
                if (contact.normal == (-Vector2.right))
                {
                    HandleDamage();
                    return;
                }
            }
        }

        private void HandleDamage()
        {
            --m_currentLives;
            if (m_currentLives < 1)
                Debug.Log("DED");

            if (c_invulnerabilityCoroutine != null)
                StopCoroutine(c_invulnerabilityCoroutine);

            c_invulnerabilityCoroutine = InvulnerabilityTimer(m_invulnerabilityTime);
            StartCoroutine(c_invulnerabilityCoroutine);
        }


        private IEnumerator InvulnerabilityTimer(float invTime)
        {
            m_isInvulnerable = true;
            OnInvulnerability?.Invoke(true);

            float elapsedTime = 0f;
            while (elapsedTime < invTime)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
            }

            m_isInvulnerable = false;
            OnInvulnerability?.Invoke(false);
            c_invulnerabilityCoroutine = null;
        }


       /*private void FixedUpdate()
        {
            if (m_isInvulnerable) return;

            if (CheckIfWallHit())
            {
                HandleDamage();
            }
        }*/


        private void OnValidate()
        {
            if (m_maxLives < 1) m_maxLives = 1;
            if (m_invulnerabilityTime < 0.1f) m_invulnerabilityTime = 0.1f;
        }
    }
}
