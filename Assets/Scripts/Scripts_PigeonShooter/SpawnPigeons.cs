using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pigeonShooter
{
    public class SpawnPigeons : MonoBehaviour
    {
        public GameObject pigeonPrefab;

        private float maxXPos = 10f; // hard-coded boundaries of max-min spawn pos
        private float minXPos = -1f;
        private float maxYPos = 6f;
        private float minYPos = 0f;

        private float xPos;
        private float yPos;

        private void Start()
        {
            StartPigeonSpawn();
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
            while (true)
            {
                if (UnityEngine.Random.value > 0.5f) // with prob 50%
                {
                    // spawn pigeon with x fixed (in front of player) and rand y
                    xPos = maxXPos;
                    yPos = UnityEngine.Random.Range(minYPos, maxYPos);
                }
                else
                {
                    // spawn pigeon with y fixed (on top of player) and rand x
                    yPos = maxYPos;
                    xPos = UnityEngine.Random.Range(minXPos, maxXPos);
                }
                Instantiate(pigeonPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}

