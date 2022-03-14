using UnityEngine;

namespace Stickman.Obstacles.Spawner
{
    public class ObstacleSpawner : MonoBehaviour
    {
        private const int MaxOffscreenSpawnedObstacles = 3;
        private const float ViewportBoundsPadding = 0.5f;

        [SerializeField] private GameObject[] mObstaclesPool;
        [Space]
        [SerializeField] private Camera mViewport;

        private Bounds mViewportBounds;

        private void Awake()
        {
#if UNITY_EDITOR
            if (mObstaclesPool.Length == 0)
                Debug.LogError("Aoo! Che ostacoli dovrei spawnare scusa?");

            if (mViewport == null)
                Debug.LogError("Aoo! Dimmi qual'è la telecamera parcobio!");
#endif
            // Obtain the bounding box that represents the viewport.
            float cameraDistanceToGamePlane = Mathf.Abs(mViewport.transform.position.z);
            Vector3 min = mViewport.ViewportToWorldPoint(new Vector3(0f, 0f, cameraDistanceToGamePlane));
            Vector3 max = mViewport.ViewportToWorldPoint(new Vector3(1f, 1f, cameraDistanceToGamePlane));
            min.z = 0f; max.z = 0f;
            // Adds some padding, so that obstacles spawn and die slight more offscreen.
            min.x -= ViewportBoundsPadding; min.y -= ViewportBoundsPadding;
            max.x += ViewportBoundsPadding; max.x += ViewportBoundsPadding;

            mViewportBounds = new Bounds();
            mViewportBounds.SetMinMax(min, max);
            
            /*// Debug only: show the bounds.
            // From down-left to up-left
            Debug.DrawLine(min, new Vector3(min.x, max.y, 0f), Color.green, Mathf.Infinity);
            // From down-left to down-right
            Debug.DrawLine(min, new Vector3(max.x, min.y, 0f), Color.green, Mathf.Infinity);
            // From up-right to down-right
            Debug.DrawLine(max, new Vector3(max.x, min.y, 0f), Color.red, Mathf.Infinity);
            // From up-right to up-left
            Debug.DrawLine(max, new Vector3(min.x, max.y, 0f), Color.red, Mathf.Infinity);*/
        }
    }
}
