using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Stickman.pigeonShooter
{
    public class PigeonFall : MonoBehaviour, IPooledObject
    {
        //private void Start()
        //{
        //    Destroy(gameObject, 2.5f);
        //}

        public void OnObjectSpawn()
        {
            //Destroy(gameObject, 1);
            StartCoroutine(DisablePigeonFall());
        }

        IEnumerator DisablePigeonFall()
        {
            yield return new WaitForSeconds(5);
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.Translate(Vector3.down * Time.fixedDeltaTime * 5f);
        }
    }
}


