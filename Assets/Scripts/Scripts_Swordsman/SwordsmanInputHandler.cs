using System; // C# Actions
using UnityEngine;

namespace Stickman.Players.SwordsmanUtilities
{
    public class SwordsmanInputHandler : MonoBehaviour
    {
        private const float MinToleranceForVerticalSwipe = 0.6f;

        [SerializeField] private float m_minDistanceToCountAsSwipe = 1f;
        [SerializeField] private Camera m_camera;

        // In order to use sqrMagnitude of vectors.
        private float c_sqrMinDistanceToCountAsSwipe;


        public event Action OnJump;
        public event Action OnFalling;
        public event Action OnSwordSwing;


        private void Awake()
        {
            c_sqrMinDistanceToCountAsSwipe = m_minDistanceToCountAsSwipe * m_minDistanceToCountAsSwipe;

#if UNITY_EDITOR
            if (m_camera == null)
            {
                Debug.LogWarning("Assegna Camera a SwordsmanInputHandler!");
                m_camera = Camera.main;
            }
#endif
        }

        private void Update()
        {
            if (Input.touchCount == 0) return;

            Touch firstTouch = Input.GetTouch(0);

            if (firstTouch.phase == TouchPhase.Ended)
            {
                Vector2 currentTouchPosViewport =
                    new Vector2(firstTouch.position.x / m_camera.pixelWidth, firstTouch.position.y / m_camera.pixelHeight);

                Vector2 initialTouchPosViewport =
                    new Vector2(firstTouch.rawPosition.x / m_camera.pixelWidth, firstTouch.rawPosition.y / m_camera.pixelHeight);

                Vector2 swipeDirection = currentTouchPosViewport - initialTouchPosViewport;

                if (Mathf.Abs(swipeDirection.sqrMagnitude) > c_sqrMinDistanceToCountAsSwipe)
                {
                    swipeDirection.Normalize();

                    if (swipeDirection.y > MinToleranceForVerticalSwipe)
                        OnJump?.Invoke();

                    else if (swipeDirection.y < -MinToleranceForVerticalSwipe)
                        OnFalling?.Invoke();

                    else OnSwordSwing?.Invoke();
                }
                else OnSwordSwing?.Invoke();
            }
        }
    }
}
