using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pigeonShooter
{
    public class Bullet : MonoBehaviour, IPooledObject
    {
        public void OnObjectSpawn()
        {
            //Destroy(gameObject, 1);
            StartCoroutine(DisableBullet());
        }

        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(5);
            gameObject.SetActive(false);
        }
    }
}

