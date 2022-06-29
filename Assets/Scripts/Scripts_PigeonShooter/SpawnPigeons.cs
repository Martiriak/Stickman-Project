using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stickman.PlayArea;
using Stickman.Managers;

namespace Stickman.pigeonShooter
{
    public class SpawnPigeons : MonoBehaviour
    {
        public GameObject pigeonPrefab;
        public GameObject pigeonPowerUpPrefab;

        [SerializeField] private float pigeonPowerUpSpawnProbability = 0.05f;

        [SerializeField] private PlayAreaInitializer playArea;
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
            min = playArea.BottomLeftCorner;
            max = playArea.TopRightCorner;
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

        private float SpeedToSpawnSecondsDelay()
        {
            float gameSpeed = GameManager.Instance.SpeedManager.CurrentSpeed;
            //float gameSpeed = 5;
            float m = -0.03f;
            float q = 0.7f;
            float seconds = m * gameSpeed + q;
            if (seconds < 0.2f) seconds = 0.21f;
            return seconds;
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

                // Spawn PigeonPowerUp with probability
                if (UnityEngine.Random.value < pigeonPowerUpSpawnProbability)
                {
                    Instantiate(pigeonPowerUpPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(pigeonPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                }


                //ObjectPooler.Instance.SpawnFromPool("PigeonPoolName", new Vector3(xPos, yPos, 0), Quaternion.identity);

                float seconds = SpeedToSpawnSecondsDelay();
                yield return new WaitForSeconds(seconds);
            }
        }

    }
}

