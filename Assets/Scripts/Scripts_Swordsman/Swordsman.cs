using System; // C# Actions.
using System.Collections;
using UnityEngine;

namespace Stickman.Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Swordsman : MonoBehaviour
    {
        [SerializeField] private float m_jumpForce = 10f;
        [SerializeField] private float m_crashDownSpeed = 5f;
        [SerializeField] private LayerMask m_groundLayers;

        [Header("TMP STUFF")]
        [SerializeField] private float m_animationDurationTMP = 2f;
        [SerializeField] private GameObject m_hitbox;

        private Rigidbody2D c_rb;

        private bool m_isInAir = false;
        private bool m_isSwinging = false;
        private bool m_isCrashingDown = false;
        private bool m_justJumped = false;
        private IEnumerator c_animationCoroutine = null;


        public event Action OnRunning;
        //public event Action OnJumping;
        //public event Action OnCrushingDown;
        public event Action OnSwinging;


        private bool IsGrounded
        {
            get
            {
                RaycastHit2D wasGroundHit;
                wasGroundHit = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.5f), 0f, -Vector2.up, 1f, m_groundLayers);

                return wasGroundHit.collider != null;
            }
        }

        private void Awake()
        {
            c_rb = GetComponent<Rigidbody2D>();

            m_hitbox.SetActive(false);
        }

        private void Start() => OnRunning?.Invoke();

        private void Update()
        {
            // Gather input TMP!
            if (Input.GetKeyDown(KeyCode.W)) Jump();

            if (Input.GetKeyDown(KeyCode.S) && m_isInAir)
                m_isCrashingDown = true;

            if (Input.GetKeyDown(KeyCode.Space)/* && !m_isSwinging*/)
                m_isSwinging = true;
        }

        private void Jump()
        {
            m_justJumped = true;
        }

        private void FixedUpdate()
        {
            if (IsGrounded)
            {
                m_isInAir = false;
                m_isCrashingDown = false;

                if (m_justJumped)
                    c_rb.velocity = Vector2.up * m_jumpForce;
            }
            else
            {
                m_isInAir = true;

                if (m_isCrashingDown)
                    c_rb.velocity = (-Vector2.up) * m_crashDownSpeed;
            }

            // Gestire swing
            if (m_isSwinging && c_animationCoroutine == null)
            {
                c_animationCoroutine = SwingAnimation();
                StartCoroutine(c_animationCoroutine);
            }

            m_justJumped = false;
        }

        // MEGA TEMP.
        private IEnumerator SwingAnimation()
        {
            Debug.Log("Swing!");
            OnSwinging?.Invoke();

            m_hitbox.SetActive(true);
            yield return new WaitForSeconds(m_animationDurationTMP);
            m_hitbox.SetActive(false);

            m_isSwinging = false;
            c_animationCoroutine = null;

            OnRunning?.Invoke();
        }
    }
}