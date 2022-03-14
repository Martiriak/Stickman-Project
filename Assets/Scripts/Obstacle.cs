using UnityEngine;

namespace Stickman.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        private Bounds mObstacleBounds;
        private bool mHasBeenBounded = false;

        public void ComputeBounds()
        {
            mHasBeenBounded = true;
            mObstacleBounds = new Bounds(transform.position, Vector3.zero);

            for (int i = 0; i < transform.childCount; ++i)
            {
                mObstacleBounds.Encapsulate(GetBounds(transform.GetChild(i).gameObject));
            }

            Vector3 min = mObstacleBounds.min;
            Vector3 max = mObstacleBounds.max;
            min.z = 0f; max.z = 0f;
            mObstacleBounds.SetMinMax(min, max);

            /*// Debug only: show the bounds.
            // From down-left to up-left
            Debug.DrawLine(min, new Vector3(min.x, max.y, 0f), Color.blue, 5f);
            // From down-left to down-right
            Debug.DrawLine(min, new Vector3(max.x, min.y, 0f), Color.blue, 5f);
            // From up-right to down-right
            Debug.DrawLine(max, new Vector3(max.x, min.y, 0f), Color.yellow, 5f);
            // From up-right to up-left
            Debug.DrawLine(max, new Vector3(min.x, max.y, 0f), Color.yellow, 5f);*/
        }

        private Bounds GetBounds(GameObject objeto)
        {
            Bounds bounds;
            Renderer childRender;
            bounds = GetRenderBounds(objeto);
            if (bounds.extents.x == 0)
            {
                bounds = new Bounds(objeto.transform.position, Vector3.zero);
                foreach (Transform child in objeto.transform)
                {
                    childRender = child.GetComponent<Renderer>();
                    if (childRender)
                    {
                        bounds.Encapsulate(childRender.bounds);
                    }
                    else
                    {
                        bounds.Encapsulate(GetBounds(child.gameObject));
                    }
                }
            }

            return bounds;
        }

        private Bounds GetRenderBounds(GameObject objeto)
        {
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
            Renderer render = objeto.GetComponent<Renderer>();
            if (render != null)
            {
                return render.bounds;
            }
            return bounds;
        }
    }
}
