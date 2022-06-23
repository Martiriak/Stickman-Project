using UnityEngine;

namespace Stickman.PlayArea
{
    public class ParticleEmitterSizeFixer : MonoBehaviour
    {
        [SerializeField] private Camera m_camera;
        [SerializeField] private PlayAreaInitializer m_playArea;
        [SerializeField] private ParticleSystem m_backroundParticles;
        [Space]
        [Range(0f, 1f)]
        [SerializeField] private float m_screenWidthFill = 1f;

        private void Start()
        {
            var emitterShape = m_backroundParticles.shape;

            Vector3 topLeftCorner = new Vector2(0f, 1f); // Viewport
            topLeftCorner.x += 1f - m_screenWidthFill; // Still viewport
            topLeftCorner = m_camera.ViewportToWorldPoint(topLeftCorner); // Bring to World

            Vector3 trueSize = topLeftCorner - m_playArea.BottomRightCorner;
            trueSize.z = 0f;
            Vector3 trueCenter = (topLeftCorner + m_playArea.BottomRightCorner) / 2f;
            trueCenter.z = 0f;

            emitterShape.scale = trueSize;
            emitterShape.position = trueCenter;
        }


        private void OnValidate()
        {
            m_screenWidthFill = Mathf.Clamp01(m_screenWidthFill);
        }
    }
}
