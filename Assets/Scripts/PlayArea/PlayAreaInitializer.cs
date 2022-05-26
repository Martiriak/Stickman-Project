using UnityEngine;
using Stickman.Managers;
using UnityEngine.SceneManagement;


namespace Stickman.PlayArea
{
    [RequireComponent(typeof(BoxCollider))]
    public class PlayAreaInitializer : MonoBehaviour
    {
        [Tooltip("How much the play area extends from the viewport.")]
        [SerializeField] private float mPlayAreaPadding = 0.4f;
        [SerializeField] private Camera mViewport;
        [Space]
        [Tooltip("It will be positioned to the bottom-right corner of the screen.")]
        [SerializeField] private Transform mSpawner;
        [Tooltip("It will be positioned to the top-right corner of the screen.")]
        [SerializeField] private Transform mSpawner2;
        [Tooltip("It will be positioned at the right of the screen, centered on the Y axis.")]
        [SerializeField] private Transform mSpawner3;

        private Vector3 mMax;
        private Vector3 mMin;

        public Vector3 TopLeftCorner => new Vector3(mMin.x, mMax.y, 0f);
        public Vector3 TopRightCorner => mMax;
        public Vector3 BottomLeftCorner => mMin;
        public Vector3 BottomRightCorner => new Vector3(mMax.x, mMin.y, 0f);

        private void Awake()
        {
            GameManager.Instance.CurrentLoadedScene = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(GameManager.Instance.CurrentLoadedScene);

            // Obtains the bottom-left (min) and the top-right (max) corners
            // of the bounding box that represents the play area, which is the viewport box.
            float cameraDistanceToGamePlane = Mathf.Abs(mViewport.transform.position.z);
            Vector3 min = mViewport.ViewportToWorldPoint(new Vector3(0f, 0f, cameraDistanceToGamePlane));
            Vector3 max = mViewport.ViewportToWorldPoint(new Vector3(1f, 1f, cameraDistanceToGamePlane));
            min.z = 0f; max.z = 0f;

            // Adds some padding, so that obstacles spawn and die slight more offscreen.
            min.x -= mPlayAreaPadding; min.y -= mPlayAreaPadding;
            max.x += mPlayAreaPadding; max.y += mPlayAreaPadding;

            mMax = max; mMin = min;

            // Obtains center and size of play area.
            Vector3 viewportCenter = mViewport.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cameraDistanceToGamePlane));
            Vector3 playAreaSize = max - min; playAreaSize.z = 1f;

            // Sets the trigger settings.
            BoxCollider playAreaTrigger = GetComponent<BoxCollider>();
            playAreaTrigger.center = viewportCenter;
            playAreaTrigger.size = playAreaSize;

#if UNITY_EDITOR
            if (!playAreaTrigger.isTrigger)
                Debug.LogError("RICORDATI DI METTERE COME TRIGGER IL COLLIDER DELL'AREA DI GIOCO!");
            playAreaTrigger.isTrigger = true;
#endif

            // Places the spawner on the top, respecting the x padding but not the y padding.
            if (mSpawner != null)
                mSpawner.position = new Vector3(max.x, min.y + mPlayAreaPadding, 0f);

            // Places the spawner on the bottom, respecting the x padding but not the y padding.
            if (mSpawner2 != null)
                mSpawner2.position = new Vector3(max.x, max.y - mPlayAreaPadding, 0f);

            // Places on the right in a central Y position.
            if (mSpawner3 != null)
                mSpawner3.position = new Vector3(max.x, viewportCenter.y, 0f);
        }

        private void OnValidate()
        {
            if (mPlayAreaPadding < 0.05f) mPlayAreaPadding = 0.05f;
        }
    }
}
