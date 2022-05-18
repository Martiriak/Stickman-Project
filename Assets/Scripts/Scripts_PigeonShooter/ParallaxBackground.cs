using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pigeonShooter
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private float groundSpeed;
        [Space]
        [SerializeField] private Camera mViewport;
        [Space]
        [SerializeField] private Transform bg1;
        [SerializeField] private Transform bg2;
        [SerializeField] private Transform bg3;
        [Space]
        [SerializeField] private float bg1SpeedRed;
        [SerializeField] private float bg2SpeedRed;
        [SerializeField] private float bg3SpeedRed;
        [Space]
        public float offset;
        private Vector3 min; //bottom left corner of viewport box
        private Vector3 max; //bottom right corner of viewport box

        private void Start()
        {
            // Obtain bottom-left (min) and the top-right (max) corners of viewport box
            float cameraDistanceToGamePlane = Mathf.Abs(mViewport.transform.position.z);
            min = mViewport.ViewportToWorldPoint(new Vector3(0f, 0f, cameraDistanceToGamePlane));
            max = mViewport.ViewportToWorldPoint(new Vector3(1f, 1f, cameraDistanceToGamePlane));
            min.z = 0f; max.z = 0f;
        }

        private void Update()
        {
            if (bg1.transform.position.x < min.x * 2)
            {
                bg1.transform.position = new Vector3(max.x * 2 + offset, (max.y + min.y / 2) - 1f);
            }
            else
            {
                bg1.transform.position += new Vector3(-groundSpeed * bg1SpeedRed * Time.deltaTime, 0);
            }


            if (bg2.transform.position.x < min.x * 2)
            {
                bg2.transform.position = new Vector3(max.x * 2 + offset, (max.y + min.y / 2) - 1f);
            }
            else
            {
                bg2.transform.position += new Vector3(-groundSpeed * bg2SpeedRed * Time.deltaTime, 0);
            }

            if (bg3.transform.position.x < min.x * 2)
            {
                bg3.transform.position = new Vector3(max.x * 2 + offset, (max.y + min.y / 2) - 1f);
            }
            else
            {
                bg3.transform.position += new Vector3(-groundSpeed * bg3SpeedRed * Time.deltaTime, 0);
            }

            
            
            

            
        }

    }
}
