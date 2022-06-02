using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pigeonShooter
{
    public class SpawnPigeons : MonoBehaviour
    {
        public GameObject pigeonPrefab;
        public GameObject pigeonPowerUpPrefab;

        [SerializeField] private Camera mViewport;
        private float screenAreaPadding = 0.35f;
        private Vector3 min; //bottom left corner of viewport box
        private Vector3 max; //bottom right corner of viewport box
        private float xPos;
        private float yPos;
        private ObjectPooler objectPooler;


        private void Start()
        {
            GetScreenBorders();
            StartPigeonSpawn();
            objectPooler = ObjectPooler.Instance;
        }

        private void GetScreenBorders() /// Would be better to call a PlayAreaInitializer GetCorners method ... Tell Alessandro!!!
        {
            // Obtain bottom-left (min) and the top-right (max) corners of viewport box
            float cameraDistanceToGamePlane = Mathf.Abs(mViewport.transform.position.z);
            min = mViewport.ViewportToWorldPoint(new Vector3(0f, 0f, cameraDistanceToGamePlane));
            max = mViewport.ViewportToWorldPoint(new Vector3(1f, 1f, cameraDistanceToGamePlane));
            min.z = 0f; max.z = 0f;
            // Adds some padding
            min.x -= screenAreaPadding; min.y -= screenAreaPadding;
            max.x += screenAreaPadding; max.y += screenAreaPadding;
            // Obtains center and size of play area.
            Vector3 viewportCenter = mViewport.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cameraDistanceToGamePlane));
            Vector3 playAreaSize = max - min; playAreaSize.z = 1f;
        }

        public void StartPigeonSpawn()
        {
            StartCoroutine(PigeonSpawn());
        }

        public void StopPigeonSpawn()
        {
            StopCoroutine(PigeonSpawn());
        }

        public void DestroyAllPigeons()
        {
            GameObject[] pigeons = GameObject.FindGameObjectsWithTag("Pigeon");
            foreach (GameObject pigeon in pigeons)
            GameObject.Destroy(pigeon);
        }

        IEnumerator PigeonSpawn()
        {
            float minX; float minY;
            minX = max.x - ((max.x - min.x) * 0.7f);
            minY = max.y - ((max.y - min.y) * 0.8f);

            while (true)
            {
                if (UnityEngine.Random.value > 0.5f) // with prob 50%
                {
                    // spawn pigeon with x fixed (in front of player) and rand y
                    xPos = max.x;
                    yPos = UnityEngine.Random.Range(minY, max.y);
                }
                else
                {
                    // spawn pigeon with y fixed (on top of player) and rand x
                    yPos = max.y;
                    xPos = UnityEngine.Random.Range(minX, max.x);
                }

                // Spawn PigeonPowerUp with probability (10%)
                if (UnityEngine.Random.value < 0.1f)
                {
                    Instantiate(pigeonPowerUpPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(pigeonPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                }
                

                //ObjectPooler.Instance.SpawnFromPool("PigeonPoolName", new Vector3(xPos, yPos, 0), Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
        }

    }
}

