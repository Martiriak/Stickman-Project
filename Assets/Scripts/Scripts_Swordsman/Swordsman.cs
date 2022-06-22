using System; // C# Actions.
using System.Collections;
using UnityEngine;
using Stickman.Players.SwordsmanUtilities;

namespace Stickman.Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SwordsmanInputHandler))]
    public class Swordsman : MonoBehaviour
    {
        [SerializeField] private float m_jumpForce = 10f;
        [SerializeField] private float m_crashDownSpeed = 5f;
        [SerializeField] private LayerMask m_groundLayers;
        [Space]
        [SerializeField] private float m_swordSwingDuration = 1f;
        [SerializeField] private GameObject m_swordHitbox;

        private bool m_isInAir = false;
        private bool m_isSwinging = false;
        private bool m_isCrashingDown = false;
        private bool m_justJumped = false;

        private Rigidbody2D c_rb;
        private SwordsmanInputHandler c_inputHandler;
        private WaitForSeconds c_yieldSwordSwingDuration = null;
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
            c_inputHandler = GetComponent<SwordsmanInputHandler>();
            c_yieldSwordSwingDuration = new WaitForSeconds(m_swordSwingDuration);

            m_swordHitbox.SetActive(false);
        }

        private void Start() => OnRunning?.Invoke();

        private void Update()
        {
            // Gather input TMP!
            if (Input.GetKeyDown(KeyCode.W)) Jump();

            if (Input.GetKeyDown(KeyCode.S)) CrashDown();

            if (Input.GetKeyDown(KeyCode.Space)) SwingSword();
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

            // Handle sword swing
            if (m_isSwinging && c_animationCoroutine == null)
            {
                c_animationCoroutine = SwingAnimation();
                StartCoroutine(c_animationCoroutine);
            }

            m_justJumped = false;
        }


        private void Jump() { m_justJumped = true; }
        private void CrashDown() { if (m_isInAir) m_isCrashingDown = true; }
        private void SwingSword() { m_isSwinging = true; }

        private IEnumerator SwingAnimation()
        {
            OnSwinging?.Invoke();

            m_swordHitbox.SetActive(true);
            yield return c_yieldSwordSwingDuration;
            m_swordHitbox.SetActive(false);

            m_isSwinging = false;
            c_animationCoroutine = null;

            OnRunning?.Invoke();
        }


        private void OnEnable()
        {
            c_inputHandler.OnJump += Jump;
            c_inputHandler.OnFalling += CrashDown;
            c_inputHandler.OnSwordSwing += SwingSword;
        }

        private void OnDisable()
        {
            c_inputHandler.OnJump -= Jump;
            c_inputHandler.OnFalling -= CrashDown;
            c_inputHandler.OnSwordSwing -= SwingSword;
        }

        private void OnValidate()
        {
            c_yieldSwordSwingDuration = new WaitForSeconds(m_swordSwingDuration);
        }
    }
}
