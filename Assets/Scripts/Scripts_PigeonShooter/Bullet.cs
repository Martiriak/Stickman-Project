using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pigeonShooter
{
    /// <summary>
    /// Script attached to Bullet.
    /// Disables bullet after some time.
    /// </summary>
    public class Bullet : MonoBehaviour, IPooledObject
    {
        public void OnObjectSpawn()
        {
            //Destroy(gameObject, 1);
            //StartCoroutine(DisableBullet());
        }

        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
        }
    }
}

